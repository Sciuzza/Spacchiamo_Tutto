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

		//AGGIUNTA DI MARCO
		Player_Controller playerLinker;
		Enemy_Controller enemyLinker;
		//FINE AGGGIUNTA DI MARCO

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
			//AGGIUNTA DI MARCO
			playerLinker = movingObjTemp.GetComponent<Player_Controller>();
			//FINE AGGGIUNTA DI MARCO

            //Initializing Enemy
            movingObjTemp = Resources.Load<GameObject>("Enemy1");
            movingObjTemp = Instantiate(movingObjTemp);
			//AGGIUNTA DI MARCO
			enemyLinker = movingObjTemp.GetComponent<Enemy_Controller>();
			//FINE AGGGIUNTA DI MARCO

        }


		//AGGIUNTA DI MARCO
		public void ChangePhase (GAME_PHASE passedPhase) {

			switch (passedPhase) {
			case GAME_PHASE.playerTurn:
				currentPhase = GAME_PHASE.npcEnemyTurn;
				break;
			case GAME_PHASE.npcEnemyTurn:
				currentPhase = GAME_PHASE.playerTurn;
				break;
			default:
				break;
			}

		}
		//FINE AGGGIUNTA DI MARCO


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