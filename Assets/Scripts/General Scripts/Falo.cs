using UnityEngine;
using System.Collections;


namespace Spacchiamo
{
    public class Falo : MonoBehaviour
    {

        int faloX, faloY;

      void Awake()
        {
            faloX = (int)this.transform.position.x;
            faloY = (int)this.transform.position.y;


        }

        
    }

}