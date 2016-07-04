using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class Cell_Interaction : MonoBehaviour
    {

        public int cell_i, cell_j;

        // For Wall and Moving Objects
        public bool isOccupied = false;

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
                    lightSource = true;
                    isOccupied = true;
                    this.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                    Grid_Manager.instance.ChangingAlpha(0.0f, this.gameObject);
                    break;
                case 1:
                    isOccupied = true;
                    this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                    Grid_Manager.instance.ChangingAlpha(0.0f, this.gameObject);
                    break;
                default:
                    break;
            }

        }


    }
}