using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class PMovement : MonoBehaviour
    {

        public enum NUM : byte { ZERO, FOUR = 4 };
        public enum BUTTON : byte { UP, DOWN, LEFT, RIGHT };
        KeyCode[] playerInputKey;
        

        Vector2 distance = new Vector2(-100, -100), direction;

        bool isMoving = false;
        public int whereI, whereJ;
        private Transform whereToGo = null;

        private void Awake()
        {


            this.playerInputKey = new KeyCode[(int)NUM.FOUR];
            this.playerInputKey[(int)BUTTON.UP] = KeyCode.W;
            this.playerInputKey[(int)BUTTON.DOWN] = KeyCode.S;
            this.playerInputKey[(int)BUTTON.LEFT] = KeyCode.A;
            this.playerInputKey[(int)BUTTON.RIGHT] = KeyCode.D;

            
        }


        public void Update()
        {


            //Calcoli matematici per spostarsi dalla propria cella a quella voluta, sempre se sussista quella voluta

            if (isMoving)
            {

                this.distance = whereToGo.position - this.transform.position;
                this.direction = this.distance.normalized;
                this.transform.position = (Vector2)this.transform.position + this.direction * Time.deltaTime;

                if (this.distance.sqrMagnitude < 0.01f)
                {
                    this.transform.position = new Vector3(whereToGo.position.x, whereToGo.position.y, 0);
                    whereToGo = null;
                    isMoving = false;
                }
            }
            else
            {
                if (Game_Controller.instance.currentPhase == Game_Controller.GAME_PHASE.playerTurn)
                {
                    if (Input.GetKey(this.playerInputKey[(int)BUTTON.UP]))
                    {
                        whereToGo = Grid_Manager.instance.CheckingUpCell(whereI, whereJ);
                        if (whereToGo != null)
                        {
                            isMoving = true;
                            whereI++;
                        }
                        else
                            Debug.Log("Sound Here");
                    }
                    else if (Input.GetKey(this.playerInputKey[(int)BUTTON.DOWN]))
                    {
                        whereToGo = Grid_Manager.instance.CheckingDownCell(whereI, whereJ);
                        if (whereToGo != null)
                        {
                            isMoving = true;
                            whereI--;
                        }
                        else
                            Debug.Log("Sound Here");
                    }
                    else if (Input.GetKey(this.playerInputKey[(int)BUTTON.LEFT]))
                    {
                        whereToGo = Grid_Manager.instance.CheckingLeftCell(whereI, whereJ);
                        if (whereToGo != null)
                        {
                            isMoving = true;
                            whereJ--;
                        }
                        else
                            Debug.Log("Sound Here");
                    }
                    else if (Input.GetKey(this.playerInputKey[(int)BUTTON.RIGHT]))
                    {
                        whereToGo = Grid_Manager.instance.CheckingRightCell(whereI, whereJ);
                        if (whereToGo != null)
                        {
                            isMoving = true;
                            whereJ++;
                        }
                        else
                            Debug.Log("Sound Here");
                    }

                }
            }
        }

    }

}



