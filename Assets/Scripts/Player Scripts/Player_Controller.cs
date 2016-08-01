using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Spacchiamo
{
    public class Player_Controller : MonoBehaviour
    {

        playerActions moveLink;
        Animator animLink;


        #region Current Abilities

        public List<actPlayerAbility> actAbilities = new List<actPlayerAbility>();
        public regAbility regPassive = new regAbility();
        private combattente fighting = new combattente();
        private esploratore traveler = new esploratore();
        private sopravvissuto survivor = new sopravvissuto();

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
        public combattente Fighting
        {
            get
            {
                return fighting;
            }

            set
            {
                fighting = value;
            }
        }
        public esploratore Traveler
        {
            get
            {
                return traveler;
            }

            set
            {
                traveler = value;
            }
        }
        public sopravvissuto Survivor
        {
            get
            {
                return survivor;
            }

            set
            {
                survivor = value;
            }
        } 
        #endregion

        public playerSettings CurSet = new playerSettings();

        public bool attackSelection;
        public bool firstAbilityPressed, secondAbilityPressed;
        public bool isLoadingScene = false;
        public bool cdIncChecked = false;

        void Awake()
        {
            moveLink = GetComponent<playerActions>();
            animLink = GetComponent<Animator>();
        }


        void Update()
        {
            if (Game_Controller.instance.currentPhase == GAME_PHASE.playerTurn)
            {
                if (!cdIncChecked)
                {
                    if (actAbilities[0].cdCounter < actAbilities[0].cooldown)
                        IncreaseAbilityCounter(0);
                    if (actAbilities[1].cdCounter < actAbilities[1].cooldown)
                        IncreaseAbilityCounter(1);

                    cdIncChecked = true;
                }

                if (Input.GetKeyDown(KeyCode.F) && !attackSelection && Grid_Manager.instance.IsPlayerCloseToTrainer(moveLink.xPlayer, moveLink.yPlayer))
                    Ui_Manager.instance.SetStory(Scene_Manager.instance.GetCurrentSceneIndex() - 6);

                #region Skip Turn Input
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    moveLink.IncreasingFearAndTurn();
                    Enemies_Manager.instance.CheckingAggro();
                    Game_Controller.instance.currentPhase = GAME_PHASE.npcEnemyTurn;
                } 
                #endregion

                #region First Ability Input
                if (Input.GetKeyUp(KeyCode.Q))
                {
                    if (!attackSelection && actAbilities[0].cdCounter == actAbilities[0].cooldown)
                    {
                        if (ActAbilities[0].knockBack == 0)
                            Grid_Manager.instance.HighlightingAttackRange(moveLink.GettingXPlayer(), moveLink.GettingyPlayer(), ActAbilities[0].range);
                        else
                            Grid_Manager.instance.HighlightingKnockRange(moveLink.GettingXPlayer(), moveLink.GettingyPlayer(), ActAbilities[0].range);

                        firstAbilityPressed = true;
                        attackSelection = true;
                    }
                    else if (attackSelection && firstAbilityPressed)
                    {
                        Grid_Manager.instance.GettingLight(moveLink.GettingXPlayer(), moveLink.GettingyPlayer());
                        firstAbilityPressed = false;
                        attackSelection = false;
                    }
                } 
                #endregion

                #region Second Ability Input
                else if (Input.GetKeyUp(KeyCode.E))
                {
                    if (!attackSelection && actAbilities[1].cdCounter == actAbilities[1].cooldown)
                    {
                        if (ActAbilities[1].knockBack == 0)
                            Grid_Manager.instance.HighlightingAttackRange(moveLink.GettingXPlayer(), moveLink.GettingyPlayer(), ActAbilities[1].range);
                        else
                            Grid_Manager.instance.HighlightingKnockRange(moveLink.GettingXPlayer(), moveLink.GettingyPlayer(), ActAbilities[1].range);

                        secondAbilityPressed = true;
                        attackSelection = true;
                    }
                    else if (attackSelection && secondAbilityPressed)
                    {
                        Grid_Manager.instance.GettingLight(moveLink.GettingXPlayer(), moveLink.GettingyPlayer());
                        firstAbilityPressed = false;
                        attackSelection = false;
                    } 
                    #endregion
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
                    Scene_Manager.instance.LoadAbilityScene();
                    isLoadingScene = true;
                }
                if (Input.GetKeyDown(KeyCode.Mouse1) && attackSelection)
                {
                    Grid_Manager.instance.GettingLight(moveLink.GettingXPlayer(), moveLink.GettingyPlayer());
                    firstAbilityPressed = false;
                    secondAbilityPressed = false;
                    attackSelection = false;
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
            cdIncChecked = false;

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
            Ui_Manager.instance.ShowGameOver();
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

            moveLink.FearManager();

            CurSet.Life += 2;
            if (CurSet.Life > CurSet.maxLife)
                CurSet.Life = CurSet.maxLife;

            Ui_Manager.instance.SettingLife((int)CurSet.Life);

            CurSet.healthPotStacks--;

            Ui_Manager.instance.SettingFearValue(CurSet.FearValue);

        }

        public void ApplyingTrainerEffects()
        {
            CurSet.expGained += 50;
            CurSet.FearValue = 0;
            Ui_Manager.instance.SettingFearValue(CurSet.FearValue);
            CurSet.healthPotStacks += 2;

            if (survivor.active && CurSet.healthPotStacks > 4)
                CurSet.healthPotStacks = 4;
            else if (!survivor.active && CurSet.healthPotStacks > 2)
                CurSet.healthPotStacks = 2;
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
            else if (CurSet.passiveStorage.fighting.active)
            {
                fighting = CurSet.passiveStorage.fighting;
                ApplyingFightingEffects();
            }
            else if (CurSet.passiveStorage.traveler.active)
            {
                traveler = CurSet.passiveStorage.traveler;
                ApplyingTravelerEffects();
            }
            else if (CurSet.passiveStorage.survivor.active)
            {
                survivor = CurSet.passiveStorage.survivor;
                ApplyingSurvivorEffects();
            }
        }


        private void ApplyingFightingEffects()
        {
            if (CurSet.passiveStorage.fighting.level == 1)
                CurSet.maxLife = 12;
            else if (CurSet.passiveStorage.fighting.level == 2)
                CurSet.maxLife = 16;
            else if (CurSet.passiveStorage.fighting.level == 3)
                CurSet.maxLife = 20;
        }

        private void ApplyingTravelerEffects()
        {
            if (CurSet.passiveStorage.traveler.level == 1)
                CurSet.maxLife = 12;
            else if (CurSet.passiveStorage.traveler.level == 2)
                CurSet.maxLife = 14;
            else if (CurSet.passiveStorage.traveler.level == 3)
                CurSet.maxLife = 16;

            CurSet.lightRange += 1;
        } 

        private void ApplyingSurvivorEffects()
        {
            if (CurSet.passiveStorage.survivor.level == 1)
                CurSet.maxLife = 12;
            else if (CurSet.passiveStorage.survivor.level == 2)
                CurSet.maxLife = 14;
            else if (CurSet.passiveStorage.survivor.level == 3)
                CurSet.maxLife = 16;

            CurSet.healthPotStacks += 2;
        }

        public void ResetAbilityCounter (int abilityIndex)
        {
            actPlayerAbility counterReset = actAbilities[abilityIndex];
            counterReset.cdCounter = 0;
            actAbilities[abilityIndex] = counterReset;
        }

        public void IncreaseAbilityCounter (int abilityIndex)
        {
            actPlayerAbility counterReset = actAbilities[abilityIndex];
            counterReset.cdCounter++;
            actAbilities[abilityIndex] = counterReset;
        }


        public void ArmaBiancaAnimation()
        {
            animLink.SetBool("ArmaBianca", true);
            Invoke("ArmaBiancaEnd", 1.0f);   
        }

        private void ArmaBiancaEnd()
        {
            animLink.SetBool("ArmaBianca", false);
        }

        public void ArmaRangedAnimation()
        {
            animLink.SetBool("ArmaRanged", true);
            Invoke("ArmaRangedEnd", 1.0f);
        }

        public void ArmaRangedEnd()
        {
            animLink.SetBool("ArmaRanged", false);
        }


        public void SettingPlayerLifeToMax()
        {
            CurSet.Life = CurSet.maxLife;
        }
    }
}
