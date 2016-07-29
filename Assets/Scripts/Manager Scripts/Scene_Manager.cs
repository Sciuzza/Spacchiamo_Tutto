using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Spacchiamo
{
    public class Scene_Manager : MonoBehaviour
    {


        public Scene currentScene, nextScene;

        public int nextSceneIndex = 0;

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
            SceneManager.LoadScene(nextSceneIndex);
        }

        public void LoadAbilityScene()
        {
            nextSceneIndex = currentScene.buildIndex;
            nextSceneIndex++;
            SceneManager.LoadScene(5);
        }

       

        public void LoadSpecificScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public int GetCurrentSceneIndex()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }     

    
        public void LoadExit()
        {
            Application.Quit();
        }

       

    }
}
