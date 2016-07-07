using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

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


        public List<bool> allMoved;
        public int moveEnemyCounter = 0;


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
            allMoved = new List<bool>();
            InitializingMoveList();
            ResettingEnemyMoves();

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

            //AGGIUNTA DI MARCO
            enemyLinker = movingObjTemp.GetComponent<Enemy_Controller>();
            //FINE AGGGIUNTA DI MARCO


            //Initializing Enemy
            for (int i = 1; i <= Designer_Tweaks.instance.level1EnemiesQuantity; i++)
            {
                switch (Random.Range(1, 4))
                {
                    case 1:
                        movingObjTemp = Resources.Load<GameObject>("Enemy1");
                        movingObjTemp = Instantiate(movingObjTemp);
                        break;
                    case 2:
                        movingObjTemp = Resources.Load<GameObject>("Enemy2");
                        movingObjTemp = Instantiate(movingObjTemp);
                        break;
                    default:
                        movingObjTemp = Resources.Load<GameObject>("Enemy3");
                        movingObjTemp = Instantiate(movingObjTemp);
                        break;
                }



            }
        }


        void Update()
        {
            /*
            if (Input.GetKeyDown(KeyCode.Space))
                Scene_Manager.instance.ResettingLevel();
                */

            if (currentPhase == GAME_PHASE.npcEnemyTurn)
            {
                if (!allMoved.Contains(false))
                {

                    ResettingEnemyMoves();
                    currentPhase = GAME_PHASE.playerTurn;
                }
            }

        }


        //AGGIUNTA DI MARCO
        public void ChangePhase(GAME_PHASE passedPhase)
        {

            switch (passedPhase)
            {
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

        public int GettingRowPStartPosition()
        {
            return 0;
        }

        public int GettingColumnPStartPosition()
        {
            return 0;
        }

        public void ResettingEnemyMoves()
        {
            for (int i = 0; i < allMoved.Count; i++)
                allMoved[i] = false;
            moveEnemyCounter = 0;
        }

        public void AddingAMove()
        {
            allMoved[moveEnemyCounter] = true;
            moveEnemyCounter++;
        }

        public void InitializingMoveList()
        {
            for (int i = 0; i < Designer_Tweaks.instance.level1EnemiesQuantity; i++)
                allMoved.Add(false);
        }
    }
}