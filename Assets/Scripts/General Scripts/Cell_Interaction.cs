using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class Cell_Interaction : MonoBehaviour
    {
        [HideInInspector]
        public int xCell, yCell;

        // For Wall and Moving Objects
        public bool isOccupied = false;

        public GameObject tileCell;

        public SpriteRenderer faloAlpha;

        


        // for Aggro
        public bool aggroCell = false;

        // For Falò 
        public bool lightSource = false;
        public bool isReceivingLight = false;
        public bool lightSourceDiscovered = false;



       


        void OnMouseDown()
        {

            Player_Controller playerLink = Game_Controller.instance.TakingPlayerRef().GetComponent<Player_Controller>();
            if (this.GetComponent<SpriteRenderer>().color == Color.yellow)
                playerLink.Attack(xCell, yCell);
            else
                Debug.Log("Out of Range");
           
        }

     

        public void SettingFalo()
        {
            lightSource = true;
            isOccupied = true;
            
        }

        public void SettingWall()
        {
            this.isOccupied = true;        
        }

       

       
    }
}