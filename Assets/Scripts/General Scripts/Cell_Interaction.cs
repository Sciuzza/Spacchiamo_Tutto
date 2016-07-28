using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Spacchiamo
{
    public class Cell_Interaction : MonoBehaviour
    {
        [HideInInspector]
        public int xCell, yCell;

        // Needed for A Star Algorithm
        public Cell_Interaction parentNode;
        public int hValue, gValue, fValue;

        // A Star for all enemies 
        public List<int> hValueL, gValueL, fValueL;
        public List<Cell_Interaction> parentNodeL;

        // For Wall and Moving Objects
        public bool isOccupied = false;
        public bool isExit = false;

        public GameObject tileCell;

        public SpriteRenderer faloAlpha, exitAlpha;

        GameObject playerLink;

        public Color oriColor, stdHighColor;


        // For Falò 
        public bool lightSource = false;
        public bool isReceivingLight = false;
        public bool couldReceiveLight = false;
        public bool lightSourceDiscovered = false;

        private bool mouseEnter = false;
        public bool inRange = false;

        private bool faloLightRefreshed = false;

        void Update()
        {
            if (lightSourceDiscovered)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                    Grid_Manager.instance.GettingLightObject(xCell, yCell);
                if (Game_Controller.instance.currentPhase == GAME_PHASE.npcEnemyTurn && !faloLightRefreshed)
                {
                    Grid_Manager.instance.GettingLightObject(xCell, yCell);
                    faloLightRefreshed = true;
                }
                if (Game_Controller.instance.currentPhase == GAME_PHASE.playerTurn && faloLightRefreshed)
                    faloLightRefreshed = false;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
                mouseEnter = false;
        }

        void OnMouseOver()
        {
            if (!mouseEnter)
            {
                Player_Controller playerContLink = playerLink.GetComponent<Player_Controller>();
                playerActions playerActionsLink = playerLink.GetComponent<playerActions>();

                if (playerContLink.attackSelection && inRange)
                {
                    if (xCell - playerActionsLink.xPlayer >= 0 && playerContLink.IsFlipped())
                        playerContLink.FlippingPlayer();
                    else if (xCell - playerActionsLink.xPlayer < 0 && !playerContLink.IsFlipped())
                        playerContLink.FlippingPlayer();


                    if (playerContLink.firstAbilityPressed)
                        Grid_Manager.instance.HighlightingAreaOfEffect(xCell, yCell, playerContLink.ActAbilities[0].areaEffect);
                    else
                        Grid_Manager.instance.HighlightingAreaOfEffect(xCell, yCell, playerContLink.ActAbilities[1].areaEffect);

                    mouseEnter = true;
                }
            }
            /*
            else if (this.GetComponent<SpriteRenderer>().color.a == 1)
            {
                this.GetComponent<SpriteRenderer>().color = Color.grey;
                tileCell.GetComponent<SpriteRenderer>().color = Color.grey;
            }
            */
        }

        void OnMouseExit()
        {
            if (mouseEnter)
            {
                Player_Controller playerContLink = playerLink.GetComponent<Player_Controller>();

                if (playerContLink.attackSelection)
                {

                    if (playerContLink.firstAbilityPressed)
                        Grid_Manager.instance.DelightingAreaOfEffect(xCell, yCell, playerContLink.ActAbilities[0].areaEffect);
                    else
                        Grid_Manager.instance.DelightingAreaOfEffect(xCell, yCell, playerContLink.ActAbilities[1].areaEffect);


                    /*
                    else if (this.GetComponent<SpriteRenderer>().color.a == 1)
                    {
                        this.GetComponent<SpriteRenderer>().color = Color.white;
                        tileCell.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    */
                }
                mouseEnter = false;
            }
        }

        void OnMouseDown()
        {

            Player_Controller playerContLink = playerLink.GetComponent<Player_Controller>();
           

            if (playerContLink.attackSelection && inRange)
            {
                playerLink.GetComponent<playerActions>().IncreasingFearAndTurn();
                Enemies_Manager.instance.CheckingAggro();

                Game_Controller.instance.currentPhase = GAME_PHASE.knockAni;
                Game_Controller.instance.previousPhase = GAME_PHASE.playerTurn;



                if (playerContLink.firstAbilityPressed)
                {
                    Enemies_Manager.instance.AttackingEnemies(Grid_Manager.instance.GettingCellsAttacked(), playerContLink.ActAbilities[0].damage, playerContLink.ActAbilities[0].knockBack);
                    playerContLink.ResetAbilityCounter(0);
                }
                else
                {
                    Enemies_Manager.instance.AttackingEnemies(Grid_Manager.instance.GettingCellsAttacked(), playerContLink.ActAbilities[1].damage, playerContLink.ActAbilities[1].knockBack);
                    playerContLink.ResetAbilityCounter(1);
                }
                playerContLink.ResetAttackBooleans();
                mouseEnter = false;
            }
            else
                Debug.Log("Out of Range");

        }



        public void SettingFalo()
        {
            lightSource = true;
            isOccupied = true;
            Grid_Manager.instance.SettingCouldReceiveLightCells(xCell, yCell);

        }

        public void SettingWall()
        {
            this.isOccupied = true;
        }


        public void GivingPlayerRef(GameObject player)
        {
            playerLink = player;
        }




    }
}