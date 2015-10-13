using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DisplayController : MonoBehaviour {
    private int COLUMNS = 4;
    private int GRIDENTRIES = 12;

    public GameObject armour;

    public GameObject empty;

    public Text textPrefab;

    public GameObject weapon;

    private int defX, defY;
    private int x;
    private int y;

    private GameManager manager;

    void Awake() {
        manager = GameManager.getInstance();

    }

    // Use this for initialization
    void Start() {
        x = -200;
        y = 90;
        defX = x;
        defY = y;



    }

    public void weaponClicked() {
        // add the weapons
        for (int i = 0; i < GRIDENTRIES; i++) {
            if (i < manager.weapons.Count) {
                GameObject temp = Instantiate(weapon) as GameObject;

                temp.transform.position = new Vector3(x, y, 0);

                temp.transform.SetParent(gameObject.transform, false);

                Text t = (Text)Instantiate(textPrefab, new Vector3(temp.transform.position.x, temp.transform.position.y, 0), Quaternion.identity);

                t.transform.SetParent(gameObject.transform);
                t.transform.localScale = new Vector3(1, 1, 1);

                t.transform.position = temp.transform.position + new Vector3(2.6f, 1.25f, 0f);

                // set the text to the value in the weapon
                t.text = manager.weapons[i].getStrength().ToString();

            } else {
                //add an empty block
                GameObject temp = Instantiate(empty) as GameObject;
                temp.transform.position = new Vector3(x, y, 0);
                temp.transform.SetParent(gameObject.transform, false);
            }

            //Update the x and y pos
            if ((i + 1) % COLUMNS == 0) {
                x = defX;
                y = y - 100;

            } else {
                x = x + 150;
            }
        }
    }

    public void armourClicked() {
        //for (int i = 0; i < manager.armoury.Count; i++) {
        for (int i = 0; i < GRIDENTRIES; i++) {
            if (i < manager.armoury.Count) {
                GameObject temp = Instantiate(armour) as GameObject;

                temp.transform.position = new Vector3(x, y, 0);

                temp.transform.SetParent(gameObject.transform, false);

                Text t = (Text)Instantiate(textPrefab, new Vector3(temp.transform.position.x, temp.transform.position.y, 0), Quaternion.identity);

                t.transform.SetParent(gameObject.transform);
                t.transform.localScale = new Vector3(1, 1, 1);

                t.transform.position = temp.transform.position + new Vector3(2.6f, 1.25f, 0f);

                // set the text to the value in the armour
                t.text = manager.armoury[i].getStrength().ToString();

            } else {
                //add an empty block
                GameObject temp = Instantiate(empty) as GameObject;
                temp.transform.position = new Vector3(x, y, 0);
                temp.transform.SetParent(gameObject.transform, false);
            }

            //Update the x and y pos
            if ((i + 1) % COLUMNS == 0) {
                x = defX;
                y = y - 100;

            } else {
                x = x + 150;
            }
        }

    }
    public void onClosePanel() {
        x = defX;
        y = defY;
    }
}