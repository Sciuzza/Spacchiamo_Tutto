using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Spacchiamo {

	internal class UIAbilitiesAndWeaponsPlayButtonScript : MonoBehaviour, ICheckPlayability {

		private Button playButton;
		private UIAbilitiesAndWeaponsTooltipCallerScript passiveAbilitySelected, primaryAbilitySelected, secondaryAbilitySelected, primaryWeaponSelected, secondaryWeaponSelected;

		private void Awake () {

			this.playButton = this.GetComponent <Button> ();

			this.passiveAbilitySelected = GameObject.FindGameObjectWithTag (PARAMETERS.PASSIVE).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();
			this.primaryAbilitySelected = GameObject.FindGameObjectWithTag (PARAMETERS.PRIMARY).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();
			this.secondaryAbilitySelected = GameObject.FindGameObjectWithTag (PARAMETERS.SECONDARY).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();
			this.primaryWeaponSelected = GameObject.FindGameObjectWithTag (PARAMETERS.WEAPON1).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();
			this.secondaryWeaponSelected = GameObject.FindGameObjectWithTag (PARAMETERS.WEAPON2).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();

		}

		private void Start () {

			this.CheckPlayability ();

		}

		public void CheckPlayability () {

			if ( (this.primaryAbilitySelected.uiCharacteristic != UIIMAGE.NULL) && (this.primaryAbilitySelected.uiCharacteristic != UIIMAGE.NO_ABILITY) && (this.primaryWeaponSelected.uiCharacteristic != UIIMAGE.NULL) && (this.secondaryWeaponSelected.uiCharacteristic != UIIMAGE.NULL) )
				this.playButton.interactable = true;
			else
				this.playButton.interactable = false;

		}
		
	}

}