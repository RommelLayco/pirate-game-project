using UnityEngine;
using UnityEngine.UI;

public class NameText : MonoBehaviour
{
    public Combatant combatant;
    void Update()
    {
        if(combatant != null)
        {
            GetComponent<Text>().text = combatant.combatantName;
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            transform.position = (combatant.transform.position);
            transform.position += new Vector3(0f,1.5f,0f);
        }

    }
}
