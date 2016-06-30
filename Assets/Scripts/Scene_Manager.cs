using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Spacchiamo
{
    public class Scene_Manager : MonoBehaviour
    {


        public Scene currentScene, nextScene;



        // Use this for initialization
        void Start()
        {

            currentScene = SceneManager.GetActiveScene();

        }

        // Update is called once per frame
        void Update()
        {

            // By Menu to level 1 pressing start game
            if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetSceneByName("Menu") == currentScene)
            {
                nextScene = SceneManager.GetSceneAt(1);
                SceneManager.LoadScene(1);
            }

        }
    }
}
