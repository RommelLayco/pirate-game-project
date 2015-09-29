using UnityEngine;
using System.Collections;

public class OnMouseClick : MonoBehaviour {

    void Update() {

		/**
		foreach (Touch t in Input.touches)
		{
			if (t.phase == TouchPhase.Ended)
			{

				bool contained = gameObject.GetComponent<Collider>().bounds.Contains(t.position);
				if(contained)
					clicked();
			}
			
		}


        if (Input.GetMouseButton(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null) {
                clicked();
            }
        }

**/
        
    }

	/**
    void clicked() {
        Application.LoadLevel("ExtendableMap");
    }
    **/
}
