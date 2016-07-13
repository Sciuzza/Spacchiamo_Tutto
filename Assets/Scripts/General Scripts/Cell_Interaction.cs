using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class Cell_Interaction : MonoBehaviour
    {

        public int xCell, yCell;

        // For Wall and Moving Objects
        public bool isOccupied = false;

        public GameObject tileCell;

        


        // for Aggro
        public bool aggroCell = false;

        // For Falò 
        public bool lightSource = false;
        public bool isReceivingLight = false;
        public bool lightSourceDiscovered = false;



       


        void OnMouseDown()
        {

            Player_Controller playerLink = Game_Controller.instance.TakingPlayerRef().GetComponent<Player_Controller>();

            if (Game_Controller.instance.currentPhase == Game_Controller.GAME_PHASE.playerTurn && playerLink.attackSelection)
            {
                if (Enemies_Manager.instance.EnemyIsHere(xCell, yCell))
                    Enemies_Manager.instance.DestroyEnemy(xCell, yCell);

                Game_Controller.instance.ChangePhase(Game_Controller.GAME_PHASE.playerTurn);
            }
        }

     

        public void SettingFalo()
        {
            lightSource = true;
            isOccupied = true;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            
        }

        public void SettingWall()
        {
            isOccupied = true;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;            
        }

        public void CellFree()
        {
            isOccupied = false;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            //Grid_Manager.instance.AddingPosition(this);
           
        }

       
    }
}