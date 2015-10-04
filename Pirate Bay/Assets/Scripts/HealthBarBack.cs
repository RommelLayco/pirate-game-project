using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarBack : MonoBehaviour
{

    public Combatant owner;

    // Use this for initialization
    void Start()
    {
        transform.position = owner.transform.position + new Vector3(-0.8f, 1.5f, 0.0f);
        transform.SetParent(owner.gameObject.transform);
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (owner.IsDead())
        {
            gameObject.SetActive(false);
        }
    }
}
