using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class Enemy_Controller : MonoBehaviour
    {

        Enemy_Patrolling patrolLink;
        

        SpriteRenderer manageSprite;
        

        public bool isAggroed = false;
        public bool isComingBack = false;
        public bool isIgnoringAggro = false;
        public int aggroIgnoringCounter = 0;

     

        void Awake()
        {
            patrolLink = GetComponent<Enemy_Patrolling>();
            manageSprite = GetComponent<SpriteRenderer>();
          

            
        }


        // Use this for initialization
        void Start()
        {

            Enemies_Manager.instance.GivingEnemyRef(this.gameObject);
            patrolLink.SettingXEnemy(Mathf.FloorToInt(this.transform.position.x));
            patrolLink.SettingYEnemy(Mathf.FloorToInt(this.transform.position.y));
            
            
            
                  
            



        }

      
    }
}



/*
           Cell_Interaction EnemyStartPosition = null;
           // Initializing Enemy Position

           EnemyStartPosition = Grid_Manager.instance.SettingEnemyPosition();

           float x = EnemyStartPosition.transform.position.x;
           float y = EnemyStartPosition.transform.position.y;
           float z = this.transform.position.z;

            this.transform.position = new Vector3(x, y, z);
           */
