using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class PMovement : MonoBehaviour
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
                this.transform.position = (Vector2)this.transform.position + this.direction * Time.deltaTime * Designer_Tweaks.instance.moveSpeed;

                if (this.distance.sqrMagnitude < 0.005f)
                {
                    this.transform.position = new Vector3(whereToGo.position.x, whereToGo.position.y, 0);
                    //whereToGo = null;
                    isMoving = false;
                    //Getting the light around the player
                    Grid_Manager.instance.GettingLight(xPlayer, yPlayer);

                    //Increasing Fear Bar and Fear Value by 1
                    if (!Grid_Manager.instance.IsCellReceivingLight(xPlayer, yPlayer))
                    {
                        if (++pControllerLink.fearTurnCounter % Designer_Tweaks.instance.fearScaleRate == 0)
                        {
                            pControllerLink.FearValue++;
                            Ui_Manager.instance.SettingFearValue(pControllerLink.FearValue);
                        }
                    }

                    // Resetting Fear Bar and Value to 0
                    else
                    {
                        pControllerLink.fearTurnCounter = 0;
                        pControllerLink.FearValue = 0;
                        Ui_Manager.instance.SettingFearValue(pControllerLink.FearValue);
                        Enemies_Manager.instance.ClearAggro();
                    }
                    //Increasing Turn Counter
                    pControllerLink.TurnValue++;
                    Ui_Manager.instance.SettingTurnValue(pControllerLink.TurnValue);
                }
            }
            else
            {
                if (Game_Controller.instance.currentPhase == Game_Controller.GAME_PHASE.playerTurn && !pControllerLink.attackSelection)
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        whereToGo = Grid_Manager.instance.CheckingUpCell(xPlayer, yPlayer);
                        if (whereToGo != null)
                        {
                            yPlayer++;
                            isMoving = true;
                            Game_Controller.instance.ChangePhase(Game_Controller.instance.currentPhase);
                        }
                        else
                            Debug.Log("Sound Here");
                        
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        whereToGo = Grid_Manager.instance.CheckingDownCell(xPlayer, yPlayer);
                        if (whereToGo != null)
                        {
                            yPlayer--;
                            isMoving = true;
                            Game_Controller.instance.ChangePhase(Game_Controller.instance.currentPhase);
                        }
                        else
                            Debug.Log("Sound Here");
                        
                    }
                    else if (Input.GetKey(KeyCode.A))
                    {
                        whereToGo = Grid_Manager.instance.CheckingLeftCell(xPlayer, yPlayer);
                        if (whereToGo != null)
                        {
                            xPlayer--;
                            isMoving = true;
                            Game_Controller.instance.ChangePhase(Game_Controller.instance.currentPhase);
                            if (!pControllerLink.IsFlipped())
                                pControllerLink.FlippingPlayer();
                        }
                        else
                            Debug.Log("Sound Here");
                        
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        whereToGo = Grid_Manager.instance.CheckingRightCell(xPlayer, yPlayer);
                        if (whereToGo != null)
                        {
                            xPlayer++;
                            isMoving = true;
                            Game_Controller.instance.ChangePhase(Game_Controller.instance.currentPhase);
                            if (pControllerLink.IsFlipped())
                                pControllerLink.FlippingPlayer();
                        }
                        else
                            Debug.Log("Sound Here");
                        
                    }

                }
            }
        }


        public int GettingXPlayer()
        {
            return xPlayer;
        }

        public int GettingyPlayer()
        {
            return yPlayer;
        }
    }

}