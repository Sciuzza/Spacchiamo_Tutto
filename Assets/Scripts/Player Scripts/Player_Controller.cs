﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Spacchiamo
{
    public class Player_Controller : MonoBehaviour
    {
        public int fearTurnCounter = 0;
        playerActions moveLink;

        public int Life = 20;
        public int FearValue = 0;
        public int TurnValue = 0;


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
                    Game_Controller.instance.ChangePhase(GAME_PHASE.playerTurn);
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
                        if (actAbilities[0].knockBack == 0)
                            Grid_Manager.instance.DelightingAttackRange(moveLink.GettingXPlayer(), moveLink.GettingyPlayer(), actAbilities[0].range);
                        else
                            Grid_Manager.instance.DelightingKnockRange(moveLink.GettingXPlayer(), moveLink.GettingyPlayer(), actAbilities[0].range);
                        firstAbilityPressed = false;
                    }
                    else if (secondAbilityPressed)
                    {
                        if (actAbilities[1].knockBack == 0)
                            Grid_Manager.instance.DelightingAttackRange(moveLink.GettingXPlayer(), moveLink.GettingyPlayer(), actAbilities[1].range);
                        else
                            Grid_Manager.instance.DelightingKnockRange(moveLink.GettingXPlayer(), moveLink.GettingyPlayer(), actAbilities[1].range);
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


        public void Attack(int xCell, int yCell)
        {

            if (Enemies_Manager.instance.EnemyIsHere(xCell, yCell))
                Enemies_Manager.instance.DestroyEnemy(xCell, yCell);

            attackSelection = false;
            if (firstAbilityPressed)
            {
                Grid_Manager.instance.DelightingAttackRange(moveLink.GettingXPlayer(), moveLink.GettingyPlayer(), actAbilities[0].range);
                firstAbilityPressed = false;
            }
            else if (secondAbilityPressed)
            {
                Grid_Manager.instance.DelightingAttackRange(moveLink.GettingXPlayer(), moveLink.GettingyPlayer(), actAbilities[1].range);
                secondAbilityPressed = false;
            }
            Game_Controller.instance.ChangePhase(GAME_PHASE.playerTurn);

        }

     


    }
}
