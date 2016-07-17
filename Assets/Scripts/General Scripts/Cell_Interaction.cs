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

        GameObject playerLink;

        public Color previousColor;

        // for Aggro
        public bool aggroCell = false;

        // For Falò 
        public bool lightSource = false;
        public bool isReceivingLight = false;
        public bool lightSourceDiscovered = false;


        

        void OnMouseEnter()
        {
            Player_Controller playerContLink = playerLink.GetComponent<Player_Controller>();

            if (this.GetComponent<SpriteRenderer>().color == Color.yellow)
            {
                if (playerContLink.firstAbilityPressed)
                {
                    if (playerContLink.actAbilities[0].areaEffect == 0)
                    {
                        this.GetComponent<SpriteRenderer>().color = Color.red;
                        tileCell.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    else
                    {
                        Grid_Manager.instance.HighlightingAreaOfEffect(xCell, yCell, playerContLink.actAbilities[0].areaEffect);
                    }
                }
                else
                {
                    if (playerContLink.actAbilities[1].areaEffect == 0)
                    {
                        this.GetComponent<SpriteRenderer>().color = Color.red;
                        tileCell.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    else
                    {
                        Grid_Manager.instance.HighlightingAreaOfEffect(xCell, yCell, playerContLink.actAbilities[1].areaEffect);
                    }
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
            Player_Controller playerContLink = playerLink.GetComponent<Player_Controller>();

            if (this.GetComponent<SpriteRenderer>().color == Color.red)
            {
                if (playerContLink.firstAbilityPressed)
                {
                    if (playerContLink.actAbilities[0].areaEffect == 0)
                    {
                        this.GetComponent<SpriteRenderer>().color = Color.yellow;
                        tileCell.GetComponent<SpriteRenderer>().color = Color.yellow;
                    }
                    else
                    {
                        Grid_Manager.instance.DelightingAreaOfEffect(xCell, yCell, playerContLink.actAbilities[0].areaEffect);
                    }
                }
                else
                {
                    if (playerContLink.actAbilities[1].areaEffect == 0)
                    {
                        this.GetComponent<SpriteRenderer>().color = Color.yellow;
                        tileCell.GetComponent<SpriteRenderer>().color = Color.yellow;
                    }
                    else
                    {
                        Grid_Manager.instance.DelightingAreaOfEffect(xCell, yCell, playerContLink.actAbilities[1].areaEffect);
                    }
                }
                /*
                else if (this.GetComponent<SpriteRenderer>().color.a == 1)
                {
                    this.GetComponent<SpriteRenderer>().color = Color.white;
                    tileCell.GetComponent<SpriteRenderer>().color = Color.white;
                }
                */
            }
        }
       


        void OnMouseDown()
        {

            Player_Controller playerContLink = playerLink.GetComponent<Player_Controller>();
            if (this.GetComponent<SpriteRenderer>().color == Color.red)
                playerContLink.Attack(xCell, yCell);
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

       
        public void GivingPlayerRef(GameObject player)
        {
            playerLink = player;
        }
       
    }
}