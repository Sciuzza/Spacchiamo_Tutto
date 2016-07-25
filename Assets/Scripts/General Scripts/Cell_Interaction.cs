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

        public GameObject tileCell;

        public SpriteRenderer faloAlpha;

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
                if (Game_Controller.instance.currentPhase == GAME_PHASE.npcEnemyTurn && faloLightRefreshed)
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
               

                if (playerContLink.attackSelection && inRange)
                {
                    if (playerContLink.firstAbilityPressed)
                        Grid_Manager.instance.HighlightingAreaOfEffect(xCell, yCell, playerContLink.actAbilities[0].areaEffect);
                    else
                        Grid_Manager.instance.HighlightingAreaOfEffect(xCell, yCell, playerContLink.actAbilities[1].areaEffect);

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
                        Grid_Manager.instance.DelightingAreaOfEffect(xCell, yCell, playerContLink.actAbilities[0].areaEffect);
                    else
                        Grid_Manager.instance.DelightingAreaOfEffect(xCell, yCell, playerContLink.actAbilities[1].areaEffect);


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
                    Enemies_Manager.instance.AttackingEnemies(Grid_Manager.instance.GettingCellsAttacked(), playerContLink.actAbilities[0].damage, playerContLink.actAbilities[0].knockBack);
                }
                else
                    Enemies_Manager.instance.AttackingEnemies(Grid_Manager.instance.GettingCellsAttacked(), playerContLink.actAbilities[1].damage, playerContLink.actAbilities[1].knockBack);

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