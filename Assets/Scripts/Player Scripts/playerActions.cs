using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class playerActions : MonoBehaviour
    {

        Vector2 distance = new Vector2(-100, -100), direction;

        private bool isMoving = false;
        public int xPlayer, yPlayer;
        public Transform whereToGo = null;


        private Player_Controller pControllerLink;

        private void Awake()
        {
            pControllerLink = this.GetComponent<Player_Controller>();
            xPlayer = Mathf.FloorToInt(this.transform.position.x);
            yPlayer = Mathf.FloorToInt(this.transform.position.y);
            this.GetComponent<SpriteRenderer>().sortingOrder = Designer_Tweaks.instance.level1yWidth - yPlayer;        
        }

        

        public void Update()
        {


            //Calcoli matematici per spostarsi dalla propria cella a quella voluta, sempre se sussista quella voluta

            if (isMoving)
            {

                this.distance = whereToGo.position - this.transform.position;
                this.direction = this.distance.normalized;
                this.transform.position = (Vector2)this.transform.position + this.direction * Time.deltaTime * Designer_Tweaks.instance.generalMoveSpeed;

                if (this.distance.sqrMagnitude < 0.005f)
                {
                    

                    this.transform.position = new Vector3(whereToGo.position.x, whereToGo.position.y, 0);
                    //whereToGo = null;
                    isMoving = false;
                    //Getting the light around the player
                    Grid_Manager.instance.GettingLight(xPlayer, yPlayer);

                    this.GetComponent<SpriteRenderer>().sortingOrder = Designer_Tweaks.instance.level1yWidth - yPlayer;
                    //Increasing Fear Bar and Fear Value by 1
                    IncreasingFearAndTurn();
                }
            }
            else
            {
                if (Game_Controller.instance.currentPhase == GAME_PHASE.playerTurn && !pControllerLink.attackSelection)
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        whereToGo = Grid_Manager.instance.CheckingUpCell(xPlayer, yPlayer);
                        if (whereToGo != null)
                        {
                            yPlayer++;
                            isMoving = true;
                            Enemies_Manager.instance.CheckingAggro();
                            Game_Controller.instance.currentPhase = GAME_PHASE.npcEnemyTurn;
                        }
                        
                        
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        whereToGo = Grid_Manager.instance.CheckingDownCell(xPlayer, yPlayer);
                        if (whereToGo != null)
                        {
                            yPlayer--;
                            isMoving = true;
                            Enemies_Manager.instance.CheckingAggro();
                            Game_Controller.instance.currentPhase = GAME_PHASE.npcEnemyTurn;
                        }
                       
                        
                    }
                    else if (Input.GetKey(KeyCode.A))
                    {
                        whereToGo = Grid_Manager.instance.CheckingLeftCell(xPlayer, yPlayer);
                        if (whereToGo != null)
                        {
                            xPlayer--;
                            isMoving = true;
                            Enemies_Manager.instance.CheckingAggro();
                            Game_Controller.instance.currentPhase = GAME_PHASE.npcEnemyTurn;
                            if (!pControllerLink.IsFlipped())
                                pControllerLink.FlippingPlayer();
                        }
                        
                        
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        whereToGo = Grid_Manager.instance.CheckingRightCell(xPlayer, yPlayer);
                        if (whereToGo != null)
                        {
                            xPlayer++;
                            isMoving = true;
                            Enemies_Manager.instance.CheckingAggro();
                            Game_Controller.instance.currentPhase = GAME_PHASE.npcEnemyTurn;
                            if (pControllerLink.IsFlipped())
                                pControllerLink.FlippingPlayer();
                        }
                        
                        
                    }

                }
            }
        }

        public void FearManager()
        {
            if (pControllerLink.FearValue >= 20 && pControllerLink.FearValue < 22)
            {
                if (!pControllerLink.fear1Activated)
                    pControllerLink.fear1Activated = true;
            }
            else if (pControllerLink.FearValue >= 22 && pControllerLink.FearValue < 24)
            {
                pControllerLink.fear1Percent = 0.20f;
            }
            else if (pControllerLink.FearValue >= 24 && pControllerLink.FearValue < 26)
            {
                pControllerLink.fear1Percent = 0.25f;
            }
            else if (pControllerLink.FearValue >= 26 && pControllerLink.FearValue < 28)
            {
                pControllerLink.fear1Percent = 0.30f;
            }
            else if (pControllerLink.FearValue >= 28 && pControllerLink.FearValue < 30)
            {
                pControllerLink.fear1Percent = 0.35f;
            }
            else if (pControllerLink.FearValue >= 30 && pControllerLink.FearValue < 32)
            {
                pControllerLink.fear1Activated = false;
                pControllerLink.fear2Activated = true;
            }
            else if (pControllerLink.FearValue >= 32 && pControllerLink.FearValue < 34)
            {
                pControllerLink.fear2Percent = 0.20f;
            }
            else if (pControllerLink.FearValue >= 34 && pControllerLink.FearValue < 36)
            {
                pControllerLink.fear2Percent = 0.25f;
            }
            else if (pControllerLink.FearValue >= 36 && pControllerLink.FearValue < 38)
            {
                pControllerLink.fear2Percent = 0.30f;
            }
            else if (pControllerLink.FearValue >= 38 && pControllerLink.FearValue < 40)
            {
                pControllerLink.fear2Percent = 0.35f;
            }
            else if (pControllerLink.FearValue == 40)
                pControllerLink.KillingPlayer();
        }

        public int GettingXPlayer()
        {
            return xPlayer;
        }

        public int GettingyPlayer()
        {
            return yPlayer;
        }

        public void IncreasingFearAndTurn()
        {
            if (!Grid_Manager.instance.IsCellReceivingLight(xPlayer, yPlayer))
            {
                if (++pControllerLink.fearTurnCounter % Designer_Tweaks.instance.fearScaleRate == 0)
                {
                    pControllerLink.FearValue++;
                    Ui_Manager.instance.SettingFearValue(pControllerLink.FearValue);

                    FearManager();
                }
            }

            // Resetting Fear Bar and Value to 0
            else
            {
                pControllerLink.fearTurnCounter = 0;
                pControllerLink.FearValue = 0;
                pControllerLink.fear1Activated = false;
                pControllerLink.fear2Activated = false;
                pControllerLink.fear1Percent = 0.15f;
                pControllerLink.fear2Percent = 0.15f;
                Ui_Manager.instance.SettingFearValue(pControllerLink.FearValue);
            }
            //Increasing Turn Counter
            pControllerLink.TurnValue++;
            Enemies_Manager.instance.SettingEnemyVisibility();
            Ui_Manager.instance.SettingTurnValue(pControllerLink.TurnValue);
        }

    }

}