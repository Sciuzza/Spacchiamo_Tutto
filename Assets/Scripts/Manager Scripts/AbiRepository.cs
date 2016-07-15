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
        private regAbility regeneration = new regAbility();

       

        public List<actPlayerAbility> ARepository
        {
            get
            {
                return actRepo;
            }
        }

        public regAbility Regeneration
        {
            get
            {
                return regeneration;
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
