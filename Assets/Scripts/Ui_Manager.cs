using UnityEngine;
using System.Collections;


namespace Spacchiamo
{
    public class Ui_Manager : MonoBehaviour
    {
        [HideInInspector]
        public static Ui_Manager instance = null;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }
    }
}