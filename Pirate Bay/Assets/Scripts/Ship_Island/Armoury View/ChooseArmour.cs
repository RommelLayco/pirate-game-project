using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChooseArmour : MonoBehaviour {

    public Armour armour ;
    
    // Update is called once per frame
    void Update() {

        /*foreach (Touch t in Input.touches) {
            if (t.phase == TouchPhase.Ended) {

                if (t.tapCount == 2) {
                    bool contained = gameObject.GetComponent<Collider>().bounds.Contains(t.position);

                    if (contained) {
                        clicked();
                    }
                }

            }

        }*/

    }

    void OnMouseDown() {
        clicked();
    }


    // save the armour to the crew member and also set the sprite
    public void clicked() {
        DisplayController.setOutlines();

        transform.parent.gameObject.GetComponentInChildren<OutlineController>().setSprite(OutlineController.colours.GREEN);


        /*
        
        GameManager manager = GameManager.getInstance();
        CrewMemberData crew = manager.crewMembers[manager.crewIndex];


        armour.setCrewMember(crew);
        crew.setArmour(armour);
        */

        

    }
}
