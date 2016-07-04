using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class Enemy_Controller : MonoBehaviour
    {

        Enemy_Patrolling patrolLink;
        

        void Awake()
        {
            patrolLink = GetComponent<Enemy_Patrolling>();

        }


        // Use this for initialization
        void Start()
        {
            Transform EnemyStartPosition = null;
            // Initializing Enemy Position
            do
            {
                EnemyStartPosition = Grid_Manager.instance.SettingEnemyPosition();
            } while (EnemyStartPosition == null);

            float x = EnemyStartPosition.position.x;
            float y = EnemyStartPosition.position.y;
            float z = this.transform.position.z;


            this.transform.position = new Vector3(x, y, z);


        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}