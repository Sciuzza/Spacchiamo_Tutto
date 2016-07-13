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
            Game_Controller.instance.GivingPlayerRef(this.gameObject);
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
                    Grid_Manager.instance.HighlightingAttackRange(moveLink.GettingXPlayer(),moveLink.GettingyPlayer());
                }
                if (Input.GetKeyDown(KeyCode.Escape) && attackSelection)
                {
                    attackSelection = false;
                    Grid_Manager.instance.DelightingAttackRange(moveLink.GettingXPlayer(), moveLink.GettingyPlayer());
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
