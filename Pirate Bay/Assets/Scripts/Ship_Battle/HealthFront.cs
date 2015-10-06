using UnityEngine;
using System.Collections;

public class HealthFront : MonoBehaviour {

    public Ship owner;

    // Use this for initialization
    void Start()
    {
        if (owner != null)
        {
            transform.position = (new Vector2(owner.transform.position.x,owner.transform.position.y)
                + new Vector2(-0.8f, 1.5f));
            transform.SetParent(owner.gameObject.transform);
            GetComponent<SpriteRenderer>().enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (owner != null)
        {
            transform.localScale = new Vector3(owner.health / 100.0f, 1.0f, 1.0f);

            if (owner.IsDead())
            {
                gameObject.SetActive(false);
            }
        }
        transform.rotation = Quaternion.identity;
    }
}
