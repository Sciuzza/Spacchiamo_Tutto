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


        
        void Start()
        {

            currentScene = SceneManager.GetActiveScene();

        }

    
    }
}
