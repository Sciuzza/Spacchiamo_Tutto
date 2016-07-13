using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class Designer_Tweaks : MonoBehaviour
    {
        //General Designer Variables
        [Range(1, 5)]
        public int manhDistancePlayer, manhDistanceFalo, moveSpeed, fearScaleRate, patrolAreaEnemy1;

        // Level 1 designer variables
        [Range(5, 100)]
        public int level1Rows, level1Columns;

        [Range(1, 30)]
        public int level1EnemiesQuantity;

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