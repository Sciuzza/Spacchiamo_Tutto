using UnityEngine;
using System.Collections;

namespace Spacchiamo {

	internal class UISingleWeaponScript : UIAbilitiesAndWeaponsTooltipCallerScript, ICharacteristicRecognizable {

		private HAND specificHand;

		protected internal override void Start () {

			switch (this.tag) {

			case PARAMETERS.WEAPON_N_1:
				this.uiImageLogic = UIIMAGE.WEAPON_1;
				break;
			case PARAMETERS.WEAPON_N_2:
				this.uiImageLogic = UIIMAGE.WEAPON_2;
				break;
			case PARAMETERS.WEAPON_N_3:
				this.uiImageLogic = UIIMAGE.WEAPON_3;
				break;
			default:
				Debug.LogWarning ("TI STAI DIMENTICANDO UN TAG!\n" + this.name);
				break;

			}

		}

		public override void SetTooltip () {

			this.tooltip.SetDescription (this.uiImageLogic, ABILITY.ABILITY_VOID, this.specificHand);

		}

		public void SetCharacteristic (int passedCharacteristic) {

			this.specificHand = (HAND)passedCharacteristic;

		}
		
	}

}