using UnityEngine;
using System.Collections;

namespace Spacchiamo {

	internal class UIAbilitiesAndWeaponsUpgradeButtonScript : MonoBehaviour, ICharacteristicRecognizable, IIncreaseOnClick {

		private UIIMAGE uiImageLogic;
		private ABILITY specificAbility;
		private UIAbilitiesAndWeaponsTooltipCallerScript passiveLevelUpRef, primaryLevelUpRef, secondaryLevelUpRef;
		private GameObject singleAbilityGameObject;

		private void Awake () {

			this.passiveLevelUpRef = GameObject.FindGameObjectWithTag (PARAMETERS.PASSIVE).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();
			this.primaryLevelUpRef = GameObject.FindGameObjectWithTag (PARAMETERS.PRIMARY).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();
			this.secondaryLevelUpRef = GameObject.FindGameObjectWithTag (PARAMETERS.SECONDARY).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();
			this.singleAbilityGameObject = this.GetComponentInParent <UISingleAbilityScript> ().gameObject;

		}

		private void Start () {

			switch (this.singleAbilityGameObject.tag) {

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

		public void SetCharacteristic (int passedCharacteristic) {

			this.specificAbility = (ABILITY)passedCharacteristic;

		}

		public void IncreaseNumberOfPoints () {

			switch (this.specificAbility) {

			case ABILITY.ABILITY_PASSIVE:
				this.passiveLevelUpRef.LevelUpAbility (this.uiImageLogic, this.specificAbility);
				break;
			case ABILITY.ABILITY_PRIMARY:
				this.primaryLevelUpRef.LevelUpAbility (this.uiImageLogic, this.specificAbility);
				break;
			case ABILITY.ABILITY_SECONDARY:
				this.secondaryLevelUpRef.LevelUpAbility (this.uiImageLogic, this.specificAbility);
				break;
			default:
				Debug.LogError ("NON RIESCO AD IDENTIFICARE LA TIPOLOGIA DELL'ABILITA'!");
				break;

			}

		}
		
	}

}