using UnityEngine;
using System.Collections;

public class HealthBack : MonoBehaviour {

    public Ship owner;

    // Use this for initialization
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

    // Update is called once per frame
    void Update()
    {
        if (owner != null && owner.IsDead())
        {
            gameObject.SetActive(false);
        }
    }
}
