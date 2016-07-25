using UnityEngine;
using System.Collections;

namespace Spacchiamo {

	internal class UISingleWeaponScript : UIAbilitiesAndWeaponsTooltipCallerScript, ICharacteristicRecognizable, IDeductLoad {

		private HAND specificHand;
		private UIAbilitiesAndWeaponsTooltipCallerScript firstWeapon, secondWeapon;

		protected internal override void Awake () {

			base.Awake ();

			this.firstWeapon = GameObject.FindGameObjectWithTag (PARAMETERS.WEAPON1).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();
			this.secondWeapon = GameObject.FindGameObjectWithTag (PARAMETERS.WEAPON2).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();

		}

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

		public void DeductCharacteristicToLoad () {

			switch (this.specificHand) {

			case HAND.HAND_ONE:
				this.firstWeapon.LoadCharacteristic (this.uiImageLogic);
				break;
			case HAND.HAND_TWO:
				this.secondWeapon.LoadCharacteristic (this.uiImageLogic);
				break;
			default:
				Debug.LogError ("NON RIESCO A PASSARGLI L'ARMA!");
				break;

			}

		}
		
	}

}