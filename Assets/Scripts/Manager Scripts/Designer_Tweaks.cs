using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Spacchiamo
{
    public class Designer_Tweaks : MonoBehaviour
    {
        #region General Designer Tweak Values
        //General Designer Variables
        [Range(1, 5)]
        public int playerLightM, faloLigthM, generalMoveSpeed, fearScaleRate; 
        #endregion

        #region General Programmer Tweak Values
        private int level1YWidth = 500;
        private int level1XWidth = 500;

        public int Level1XWidth
        {
            get
            {
                return level1XWidth;
            }
        }

        public int Level1YWidth
        {
            get
            {
                return level1YWidth;
            }
        } 
        #endregion

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

        #region SingleTone
        [HideInInspector]
        public static Designer_Tweaks instance = null;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        } 
        #endregion

    }
}