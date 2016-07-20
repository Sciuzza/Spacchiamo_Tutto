using UnityEngine;
using System.Collections;

namespace Spacchiamo {

	internal class UISingleAbilityScript : UIAbilitiesAndWeaponsTooltipCallerScript, ICharacteristicRecognizable {

		private ABILITY specificAbility;

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

		}

	}

}