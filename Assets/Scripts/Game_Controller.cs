using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Spacchiamo
{
    public class Game_Controller : MonoBehaviour
    {


        // Game Phases
        public enum GAME_PHASE : byte { init, playerTurn, npcEnemyTurn };
        public GAME_PHASE currentPhase = GAME_PHASE.playerTurn;

        // Camera and Player References
        Camera_Movement cameraLink;
        GameObject movingObjTemp;

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

        void Start()
        {

            // Getting Camera Reference 
            cameraLink = GameObject.Find("Main Camera").GetComponent<Camera_Movement>();


            //Initalizing Level Grid Space
            Grid_Manager.instance.PreparingGridSpace();

            //Initializing Player 
            movingObjTemp = Resources.Load<GameObject>("Player");
            movingObjTemp = Instantiate(movingObjTemp);

            //Initializing Enemy
            movingObjTemp = Resources.Load<GameObject>("Enemy1");
            movingObjTemp = Instantiate(movingObjTemp);

        }


        // Methods necessary for the Player Initialization
        public void InitializingCamera(GameObject playerInstance)
        {
            cameraLink.target = playerInstance;
        }

        public int GettingRowStartPosition()
        {
            return 0;
        }

        public int GettingColumnStartPosition()
        {
            return 0;
        }
    }
}