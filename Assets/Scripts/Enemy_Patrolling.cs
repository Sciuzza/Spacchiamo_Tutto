using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class Enemy_Patrolling : MonoBehaviour
    {

        Vector2 distance = new Vector2(-100, -100), direction;

        private bool isMoving = false;
        private int whereI, whereJ;
        private Transform whereToGo = null;

        Enemy_Controller eControllerLink;

        void Awake()
        {

            eControllerLink = this.GetComponent<Enemy_Controller>();

            
        }


        public void SettingWhereI(int row)
        {
            whereI = row;
        }

        public void SettingWhereJ(int column)
        {
            whereJ = column;
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