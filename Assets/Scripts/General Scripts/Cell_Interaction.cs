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
            
            int cellType = Random.Range(0, 50);

            switch (cellType)
            {
                case 0:
                    if (!isOccupied)
                    {
                        lightSource = true;
                        isOccupied = true;
                        this.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                        Grid_Manager.instance.ChangingAlpha(0.0f, this.gameObject);
                    }
                    break;
                case 1:
                    if (!isOccupied)
                    {
                        isOccupied = true;
                        this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                        Grid_Manager.instance.ChangingAlpha(0.0f, this.gameObject);
                    }
                    break;
                default:
                    if(!isOccupied)
                    Grid_Manager.instance.AddingPosition(this);
                    break;
            }
            

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
            Grid_Manager.instance.ChangingAlpha(0.0f, this.gameObject);
            Grid_Manager.instance.ChangingAlpha(0.0f, tileCell);
        }

        public void SettingWall()
        {
            isOccupied = true;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            Grid_Manager.instance.ChangingAlpha(0.0f, this.gameObject);
            Grid_Manager.instance.ChangingAlpha(0.0f, tileCell);
        }

        public void TemporaryRandom()
        {
            Grid_Manager.instance.AddingPosition(this);
        }

        public void SettingInviWall()
        {
            isOccupied = true;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            Grid_Manager.instance.ChangingAlpha(0.0f, this.gameObject);
        }
    }
}