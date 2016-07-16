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
        private passiveAbilities passRepo = new passiveAbilities();



        public List<actPlayerAbility> ARepository
        {
            get
            {
                return actRepo;
            }
        }

        public passiveAbilities PassRepo
        {
            get
            {
                return passRepo;
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
