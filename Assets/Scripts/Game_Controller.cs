using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Spacchiamo {
    public class Game_Controller : MonoBehaviour {



        public enum GAME_PHASE : byte { init, playerTurn, animation};
        public GAME_PHASE currentPhase = GAME_PHASE.playerTurn;
       
        Camera_Movement cameraLink;
        GameObject playerTemp;

       [HideInInspector]
        public static Game_Controller instance = null;

        
        void Awake()
        {
                           
            //Check if instance already exists
            if (instance == null)

                //if not, set instance to this
                instance = this;

            //If instance already exists and it's not this:
            else if (instance != this)

                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);

           
        
    }
        
        void Start() {

            // Getting Camera Reference 
            cameraLink = GameObject.Find("Main Camera").GetComponent<Camera_Movement>();


            //Initalizing Level Grid Space
            Grid_Manager.instance.PreparingGridSpace();

            //Initializing Player Starting Position 
            playerTemp = Resources.Load<GameObject>("Player");
            playerTemp = Instantiate(playerTemp);
                      

        }

    
        public void InitializingPlayer()
        {
            // Initializing movement script variables and player position
            PMovement playerLink = playerTemp.GetComponent<PMovement>();

            //if scene is n then else if...
           playerLink.whereI = 0;
           playerLink.whereJ = 0;

            cameraLink.target = playerTemp.transform;
        }
    }
}