using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class Player_Controller : MonoBehaviour
    {
        public int fearTurnCounter = 0;
        PMovement moveLink;

        public int Life = 20;
        public int FearValue = 0;
        public int TurnValue = 0;

        public bool attackSelection = false;

        void Awake()
        {
            moveLink = GetComponent<PMovement>();
        }

        void Start()
        {

            // Initializing Player Position
            Transform playerStartPosition = Grid_Manager.instance.SettingPlayerPosition(moveLink.GettingRow(), moveLink.GettingColumn());

            float x = playerStartPosition.position.x;
            float y = playerStartPosition.position.y;
            float z = this.transform.position.z;


            this.transform.position = new Vector3(x, y, z);

            


            // Linking Camera Smooth Follow
            Game_Controller.instance.InitializingCamera(this.gameObject);

            // Initializing Light
            Grid_Manager.instance.GettingLight(moveLink.GettingRow(), moveLink.GettingColumn());
        }


        void Update()
        {
            if (Game_Controller.instance.currentPhase == Game_Controller.GAME_PHASE.playerTurn)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    Game_Controller.instance.ChangePhase(Game_Controller.GAME_PHASE.playerTurn);
                if (Input.GetKeyUp(KeyCode.Q))
                {
                    attackSelection = true;
                    Grid_Manager.instance.HighlightingAttackRange(moveLink.GettingRow(),moveLink.GettingColumn());
                }
                if (Input.GetKeyDown(KeyCode.Escape) && attackSelection)
                {
                    attackSelection = false;
                    Grid_Manager.instance.DelightingAttackRange(moveLink.GettingRow(), moveLink.GettingColumn());
                }
                    
            }
        }


        public bool IsFlipped()
        {
            return this.GetComponent<SpriteRenderer>().flipX;
        }

        public void FlippingPlayer()
        {
            SpriteRenderer flipSprite = this.GetComponent<SpriteRenderer>();

            if (flipSprite.flipX)
                flipSprite.flipX = false;
            else
                flipSprite.flipX = true;

        }

    }
}
