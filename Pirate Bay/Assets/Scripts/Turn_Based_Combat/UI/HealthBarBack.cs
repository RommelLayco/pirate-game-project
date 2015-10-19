using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Controls the background (red) part of the health bars
public class HealthBarBack : MonoBehaviour
{
    private Combatant owner;

    void Start()
    {
        if (owner != null)
        {
            transform.position = owner.transform.position + new Vector3(-0.8f, 1.5f, 0.0f);
            transform.SetParent(owner.gameObject.transform);
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            GetComponent<SpriteRenderer>().enabled = true;
        }

    }

    void Update()
    {
        if (owner != null && owner.IsDead())
        {
            gameObject.SetActive(false);
        }
    }

    public void SetOwner(Combatant owner)
    {
        this.owner = owner;
    }
}
