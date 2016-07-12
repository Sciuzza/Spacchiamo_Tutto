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


        


        [HideInInspector]
        public static Game_Controller instance = null;


        void Awake()
        {

            #region SingleTone

            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject); 
            #endregion



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

            //AGGIUNTA DI MARCO
            enemyLinker = movingObjTemp.GetComponent<Enemy_Controller>();
            //FINE AGGGIUNTA DI MARCO


            Enemies_Manager.instance.PreparingEnemies();
            //Initializing Enemy
          
        }



        
        public void ChangePhase(GAME_PHASE passedPhase)
        {
            Enemies_Manager.instance.CheckingAggro();

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

    }
}