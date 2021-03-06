﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

/**
    Controls the main flow of the turn-based-combat. Implemented as a FSM. At each update the current state is checked 
    and the corresponding handler called accordingly.
**/
public class CombatManager : MonoBehaviour {

    // States of the FSM
    public enum State {
        CombatStart, CrewMemberTurn, ChooseEnemy, EnemyTurn, CleanupActions,
        Resolve, ResolveBuffs, EndTurn, CombatWon, CombatLost, CombatFinish
    }
    private State state;

    // Index of the combatant unit who has the current turn
    private int currentIndex;

    // List of actions to be carried out in the Resolve states 
    private ActionList actions = new ActionList();

    // List of combatant units. The list of combatants is the union of enemies and crewmembers.
    private List<Combatant> combatants = new List<Combatant>();
    private List<Combatant> enemies = new List<Combatant>();
    private List<Combatant> crewMembers = new List<Combatant>();

    // Flags that determine user input
    private bool choseAttack = false;
    private bool choseAbility = false;
    private Combatant target = null;

    // Flag for win/loss condition
    private bool won = false;

    // Position vectors for combatant placement
    private List<Vector3> enemyPositions;
    private List<Vector3> crewPositions;

    // Initialisation
    void Start() {

        // Set to combat starting state
        state = State.CombatStart;

        // Set arbitrary fixed positions for enemy placement
        enemyPositions = new List<Vector3>();
        enemyPositions.Add(new Vector3(5.8f, -0.71f));
        enemyPositions.Add(new Vector3(7.83f, -2.82f));
        enemyPositions.Add(new Vector3(7.88f, 1.5f));
        enemyPositions.Add(new Vector3(3.69f, 1.56f));
        enemyPositions.Add(new Vector3(3.74f, -2.92f));

        // Randomly generate enemy objects from the EnemyGenerator with the specified types and max number of units.
        // Depends on the current island level.
        HashSet<EnemyGenerator.EnemyType> enemytypes = new HashSet<EnemyGenerator.EnemyType>();
        int islandLevel = GameManager.getInstance().islandLevel;
        List<GameObject> enemyList;
        if (islandLevel == 1) {
            enemytypes.Add(EnemyGenerator.EnemyType.Snake);
            enemytypes.Add(EnemyGenerator.EnemyType.Maneater);
            enemyList = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>().
                GenerateEnemyList(enemytypes, 1, 2);
        }
        else if (islandLevel == 2)
        {
            enemytypes.Add(EnemyGenerator.EnemyType.EnemyPirate);
            enemyList = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>().
                GenerateEnemyList(enemytypes, 1, 3);
        }
        else if (islandLevel == 3)
        {
            enemytypes.Add(EnemyGenerator.EnemyType.Maneater);
            enemytypes.Add(EnemyGenerator.EnemyType.GiantCrab);
            enemyList = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>().
                GenerateEnemyList(enemytypes, 2, 4);
        }
        else if (islandLevel == 4)
        {
            enemytypes.Add(EnemyGenerator.EnemyType.Snake);
            enemytypes.Add(EnemyGenerator.EnemyType.Maneater);
            enemytypes.Add(EnemyGenerator.EnemyType.GiantCrab);
            enemyList = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>().
                GenerateEnemyList(enemytypes, 2, 5);
        }
        else if (islandLevel == 5)
        {
            enemytypes.Add(EnemyGenerator.EnemyType.EnemyPirate);
            enemytypes.Add(EnemyGenerator.EnemyType.Snake);
            enemytypes.Add(EnemyGenerator.EnemyType.GiantCrab);
            enemyList = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>().
                GenerateEnemyList(enemytypes, 3, 5);
        }
        else
        {
            enemyList = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>().
                GenerateEnemyList(null, 4, 5);
        }

        // Instantiate enemy game objects and place them at their position
        for (int i = 0; i < enemyList.Count; i++) {
            GameObject g = Instantiate(enemyList[i]);
            g.transform.position = enemyPositions[i];
            combatants.Add(g.GetComponent<Enemy>());
            enemies.Add(g.GetComponent<Enemy>());
            g.GetComponent<Enemy>().scaleStatsBy((islandLevel * 0.1f) + 0.9f);
        }

        // Set arbitrary fixed positions for crew placement
        crewPositions = new List<Vector3>();
        crewPositions.Add(new Vector3(-3.5f, 0f));
        crewPositions.Add(new Vector3(-6f, -2f));
        crewPositions.Add(new Vector3(-6.5f, 2.3f));

        // Fetch crew team from CrewGenerator and place them at their positions
        List<CrewMember> crewList = GameObject.Find("CrewGenerator").GetComponent<CrewGenerator>().GenerateCrewList();
        for (int i = 0; i < crewList.Count; i++) {
            crewList[i].transform.position = crewPositions[i];
            combatants.Add(crewList[i]);
            crewMembers.Add(crewList[i]);
        }

        // Hide exp information
        GameObject.Find("Exp Info").GetComponent<Text>().enabled = false;
        GameObject.Find("XPImage").GetComponent<Image>().enabled = false;

    }

    // At each frame, check state and call corresponding function
    void FixedUpdate() {
        switch (state) {
            case State.CombatStart: CombatStart(); break;
            case State.CrewMemberTurn: CrewMemberTurn(); break;
            case State.ChooseEnemy: ChooseEnemy(); break;
            case State.CleanupActions: CleanupActions(); break;
            case State.EnemyTurn: EnemyTurn(); break;
            case State.Resolve: Resolve(); break;
            case State.ResolveBuffs: ResolveBuffs(); break;
            case State.EndTurn: EndTurn(); break;
            case State.CombatWon: CombatWon(); break;
            case State.CombatLost: CombatLost(); break;
            case State.CombatFinish: CombatFinish(); break;
        }
    }

    // Called once at the start of the combat.
    void CombatStart() {

        // Sort combatants by speed to determine order
        combatants.Sort();

        // Show combat encounter message
        actions.Add(new ActionInfo("Enemy encountered!"));
        actions.Add(new ActionPauseForFrames(90));
        actions.Add(new ActionHideInfo());

        // Switch to Resolve state to carried out above actions. Current index is set to the last
        // of the list as it cycles back to the first in the Resolve function.
        currentIndex = combatants.Count - 1;
        state = State.Resolve;

    }

    // Called at the start of a crew's turn
    void CrewMemberTurn() {

        // Show info message
        GameObject.Find("Battle Info").GetComponent<BattleText>().ShowText(combatants[currentIndex].combatantName + " 's turn!");

        // Show command buttons and reset selection flags
        choseAttack = false;
        choseAbility = false;
        ShowAttackButton(true);

    }

    // Called when a crew has selected its action and an enemy needs to be targeted
    void ChooseEnemy() {

        GameObject.Find("Battle Info").GetComponent<BattleText>().ShowText("Choose enemy");

        // Awaits user input. Once an enemy is chosen, set it as the target for this turn
        foreach (Enemy e in enemies) {
            if (e.IsTargeted()) {
                target = e;
                foreach (Enemy e1 in enemies) {
                    e1.Untarget(); // Reset enemy target state
                }
                state = State.CleanupActions;
                break;
            }
        }

    }

    // Called after an enemy is chosen for a particular action
    void CleanupActions() {

        // For an attack action
        if (choseAttack) {
            state = State.Resolve;
            AbilityBasicAttack basicAttack = new AbilityBasicAttack();
            basicAttack.SetTarget(target);
            Queue<Action> abilityActions = basicAttack.GetActions(combatants[currentIndex], crewMembers, enemies);
            while (abilityActions.Count > 0) {
                actions.Add(abilityActions.Dequeue());
            }
        }

        // For an ability action
        if (choseAbility) {
            // Get the ability of the current crew and set its target to the selected enemy
            AbilityTargeted ability = combatants[currentIndex].ability as AbilityTargeted;
            ability.SetTarget(target);
            if (ability != null) {
                Combatant me = combatants[currentIndex];
                Queue<Action> abilityActions = ability.GetActions(me, crewMembers, enemies);
                while (abilityActions.Count > 0) {
                    actions.Add(abilityActions.Dequeue());
                }
                combatants[currentIndex].ability.PutOnCD(); // Put ability on cooldown
            }
        }

        // Retrieve status effects and activate them at the end of turn

        state = State.Resolve;

    }

    // Called at an enemy's turn
    void EnemyTurn() {

        state = State.Resolve;

        Enemy currentEnemy = combatants[currentIndex] as Enemy;

        // Obtain the enemy's action from its AI
        Queue<Action> abilityActions = currentEnemy.ActionAI(crewMembers, enemies);
        while (abilityActions.Count > 0) {
            actions.Add(abilityActions.Dequeue());
        }

    }

    // Called at the end of each turn to execute the enqueued actions
    void Resolve() {

        // Continue action at each frame until it is finished (mainly for game object movement).
        if (actions.IsDone()) {
            //check to see if combat over
            if (checkWinLoss())
                return;
            //Else retrieve status effects and activate them at the end of turn

            Queue<Action> buffEffects = combatants[currentIndex].GetBuffEffect();
            while (buffEffects.Count > 0)
            {
                actions.Add(buffEffects.Dequeue());
            }
            state = State.ResolveBuffs;
            return;
        }
        actions.Work(Time.deltaTime);

    }

    void ResolveBuffs()
    {
        
        if (actions.IsDone())
        {
            state = State.EndTurn;
            return;
        }
        actions.Work(Time.deltaTime);
    }
    

    // Called after all action are resolved to start the next turn
    void EndTurn() {
        if (checkWinLoss())
            return;
        // Check whether win/loss conditions are achieved. Switch to combat end states if so.
        // End turn updates for current combatant
        combatants[currentIndex].ability.ReduceCD();
        combatants[currentIndex].buffs.ReduceDuration();
        combatants[currentIndex].UnsetSelectionRing();

        // Switch to next (not dead) combatant in queue, back to first when end of queue reached
        do {
            currentIndex += 1;
            if (currentIndex >= combatants.Count)
                currentIndex = 0;
        } while (combatants[currentIndex].IsDead());

        // Determine whether it is a crew or enemy's turn
        if (combatants[currentIndex] as CrewMember != null) {
            state = State.CrewMemberTurn;
        }
        if (combatants[currentIndex] as Enemy != null) {
            state = State.EnemyTurn;
        }
        combatants[currentIndex].SetSelectionRing();
    }

    // Called once when combat is won
    void CombatWon() {
        Debug.Log("Combat Won");
        GameObject.Find("Battle Info").GetComponent<BattleText>().ShowText("You Win!");

        // Get exp gain from enemies
        float expGained = 0;
        foreach (Enemy e in enemies) {
            expGained += e.getExp();
        }


        StringBuilder expDisplay = new StringBuilder();
        List<CrewMember> dead = new List<CrewMember>();
        foreach (CrewMember m in crewMembers) {
            //Restoring health to crew members between battles
            m.increaseHealth();

            //Need to remove dead crew members. 
            if (m.health <= 0.0f) {
                dead.Add(m);
            }
        }
        foreach (CrewMember m in dead) {
            expDisplay.Append(m.combatantName + " died, and was removed from your crew :(");
            m.crewDied();
            crewMembers.Remove(m);
        }

        // Persist exp gain for each crew member and display exp info
        foreach (CrewMember m in crewMembers) {
            int levelUp = m.persistExp(expGained);
            if (levelUp > 0) {
                expDisplay.Append(m.combatantName + " gained " + expGained + " experience!\n");
                expDisplay.Append(m.combatantName + " levelled up by " + levelUp + "!\n");
            } else if (levelUp == 0) {
                expDisplay.Append(m.combatantName + " gained " + expGained + " experience!\n");
            } else {
                expDisplay.Append(m.combatantName + " is already at maximum level!\n");
            }
            m.persistHealth(); // persist crew member health after battle
        }

        GameObject.Find("Exp Info").GetComponent<Text>().enabled = true;
        GameObject.Find("XPImage").GetComponent<Image>().enabled = true;
        GameObject.Find("Exp Info").GetComponent<Text>().text = expDisplay.ToString();

        won = true;
        state = State.CombatFinish;

    }

    // Called once if lost combat
    void CombatLost() {

        StringBuilder expDisplay = new StringBuilder();
        foreach (CrewMember m in crewMembers)
        {
            expDisplay.Append(m.combatantName + " died, and was removed from your crew :(");
            m.crewDied();
        }

        GameObject.Find("Exp Info").GetComponent<Text>().enabled = true;
        GameObject.Find("XPImage").GetComponent<Image>().enabled = true;
        GameObject.Find("Exp Info").GetComponent<Text>().text = expDisplay.ToString();

        GameObject.Find("Battle Info").GetComponent<BattleText>().ShowText("You Lose...");

        GameObject.Find("Exp Info").GetComponent<Text>().enabled = true;
        GameObject.Find("XPImage").GetComponent<Image>().enabled = true;
        GameObject.Find("Exp Info").GetComponent<Text>().text = expDisplay.ToString();

        won = false;
        state = State.CombatFinish;

        GameObject.Find("ContinueButton").GetComponentInChildren<Text>().text = "Return to ship";

    }

    // The state awaiting user input to exit combat scene
    public void CombatFinish() {

        GameObject.Find("ContinueButton").GetComponent<Button>().interactable = true;
        GameObject.Find("ContinueButton").GetComponentInChildren<Text>().enabled = true;

    }

    public bool checkWinLoss() {
        bool win = true;
        bool lose = true;
        foreach (Combatant combatant in combatants) {
            if (!combatant.IsDead()) {
                if (combatant as CrewMember != null) {
                    lose = false;
                }
                if (combatant as Enemy != null) {
                    win = false;
                }
            }
        }
        if (win) {
            state = State.CombatWon;
            return true;
        } else if (lose) {
            state = State.CombatLost;
            
			// decrease notoriety by 5 percent if battle is lost
			GameManager.getInstance ().notoriety = GameManager.getInstance ().notoriety - (int)Math.Ceiling(GameManager.getInstance ().notoriety * 0.05);

			//GameManager.getInstance().notoriety--;


            return true;
        }
        return false;
    }

    // Attached to the Attack command button, called on click
    public void ChoseAttack() {

        ShowAttackButton(false);
        choseAttack = true;
        state = State.ChooseEnemy;

        // Get targetable enemies and set them as targetable for this turn
        List<Combatant> targetables = combatants[currentIndex].GetTargetable(enemies);
        foreach (Combatant e in targetables) {
            if (!e.IsDead())
                e.SetTargetable();
        }
        state = State.ChooseEnemy;

    }

    // Attached to the Ability command button, called on click
    public void ChoseAbility() {

        choseAbility = true;
        ShowAttackButton(false);

        // if the ability targets a specific enemy
        if (combatants[currentIndex].ability.needsTarget) {
            List<Combatant> targetables = combatants[currentIndex].GetTargetable(enemies);
            foreach (Combatant e in targetables) {
                if (!e.IsDead())
                    e.SetTargetable();
            }
            state = State.ChooseEnemy;

            // if the ability does not need a target
        } else {
            Ability ability = combatants[currentIndex].ability;
            if (ability != null) {
                Combatant me = combatants[currentIndex];
                Queue<Action> abilityActions = ability.GetActions(me, crewMembers, enemies);
                while (abilityActions.Count > 0) {
                    actions.Add(abilityActions.Dequeue());
                }
                combatants[currentIndex].ability.PutOnCD();

                state = State.Resolve;
            }
        }
    }

    // Activate/deactivate the command buttons at a crew's turn
    public void ShowAttackButton(bool show) {

        GameObject attackButton = GameObject.Find("ButtonAttack");
        GameObject abilityButton = GameObject.Find("ButtonAbility");
        attackButton.GetComponent<Button>().interactable = show;
        abilityButton.GetComponent<Button>().interactable = show;

        if (show) {
            Ability ability = combatants[currentIndex].ability;
            attackButton.GetComponentInChildren<Text>().text = "Attack";
            attackButton.transform.position = combatants[currentIndex].transform.position;
            attackButton.transform.position += new Vector3(1.0f, 2.2f);
            bool hasAbility = (ability != null && ability.GetCD() <= 0);
            abilityButton.GetComponent<Button>().interactable = hasAbility;
            abilityButton.transform.position = combatants[currentIndex].transform.position;
            abilityButton.transform.position += new Vector3(-1.0f, 2.2f);

            if (ability == null)
                abilityButton.GetComponentInChildren<Text>().text = "No Ability";
            else if (ability.GetCD() == 0)
                abilityButton.GetComponentInChildren<Text>().text = ability.name;
            else
                abilityButton.GetComponentInChildren<Text>().text = ability.name + " (" + ability.GetCD() + ")";
        } else {
            attackButton.GetComponentInChildren<Text>().text = "";
            abilityButton.GetComponentInChildren<Text>().text = "";
        }

    }

    // Attached to the continue button shown at the end of combat, sends player back to maze or ship depending on 
    // whether won or lost
    public void Continue() {
        if (won) {
            Application.LoadLevel("Maze");
        } else {
            Application.LoadLevel("Ship");
        }

    }

    public State GetState() {
        return state;
    }

}
