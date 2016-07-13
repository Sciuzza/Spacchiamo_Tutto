using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class Cell_Interaction : MonoBehaviour
    {

        public int cell_i, cell_j;

        // For Wall and Moving Objects
        public bool isOccupied = false;

        public GameObject tileCell;

        Player_Controller playerLink;


        // for Aggro
        public bool aggroCell = false;

        // For Falò 
        public bool lightSource = false;
        public bool isReceivingLight = false;
        public bool lightSourceDiscovered = false;



        void Start()
        {

            playerLink = GameObject.Find("Player(Clone)").GetComponent<Player_Controller>();
        }


        void OnMouseDown()
        {
          
            if (Game_Controller.instance.currentPhase == Game_Controller.GAME_PHASE.playerTurn && playerLink.attackSelection)
            {
                if (Enemies_Manager.instance.EnemyIsHere(cell_i, cell_j))
                    Enemies_Manager.instance.DestroyEnemy(cell_i, cell_j);

                Game_Controller.instance.ChangePhase(Game_Controller.GAME_PHASE.playerTurn);
            }
        }

        void OnMouseOver()
        {

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
            Grid_Manager.instance.AddingPosition(this);
           
        }

       
    }
}