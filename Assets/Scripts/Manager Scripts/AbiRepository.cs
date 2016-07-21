using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Spacchiamo
{

    public class AbiRepository : MonoBehaviour
    {
        #region PlayerRepo

        [SerializeField]
        private List<actPlayerAbility> actRepo = new List<actPlayerAbility>();

        [SerializeField]
        private passAbilities passRepostr = new passAbilities();

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
        #endregion


        #region EnemyRepo
        [SerializeField]
        public enemySetting[] enemyRepo = new enemySetting[7]; 
        #endregion


        #region SingleTone

        [HideInInspector]
        public static AbiRepository instance = null;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);


        }
        #endregion


        #region EnemyRepoSceneInitializer
        public void SetEnemyLevels(int e1level, int e2level, int e3level, int e4level, int e5level)
        {
            IncreasingSettingStats(e1level, 0);
            IncreasingSettingStats(e2level, 1);
            IncreasingSettingStats(e3level, 2);
            IncreasingSettingStats(e4level, 3);
            IncreasingSettingStats(e5level, 4);

        }

        private void IncreasingSettingStats(int levelToReach, int index)
        {
            for (int i = enemyRepo[index].level; i < levelToReach; i++)
            {
                enemyRepo[index].life += enemyRepo[index].lifeIncPerLevel;
                enemyRepo[index].moveRate -= enemyRepo[index].moveRateDecPerLevel;
                enemyRepo[index].ability.damage += enemyRepo[index].ability.damIncPerLevel;
                enemyRepo[index].ability.cooldown -= enemyRepo[index].ability.cooldownDecPerLevel;
                enemyRepo[index].ability.range += enemyRepo[index].ability.rangeIncPerLevel;
            }
            enemyRepo[index].level = levelToReach;
        } 
        #endregion
    }
}
