using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Spacchiamo
{

    public class AbiRepository : MonoBehaviour
    {

        [SerializeField]
        private List<actPlayerAbility> actRepo = new List<actPlayerAbility>();

        [SerializeField]
        private passAbilities passRepostr = new passAbilities();


        [SerializeField]
        private List<actEnemyAbility> actEnemyRepo = new List<actEnemyAbility>();



        public List<actPlayerAbility> ARepository
        {
            get
            {
                return actRepo;
            }
        }

        public passAbilities PassRepostr
        {
            get
            {
                return passRepostr;
            }
        }

        public List<actEnemyAbility> ActEnemyRepo
        {
            get
            {
                return actEnemyRepo;
            }
        }

        [HideInInspector]
        public static AbiRepository instance = null;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            
        }

    }
}
