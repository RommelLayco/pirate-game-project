using UnityEngine;
using System.Collections;

public class BuffIcon : MonoBehaviour {
    private bool shaking;
    private Vector3 originalpos;
    private IEnumerator Shake(float time)
    {
        shaking = true;
        yield return new WaitForSeconds(time);
        shaking = false;
    }

    public void ShakeIcon(float time)
    {
        originalpos = transform.position;
        StartCoroutine(Shake(time));
        transform.position = originalpos;
    }

    void Update()
    {

        if(shaking)
        {
            
            transform.position = originalpos + new Vector3(Random.Range(-.05f, .05f), Random.Range(-.05f, .05f), 0);
        }
    }
}
