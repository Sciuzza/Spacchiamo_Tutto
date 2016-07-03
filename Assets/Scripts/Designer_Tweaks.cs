using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class Designer_Tweaks : MonoBehaviour
    {
        //General Designer Variables
        [Range(1,5)]
        public int manhDistance;

        [Range(1, 5)]
        public int moveSpeed;

        // Level 1 designer variables
        [Range(5,50)]
        public int level1Rows, level1Columns;

        [HideInInspector]
        public static Designer_Tweaks instance = null;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

    }
}