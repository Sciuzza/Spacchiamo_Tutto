using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class Designer_Tweaks : MonoBehaviour
    {

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