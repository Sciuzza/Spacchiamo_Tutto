using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Spacchiamo
{
    public class Designer_Tweaks : MonoBehaviour
    {
        //General Designer Variables
        [Range(1, 5)]
        public int playerLightM, faloLigthM, generalMoveSpeed, fearScaleRate, patrolAreaEnemyM;

        [HideInInspector]
        public int level1XWidth = 64, level1yWidth = 54;

        #region Designer player ability selection for testing purpose

        public originalName primaryTesting;

        public weaponType primaryWeapon;

        [Range(1, 5)]
        public int primaryLevel;


        public originalName seconTesting;

        public weaponType seconWeapon;

        [Range(1, 5)]
        public int seconLevel;

        public pOriginalName passiveTesting;

        [Range(1, 3)]
        public int passiveLevel;
        #endregion

        #region Designer enemy ability selection

        [System.Serializable]
        public struct ActEnemyAbDesigner
        {
            public eneOriginalName oname;
            [Range(1, 5)]
            public int level;
        }

        [System.Serializable]
        public struct enemySettings
        {
            public enemy whichEnemy;
            public List<ActEnemyAbDesigner> actAbilities;
        }

        public List<enemySettings> Enemies = new List<enemySettings>(); 
        #endregion


        [HideInInspector]
        public static Designer_Tweaks instance = null;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

    }
}