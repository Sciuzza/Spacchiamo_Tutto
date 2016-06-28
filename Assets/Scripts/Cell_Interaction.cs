using UnityEngine;
using System.Collections;

public class Cell_Interaction : MonoBehaviour {

   

    // Use this for initialization
    void Start () {

        Invoke("FixingCollider", 0.2f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    void FixingCollider()
    {
        BoxCollider2D colliderReference;
        RectTransform rtReference;

        // Fixing Box Collider Size to match cell size
        colliderReference = GetComponent<BoxCollider2D>();
        rtReference = GetComponent<RectTransform>();
        colliderReference.size = new Vector2(rtReference.rect.width, rtReference.rect.height);
    }

}
