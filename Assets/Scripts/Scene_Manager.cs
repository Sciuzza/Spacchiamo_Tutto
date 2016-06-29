using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour {

    
    Scene currentScene, nextScene;

    void Awake()
    {
        

    }

	// Use this for initialization
	void Start () {

        currentScene = SceneManager.GetActiveScene();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
