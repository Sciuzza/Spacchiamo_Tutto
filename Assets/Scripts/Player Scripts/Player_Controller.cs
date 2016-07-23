using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Spacchiamo
{
    public class Player_Controller : MonoBehaviour
    {
        
        playerActions moveLink;

        public float Life = 20;
        public int expGained;
        public int unspentAbilityPoints;
        public int playerLevel;

        public int FearValue = 0;
        public bool fear1Activated = false;
        public bool fear2Activated = false;
        public float fear1Percent = 0.15f;
        public float fear2Percent = 0.15f;
        public int TurnValue = 0;
        public int fearTurnCounter = 0;

        public bool attackSelection = false;

        public bool firstAbilityPressed, secondAbilityPressed;


        public List<actPlayerAbility> actAbilities = new List<actPlayerAbility>();
        public regAbility regPassive = new regAbility();

        public List<actPlayerAbility> Abilities
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
                    if (actAbilities[0].knockBack == 0)
                        Grid_Manager.instance.HighlightingAttackRange(moveLink.GettingXPlayer(), moveLink.GettingyPlayer(), actAbilities[0].range);
                    else
                        Grid_Manager.instance.HighlightingKnockRange(moveLink.GettingXPlayer(), moveLink.GettingyPlayer(), actAbilities[0].range);

                    firstAbilityPressed = true;
                    attackSelection = true;
                }
                if (Input.GetKeyUp(KeyCode.E) && !attackSelection)
                {
                    if (actAbilities[1].knockBack == 0)
                        Grid_Manager.instance.HighlightingAttackRange(moveLink.GettingXPlayer(), moveLink.GettingyPlayer(), actAbilities[1].range);
                    else
                        Grid_Manager.instance.HighlightingKnockRange(moveLink.GettingXPlayer(), moveLink.GettingyPlayer(), actAbilities[1].range);

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
            Life -= damage;
        }

        public void KillingPlayer()
        {
            Scene_Manager.instance.ResettingLevel();
        }

        public void GainingExp(int expDeadMonster)
        {
            expGained += expDeadMonster;
        }

        public void CheckingCurrentLevel()
        {
            if (expGained % ( (int)(((float)5/2) * playerLevel * playerLevel) + (((float)195/2) * playerLevel) ) == 0)
            {
                unspentAbilityPoints++;
                playerLevel++;
            }

        }

    }
}
