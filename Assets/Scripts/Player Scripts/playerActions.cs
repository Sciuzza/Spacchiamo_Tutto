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

                    this.GetComponent<SpriteRenderer>().sortingOrder = Designer_Tweaks.instance.Level1YWidth - yPlayer;
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
            if (pControllerLink.CurSet.FearValue < 20)
            {
                pControllerLink.CurSet.fear1Activated = false;
                pControllerLink.CurSet.fear2Activated = false;
                pControllerLink.CurSet.fear1Percent = 0.15f;
                pControllerLink.CurSet.fear2Percent = 0.15f;
            }
            else if (pControllerLink.CurSet.FearValue >= 20 && pControllerLink.CurSet.FearValue < 22)
            {
                if (!pControllerLink.CurSet.fear1Activated)
                    pControllerLink.CurSet.fear1Activated = true;
            }
            else if (pControllerLink.CurSet.FearValue >= 22 && pControllerLink.CurSet.FearValue < 24)
            {
                pControllerLink.CurSet.fear1Percent = 0.20f;
            }
            else if (pControllerLink.CurSet.FearValue >= 24 && pControllerLink.CurSet.FearValue < 26)
            {
                pControllerLink.CurSet.fear1Percent = 0.25f;
            }
            else if (pControllerLink.CurSet.FearValue >= 26 && pControllerLink.CurSet.FearValue < 28)
            {
                pControllerLink.CurSet.fear1Percent = 0.30f;
            }
            else if (pControllerLink.CurSet.FearValue >= 28 && pControllerLink.CurSet.FearValue < 30)
            {
                pControllerLink.CurSet.fear1Percent = 0.35f;
            }
            else if (pControllerLink.CurSet.FearValue >= 30 && pControllerLink.CurSet.FearValue < 32)
            {
                pControllerLink.CurSet.fear1Activated = false;
                pControllerLink.CurSet.fear2Activated = true;
            }
            else if (pControllerLink.CurSet.FearValue >= 32 && pControllerLink.CurSet.FearValue < 34)
            {
                pControllerLink.CurSet.fear2Percent = 0.20f;
            }
            else if (pControllerLink.CurSet.FearValue >= 34 && pControllerLink.CurSet.FearValue < 36)
            {
                pControllerLink.CurSet.fear2Percent = 0.25f;
            }
            else if (pControllerLink.CurSet.FearValue >= 36 && pControllerLink.CurSet.FearValue < 38)
            {
                pControllerLink.CurSet.fear2Percent = 0.30f;
            }
            else if (pControllerLink.CurSet.FearValue >= 38 && pControllerLink.CurSet.FearValue < 40)
            {
                pControllerLink.CurSet.fear2Percent = 0.35f;
            }
            else if (pControllerLink.CurSet.FearValue == 40)
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
                if (++pControllerLink.CurSet.fearTurnCounter % Designer_Tweaks.instance.fearScaleRate == 0)
                {
                    pControllerLink.CurSet.FearValue++;
                    Ui_Manager.instance.SettingFearValue(pControllerLink.CurSet.FearValue);

                    FearManager();
                }
            }

            // Resetting Fear Bar and Value to 0
            else
            {
                pControllerLink.CurSet.fearTurnCounter = 0;
                pControllerLink.CurSet.FearValue = 0;
                pControllerLink.CurSet.fear1Activated = false;
                pControllerLink.CurSet.fear2Activated = false;
                pControllerLink.CurSet.fear1Percent = 0.15f;
                pControllerLink.CurSet.fear2Percent = 0.15f;
                Ui_Manager.instance.SettingFearValue(pControllerLink.CurSet.FearValue);
            }
            //Increasing Turn Counter
            pControllerLink.CurSet.TurnValue++;

            if (pControllerLink.RegPassive.active)
            {
                RegPassiveApplying();
            }
            Enemies_Manager.instance.SettingEnemyVisibility(0);
            Ui_Manager.instance.SettingTurnValue(pControllerLink.CurSet.TurnValue);
        }

        private void RegPassiveApplying()
        {
            if (pControllerLink.CurSet.passiveStorage.regeneration.regCounter == pControllerLink.RegPassive.cooldown)
            {
                if (pControllerLink.CurSet.Life < pControllerLink.CurSet.maxLife)
                {
                    pControllerLink.CurSet.Life += pControllerLink.RegPassive.regPower;

                    if (pControllerLink.CurSet.Life > pControllerLink.CurSet.maxLife)
                        pControllerLink.CurSet.Life = pControllerLink.CurSet.maxLife;

                    Ui_Manager.instance.SettingLife((int)pControllerLink.CurSet.Life);
                    pControllerLink.CurSet.passiveStorage.regeneration.regCounter = 0;
                }
            }
            else
            {
                pControllerLink.CurSet.passiveStorage.regeneration.regCounter++;
            }
        }

        public void InitializeSortingOrder()
        {
            this.GetComponent<SpriteRenderer>().sortingOrder = Designer_Tweaks.instance.Level1YWidth - yPlayer;
        }

    }

}