using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class Permanent : MonoBehaviour
    {


        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }


    }
}
