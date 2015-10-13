using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChooseArmour : MonoBehaviour {

    private List<Armour> armoury;

    void Start() {
        armoury = GameManager.getInstance().armoury;
    }

    // Update is called once per frame
    void Update() {

        foreach (Touch t in Input.touches) {
            if (t.phase == TouchPhase.Ended) {

                if (t.tapCount == 2) {
                    bool contained = gameObject.GetComponent<Collider>().bounds.Contains(t.position);

                    if (contained) {
                        clicked();
                    }
                }

            }

        }

    }

    void OnMouseDown() {
        clicked();
    }

    // save the armour to the crew member and also set the sprite
    void clicked() {
        GameObject currentShowingArmour = GameObject.FindGameObjectWithTag("Armour");
        currentShowingArmour.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;


        // save the armour for the crew member

        // need to change this so that it saves the clicked armour
        Armour toSave = armoury[0];



        // save the armour to the crew member
        GameManager.getInstance().currentInArmory.setArmour(toSave);


    }
}
