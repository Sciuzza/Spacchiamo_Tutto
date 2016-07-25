using UnityEngine;
using System.Collections;

namespace Spacchiamo {

	internal class UISingleAbilityScript : UIAbilitiesAndWeaponsTooltipCallerScript, ICharacteristicRecognizable, IDeductLoad {

		private ABILITY specificAbility;
		private UIAbilitiesAndWeaponsTooltipCallerScript passiveability, primaryAbility, secondaryAbility;

		protected internal override void Awake () {

			base.Awake ();

			this.passiveability = GameObject.FindGameObjectWithTag (PARAMETERS.PASSIVE).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();
			this.primaryAbility = GameObject.FindGameObjectWithTag (PARAMETERS.PRIMARY).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();
			this.secondaryAbility = GameObject.FindGameObjectWithTag (PARAMETERS.SECONDARY).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();

		}

		protected internal override void Start () {

			switch (this.tag) {

			case PARAMETERS.ABILITY_N_0:
				this.uiImageLogic = UIIMAGE.NO_ABILITY;
				break;
			case PARAMETERS.ABILITY_N_1:
				this.uiImageLogic = UIIMAGE.ABILITY_1;
				break;
			case PARAMETERS.ABILITY_N_2:
				this.uiImageLogic = UIIMAGE.ABILITY_2;
				break;
			case PARAMETERS.ABILITY_N_3:
				this.uiImageLogic = UIIMAGE.ABILITY_3;
				break;
			case PARAMETERS.ABILITY_N_4:
				this.uiImageLogic = UIIMAGE.ABILITY_4;
				break;
			case PARAMETERS.ABILITY_N_5:
				this.uiImageLogic = UIIMAGE.ABILITY_5;
				break;
			default:
				Debug.LogWarning ("TI STAI DIMENTICANDO UN TAG!\n" + this.name);
				break;

			}

		}

		public override void SetTooltip () {

			this.tooltip.SetDescription (this.uiImageLogic, this.specificAbility);

		}

		public void SetCharacteristic (int passedCharacteristic) {

			this.specificAbility = (ABILITY)passedCharacteristic;

			//Qui dentro, caricamento delle specifiche sprite di abilità

		}

		public void DeductCharacteristicToLoad () {

			switch (this.specificAbility) {

			case ABILITY.ABILITY_PASSIVE:
				this.passiveability.LoadCharacteristic (this.uiImageLogic);
				break;
			case ABILITY.ABILITY_PRIMARY:
				this.primaryAbility.LoadCharacteristic (this.uiImageLogic);
				break;
			case ABILITY.ABILITY_SECONDARY:
				this.secondaryAbility.LoadCharacteristic (this.uiImageLogic);
				break;
			default:
				Debug.LogError ("NON RIESCO A PASSARGLI L'ABILITA'!");
				break;

			}

		}

	}

}