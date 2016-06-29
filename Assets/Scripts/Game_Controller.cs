using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour {


    Audio_Manager audioManagerLinking;
    Scene_Manager sceneManagerLinking;
    Grid_Manager gridManagerLinking;
    Designer_Tweaks designTweaksLinking;
    GameObject playerLinking;
    

    void Awake()
    {
        playerLinking = Resources.Load<GameObject>("Player");
        audioManagerLinking = GameObject.Find("Audio Manager").GetComponent<Audio_Manager>();
        sceneManagerLinking = GameObject.Find("Scene Manager").GetComponent<Scene_Manager>();
        gridManagerLinking = GameObject.Find("Grid Manager").GetComponent<Grid_Manager>();
        designTweaksLinking = GameObject.Find("Designer Variables").GetComponent<Designer_Tweaks>();
        
    }

    // Use this for initialization
    void Start () {

        //Initalizing Level Grid Space
        gridManagerLinking.PreparingGridSpace(designTweaksLinking.level1Rows, designTweaksLinking.level1Columns);
        playerLinking = Instantiate(playerLinking);

        //Initializing Player Starting Position;
        Transform cellStartingP = gridManagerLinking.ReturningStartPosition();
        playerLinking.transform.position = new Vector3(cellStartingP.position.x, cellStartingP.position.y, -3);
         
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
