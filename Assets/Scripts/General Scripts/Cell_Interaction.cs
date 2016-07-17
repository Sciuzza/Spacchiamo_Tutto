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

        public Color oriColor, stdHighColor;

        // for Aggro
        public bool aggroCell = false;

        // For Falò 
        public bool lightSource = false;
        public bool isReceivingLight = false;
        public bool lightSourceDiscovered = false;




        void OnMouseEnter()
        {
            Debug.Log(this.GetComponent<SpriteRenderer>().color);

            Player_Controller playerContLink = playerLink.GetComponent<Player_Controller>();
            SpriteRenderer tileHighlight = tileCell.GetComponent<SpriteRenderer>();

            if (playerContLink.attackSelection)
            {
                if (playerContLink.firstAbilityPressed)
                {
                    if (playerContLink.actAbilities[0].areaEffect == 0)
                    {
                        float tempAlpha = Grid_Manager.instance.GettingAlpha(this.gameObject);
                        stdHighColor = this.GetComponent<SpriteRenderer>().color;

                        this.GetComponent<SpriteRenderer>().color = Color.red;
                        Grid_Manager.instance.ChangingAlpha(tempAlpha, this.gameObject);

                        tileHighlight.color = this.GetComponent<SpriteRenderer>().color;
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
                        float tempAlpha = Grid_Manager.instance.GettingAlpha(this.gameObject);
                        stdHighColor = this.GetComponent<SpriteRenderer>().color;

                        this.GetComponent<SpriteRenderer>().color = Color.red;
                        Grid_Manager.instance.ChangingAlpha(tempAlpha, this.gameObject);

                        tileHighlight.color = this.GetComponent<SpriteRenderer>().color;
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
            SpriteRenderer tileHighlight = tileCell.GetComponent<SpriteRenderer>();

            if (playerContLink.attackSelection)
            {
                if (playerContLink.firstAbilityPressed)
                {
                    if (playerContLink.actAbilities[0].areaEffect == 0)
                    {
                        this.GetComponent<SpriteRenderer>().color = stdHighColor;
                        tileHighlight.color = stdHighColor;
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
                        this.GetComponent<SpriteRenderer>().color = stdHighColor;
                        tileHighlight.color = stdHighColor;
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


        private bool IsColorYellow()
        {
            Color cellColor = this.GetComponent<SpriteRenderer>().color;

            if (cellColor == Game_Controller.instance.Yel1)
                return true;
            else if (cellColor == Game_Controller.instance.Yel08)
                return true;
            else if (cellColor == Game_Controller.instance.Yel05)
                return true;
            else if (cellColor == Game_Controller.instance.Yel00)
                return true;
            else
                return false;
        }

        private bool IsColorRed()
        {
            Color cellColor = this.GetComponent<SpriteRenderer>().color;

            if (cellColor == Game_Controller.instance.Red1)
                return true;
            else if (cellColor == Game_Controller.instance.Red08)
                return true;
            else if (cellColor == Game_Controller.instance.Red05)
                return true;
            else if (cellColor == Game_Controller.instance.Red00)
                return true;
            else
                return false;
        }

    }
}