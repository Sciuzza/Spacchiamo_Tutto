using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class Player_Controller : MonoBehaviour
    {
        public int fearTurnCounter = 0;
        PMovement moveLink;

        void Awake()
        {
            moveLink = GetComponent<PMovement>();
        }

        void Start()
        {

            // Initializing Player Position
            Transform playerStartPosition = Grid_Manager.instance.SettingPlayerPosition(moveLink.GettingRow(), moveLink.GettingColumn());

            float x = playerStartPosition.position.x;
            float y = playerStartPosition.position.y;
            float z = this.transform.position.z;


            this.transform.position = new Vector3(x, y, z);

            this.gameObject.layer = moveLink.GettingRow() + 8;


            // Linking Camera Smooth Follow
            Game_Controller.instance.InitializingCamera(this.gameObject);

            // Initializing Light
            Grid_Manager.instance.GettingLight(moveLink.GettingRow(), moveLink.GettingColumn());
        }


        public bool IsFlipped()
        {
            return this.GetComponent<SpriteRenderer>().flipX;
        }

        public void FlippingPlayer()
        {
            SpriteRenderer flipSprite = this.GetComponent<SpriteRenderer>();

            if (flipSprite.flipX)
                flipSprite.flipX = false;
            else
                flipSprite.flipX = true;

        }

    }
}
