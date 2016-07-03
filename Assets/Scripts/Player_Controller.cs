using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class Player_Controller : MonoBehaviour
    {

        PMovement moveLink;
                     
        void Awake()
        {

            Game_Controller.instance.InitializingPlayer();
            moveLink = GetComponent<PMovement>();
        }

      void Start()
        {
            Transform playerStartPosition = Grid_Manager.instance.SettingPlayerPosition(moveLink.whereI, moveLink.whereJ);

            float x = playerStartPosition.position.x;
            float y = playerStartPosition.position.y;
            float z = this.transform.position.z;


            this.transform.position = new Vector3(x, y, z);
        }
        
      
    }
}
