using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Spacchiamo
{
    public class Player_Controller : MonoBehaviour
    {

        playerActions moveLink;



        public List<actPlayerAbility> actAbilities = new List<actPlayerAbility>();
        public regAbility regPassive = new regAbility();

        public regAbility RegPassive
        {
            get
            {
                return regPassive;
            }

            set
            {
                regPassive = value;
            }
        }
        public List<actPlayerAbility> ActAbilities
        {
            get
            {
                return actAbilities;
            }

            set
            {
                actAbilities = value;
            }
        }

        public playerSettings CurSet = new playerSettings();

        public bool attackSelection;
        public bool firstAbilityPressed, secondAbilityPressed;
        public bool isLoadingScene = false;

        void Awake()
        {
            moveLink = GetComponent<playerActions>();

        }


        void Update()
        {
            if (Game_Controller.instance.currentPhase == GAME_PHASE.playerTurn)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    moveLink.IncreasingFearAndTurn();
                    Enemies_Manager.instance.CheckingAggro();
                    Game_Controller.instance.currentPhase = GAME_PHASE.npcEnemyTurn;
                }
                if (Input.GetKeyUp(KeyCode.Q) && !attackSelection)
                {
                    if (ActAbilities[0].knockBack == 0)
                        Grid_Manager.instance.HighlightingAttackRange(moveLink.GettingXPlayer(), moveLink.GettingyPlayer(), ActAbilities[0].range);
                    else
                        Grid_Manager.instance.HighlightingKnockRange(moveLink.GettingXPlayer(), moveLink.GettingyPlayer(), ActAbilities[0].range);

                    firstAbilityPressed = true;
                    attackSelection = true;
                }
                if (Input.GetKeyUp(KeyCode.E) && !attackSelection)
                {
                    if (ActAbilities[1].knockBack == 0)
                        Grid_Manager.instance.HighlightingAttackRange(moveLink.GettingXPlayer(), moveLink.GettingyPlayer(), ActAbilities[1].range);
                    else
                        Grid_Manager.instance.HighlightingKnockRange(moveLink.GettingXPlayer(), moveLink.GettingyPlayer(), ActAbilities[1].range);

                    secondAbilityPressed = true;
                    attackSelection = true;
                }
                if (Input.GetKeyDown(KeyCode.Escape) && attackSelection)
                {
                    if (firstAbilityPressed)
                    {
                        Grid_Manager.instance.GettingLight(moveLink.GettingXPlayer(), moveLink.GettingyPlayer());
                        firstAbilityPressed = false;
                    }
                    else if (secondAbilityPressed)
                    {
                        Grid_Manager.instance.GettingLight(moveLink.GettingXPlayer(), moveLink.GettingyPlayer());
                        secondAbilityPressed = false;
                    }

                    attackSelection = false;
                }
                if (Input.GetKeyUp(KeyCode.R) && !attackSelection && CurSet.healthPotStacks >= 1)
                {
                    ApplyingHPotionEffects();
                    moveLink.IncreasingFearAndTurn();
                    Enemies_Manager.instance.CheckingAggro();
                    Game_Controller.instance.currentPhase = GAME_PHASE.npcEnemyTurn;
                }
                if (Grid_Manager.instance.IsPlayerOnExit(moveLink.xPlayer, moveLink.yPlayer) && !isLoadingScene)
                {
                    Game_Controller.instance.SavePlayerData(CurSet);
                    Scene_Manager.instance.LoadNextLevel();
                    isLoadingScene = true;
                }
            }
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


        public void ResetAttackBooleans()
        {

            attackSelection = false;

            Grid_Manager.instance.GettingLight(moveLink.GettingXPlayer(), moveLink.GettingyPlayer());

            if (firstAbilityPressed)
                firstAbilityPressed = false;
            else if (secondAbilityPressed)
                secondAbilityPressed = false;



        }


        public void TakingDamage(float damage)
        {
            CurSet.Life -= damage;
            if (CurSet.Life > 0)
                Ui_Manager.instance.SettingLife((int)CurSet.Life);
            else if (CurSet.Life <= 0)
            {
                CurSet.Life = 0;
                Ui_Manager.instance.SettingLife((int)CurSet.Life);
                KillingPlayer();
            }
        }

        public void KillingPlayer()
        {
            Scene_Manager.instance.ResettingLevel();
        }

        public void GainingExp(int expDeadMonster)
        {
            CurSet.expGained += expDeadMonster;
        }

        public void CheckingCurrentLevel()
        {
            if (CurSet.expGained % ((int)(((float)5 / 2) * CurSet.playerLevel * CurSet.playerLevel) + (((float)195 / 2) * CurSet.playerLevel)) == 0)
            {
                CurSet.unspentAbilityPoints++;
                CurSet.playerLevel++;
            }

        }

        private void ApplyingHPotionEffects()
        {
            CurSet.FearValue -= 10;
            if (CurSet.FearValue < 0)
                CurSet.FearValue = 0;

            CurSet.Life += 2;
            if (CurSet.Life > 20)
                CurSet.Life = 20;

            Ui_Manager.instance.SettingLife((int)CurSet.Life);

            CurSet.healthPotStacks--;

            Ui_Manager.instance.SettingFearValue(CurSet.FearValue);

        }


        public void SelectingActiveAbilities()
        {
            actPlayerAbility qAbility = new actPlayerAbility();
            qAbility = CurSet.activeStorage.Find(x => x.active == true && x.category == type.Primary);
            actAbilities.Add(qAbility);

            actPlayerAbility eAbility = new actPlayerAbility();
            eAbility = CurSet.activeStorage.Find(x => x.active == true && x.category == type.Secondary);
            actAbilities.Add(eAbility);

            if (CurSet.passiveStorage.regeneration.active)
                regPassive = CurSet.passiveStorage.regeneration;
        }

    }
}
