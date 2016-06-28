using UnityEngine;
using System.Collections;

public class Game_Controller : MonoBehaviour {


    Audio_Manager audioManagerLinking;
    Scene_Manager sceneManagerLinking;
    Grid_Space_Manager gridManagerLinking;

    void Awake()
    {
        gridManagerLinking = GameObject.Find("Grid Space").GetComponent<Grid_Space_Manager>();

        
    }

    // Use this for initialization
    void Start () {

        gridManagerLinking.PrepareGridSpace(8, 8);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
