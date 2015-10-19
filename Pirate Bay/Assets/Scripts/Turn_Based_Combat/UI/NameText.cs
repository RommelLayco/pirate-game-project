using UnityEngine;
using UnityEngine.UI;

// Used to control the show/hide of combatant unit names
public class NameText : MonoBehaviour
{
    private Combatant owner;

    void Start()
    {
        transform.SetParent(GameObject.Find("Canvas").transform);
        GetComponent<Text>().enabled = true;
    }

    public void SetOwner(Combatant owner)
    {
        this.owner = owner;
    }

    void Update()
    {
        // Show the names only during player turn and choosing enemy
        CombatManager.State state = GameObject.Find("Combat Manager").GetComponent<CombatManager>().GetState();
        if (state == CombatManager.State.Resolve || state == CombatManager.State.EndTurn || state == CombatManager.State.EnemyTurn
            || state == CombatManager.State.ResolveBuffs)
        {
            GetComponent<Text>().enabled = false;            
        }
        else
        {
            if (owner != null)
            {
                GetComponent<Text>().enabled = true;
                GetComponent<Text>().text = owner.combatantName;
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                transform.position = owner.transform.position;
                transform.position += new Vector3(0f, -1.5f, 0f);
            }
        }
    }

    public Combatant GetOwner()
    {
        return owner;
    }
}
