using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class PMovement : MonoBehaviour
    {

        Vector2 distance = new Vector2(-100, -100), direction;

        private bool isMoving = false;
        private int whereI, whereJ;
        private Transform whereToGo = null;
        

        private Player_Controller pControllerLink;

        private void Awake()
        {
            pControllerLink = this.GetComponent<Player_Controller>();

            whereI = Game_Controller.instance.GettingRowStartPosition();
            whereJ = Game_Controller.instance.GettingColumnStartPosition();
        }


        public void Update()
        {


            //Calcoli matematici per spostarsi dalla propria cella a quella voluta, sempre se sussista quella voluta

            if (isMoving)
            {

                this.distance = whereToGo.position - this.transform.position;
                this.direction = this.distance.normalized;
                this.transform.position = (Vector2)this.transform.position + this.direction * Time.deltaTime * Designer_Tweaks.instance.moveSpeed;

                if (this.distance.sqrMagnitude < 0.001f)
                {
                    this.transform.position = new Vector3(whereToGo.position.x, whereToGo.position.y, 0);
                    whereToGo = null;
                    isMoving = false;
                    //Getting the light around the player
                    Grid_Manager.instance.GettingLight(whereI, whereJ);
                    
                    //Increasing Fear Bar and Fear Value by 1
                    if (!Grid_Manager.instance.IsCellReceivingLight(whereI, whereJ))
                    {
                        Ui_Manager.instance.IncreasingFearBar();
                        Ui_Manager.instance.SettingFearValue();
                    }
                    // Resetting Fear Bar and Value to 0
                    else
                    {
                        Ui_Manager.instance.ResettingFearBar();
                        Ui_Manager.instance.SettingFearValue();
                    }
                    //Increasing Turn Counter
                    Ui_Manager.instance.IncreasingTurnCount();
                }
            }
            else
            {
                if (Game_Controller.instance.currentPhase == Game_Controller.GAME_PHASE.playerTurn)
                {
					if (Input.GetKey(KeyCode.W))
                    {
                        whereToGo = Grid_Manager.instance.CheckingUpCell(whereI, whereJ);
                        if (whereToGo != null)
                        {
                            isMoving = true;
                            whereI++;
                        }
                        else
                            Debug.Log("Sound Here");
						//AGGIUNTA DI MARCO
						Game_Controller.instance.ChangePhase (Game_Controller.instance.currentPhase);
						//FINE AGGGIUNTA DI MARCO
                    }
					else if (Input.GetKey(KeyCode.S))
                    {
                        whereToGo = Grid_Manager.instance.CheckingDownCell(whereI, whereJ);
                        if (whereToGo != null)
                        {
                            isMoving = true;
                            whereI--;
                        }
                        else
                            Debug.Log("Sound Here");
						//AGGIUNTA DI MARCO
						Game_Controller.instance.ChangePhase (Game_Controller.instance.currentPhase);
						//FINE AGGGIUNTA DI MARCO
                    }
					else if (Input.GetKey(KeyCode.A))
                    {
                        whereToGo = Grid_Manager.instance.CheckingLeftCell(whereI, whereJ);
                        if (whereToGo != null)
                        {
                            isMoving = true;
                            whereJ--;
                            if (!pControllerLink.IsFlipped())
                                pControllerLink.FlippingPlayer();
                        }
                        else
                            Debug.Log("Sound Here");
						//AGGIUNTA DI MARCO
						Game_Controller.instance.ChangePhase (Game_Controller.instance.currentPhase);
						//FINE AGGGIUNTA DI MARCO
                    }
					else if (Input.GetKey(KeyCode.D))
                    {
                        whereToGo = Grid_Manager.instance.CheckingRightCell(whereI, whereJ);
                        if (whereToGo != null)
                        {
                            isMoving = true;
                            whereJ++;
                            if (pControllerLink.IsFlipped())
                                pControllerLink.FlippingPlayer();
                        }
                        else
                            Debug.Log("Sound Here");
						//AGGIUNTA DI MARCO
						Game_Controller.instance.ChangePhase (Game_Controller.instance.currentPhase);
						//FINE AGGGIUNTA DI MARCO
                    }

                }
            }
        }


        public int GettingRow()
        {
            return whereI;
        }

        public int GettingColumn()
        {
            return whereJ;
        }
    }

}



