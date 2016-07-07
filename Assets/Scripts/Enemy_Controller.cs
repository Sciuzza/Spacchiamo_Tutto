using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class Enemy_Controller : MonoBehaviour
    {

        Enemy_Patrolling patrolLink;
        SpriteRenderer manageSprite;
        Sprite original;

        void Awake()
        {
            patrolLink = GetComponent<Enemy_Patrolling>();
            manageSprite = GetComponent<SpriteRenderer>();
            original = manageSprite.sprite;
        }


        // Use this for initialization
        void Start()
        {
            Cell_Interaction EnemyStartPosition = null;
            // Initializing Enemy Position

            EnemyStartPosition = Grid_Manager.instance.SettingEnemyPosition();

            float x = EnemyStartPosition.transform.position.x;
            float y = EnemyStartPosition.transform.position.y;
            float z = this.transform.position.z;

            patrolLink.SettingWhereI(EnemyStartPosition.cell_i);
            patrolLink.SettingWhereJ(EnemyStartPosition.cell_j);

            Grid_Manager.instance.RemovingPosition(EnemyStartPosition);

            this.transform.position = new Vector3(x, y, z);

            patrolLink.InitalizingPatrolArea(Grid_Manager.instance.FindingPatrolArea(patrolLink.GettingRow(), patrolLink.GettingColumn()));



        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}