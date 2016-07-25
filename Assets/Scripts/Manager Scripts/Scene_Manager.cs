using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Spacchiamo
{
    public class Scene_Manager : MonoBehaviour
    {


        public Scene currentScene, nextScene;

        [HideInInspector]
        public static Scene_Manager instance = null;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

    
        public void ResettingLevel()
        {

            SceneManager.LoadScene(currentScene.buildIndex);
       
        }

        public void SceneManagerInitialization()
        {
            currentScene = SceneManager.GetActiveScene();
        }

        public void LoadNextLevel()
        {
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }

    }
}
