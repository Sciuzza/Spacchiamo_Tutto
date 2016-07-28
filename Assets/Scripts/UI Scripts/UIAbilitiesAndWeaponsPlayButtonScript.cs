using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Spacchiamo {

	internal class UIAbilitiesAndWeaponsPlayButtonScript : MonoBehaviour, ICheckPlayability {

		private Button playButton;
		private UIAbilitiesAndWeaponsTooltipCallerScript passiveAbilitySelected, primaryAbilitySelected, secondaryAbilitySelected, primaryWeaponSelected, secondaryWeaponSelected;
		private UIAbilitiesAndWeaponsPointsScript pointsRepository;

		private void Awake () {

			this.playButton = this.GetComponent <Button> ();

			this.passiveAbilitySelected = GameObject.FindGameObjectWithTag (PARAMETERS.PASSIVE).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();
			this.primaryAbilitySelected = GameObject.FindGameObjectWithTag (PARAMETERS.PRIMARY).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();
			this.secondaryAbilitySelected = GameObject.FindGameObjectWithTag (PARAMETERS.SECONDARY).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();
			this.pointsRepository = GameObject.FindGameObjectWithTag (PARAMETERS.POINTS).GetComponent <UIAbilitiesAndWeaponsPointsScript> ();
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

		public void RecordParameters () {

			AbilitiesAndWeaponsInitialization newAbilitiesAndWeaponsParameters = new AbilitiesAndWeaponsInitialization ();

			newAbilitiesAndWeaponsParameters.passiveAbility1Level = this.passiveAbilitySelected.Ability1Level;
			newAbilitiesAndWeaponsParameters.passiveAbility2Level = this.passiveAbilitySelected.Ability2Level;
			newAbilitiesAndWeaponsParameters.passiveAbility3Level = this.passiveAbilitySelected.Ability3Level;
			newAbilitiesAndWeaponsParameters.passiveAbility4Level = this.passiveAbilitySelected.Ability4Level;
			newAbilitiesAndWeaponsParameters.passiveAbility5Level = this.passiveAbilitySelected.Ability5Level;

			newAbilitiesAndWeaponsParameters.primaryAbility1Level = this.primaryAbilitySelected.Ability1Level;
			newAbilitiesAndWeaponsParameters.primaryAbility2Level = this.primaryAbilitySelected.Ability2Level;
			newAbilitiesAndWeaponsParameters.primaryAbility3Level = this.primaryAbilitySelected.Ability3Level;
			newAbilitiesAndWeaponsParameters.primaryAbility4Level = this.primaryAbilitySelected.Ability4Level;
			newAbilitiesAndWeaponsParameters.primaryAbility5Level = this.primaryAbilitySelected.Ability5Level;

			newAbilitiesAndWeaponsParameters.secondaryAbility1Level = this.secondaryAbilitySelected.Ability1Level;
			newAbilitiesAndWeaponsParameters.secondaryAbility2Level = this.secondaryAbilitySelected.Ability2Level;
			newAbilitiesAndWeaponsParameters.secondaryAbility3Level = this.secondaryAbilitySelected.Ability3Level;
			newAbilitiesAndWeaponsParameters.secondaryAbility4Level = this.secondaryAbilitySelected.Ability4Level;
			newAbilitiesAndWeaponsParameters.secondaryAbility5Level = this.secondaryAbilitySelected.Ability5Level;

			newAbilitiesAndWeaponsParameters.levelUpPoints = this.pointsRepository.Points;

			newAbilitiesAndWeaponsParameters.passiveAbilityUICharacteristic = this.passiveAbilitySelected.uiCharacteristic;
			newAbilitiesAndWeaponsParameters.primaryAbilityUICharacteristic = this.primaryAbilitySelected.uiCharacteristic;
			newAbilitiesAndWeaponsParameters.secondaryAbilityUICharacteristic = this.secondaryAbilitySelected.uiCharacteristic;

			newAbilitiesAndWeaponsParameters.primaryWeaponUICharacteristic = this.primaryWeaponSelected.uiCharacteristic;
			newAbilitiesAndWeaponsParameters.secondaryWeaponUICharacteristic = this.secondaryWeaponSelected.uiCharacteristic;

            /*Debug.Log (this.passiveAbilitySelected.uiCharacteristic.ToString ());

			Debug.Log (this.passiveAbilitySelected.Ability1Level.ToString ());
			Debug.Log (this.passiveAbilitySelected.Ability2Level.ToString ());
			Debug.Log (this.passiveAbilitySelected.Ability3Level.ToString ());
			Debug.Log (this.passiveAbilitySelected.Ability4Level.ToString ());
			Debug.Log (this.passiveAbilitySelected.Ability5Level.ToString ());

			Debug.Log (this.primaryAbilitySelected.uiCharacteristic.ToString ());

			Debug.Log (this.primaryAbilitySelected.Ability1Level.ToString ());
			Debug.Log (this.primaryAbilitySelected.Ability2Level.ToString ());
			Debug.Log (this.primaryAbilitySelected.Ability3Level.ToString ());
			Debug.Log (this.primaryAbilitySelected.Ability4Level.ToString ());
			Debug.Log (this.primaryAbilitySelected.Ability5Level.ToString ());

			Debug.Log (this.secondaryAbilitySelected.uiCharacteristic.ToString ());

			Debug.Log (this.secondaryAbilitySelected.Ability1Level.ToString ());
			Debug.Log (this.secondaryAbilitySelected.Ability2Level.ToString ());
			Debug.Log (this.secondaryAbilitySelected.Ability3Level.ToString ());
			Debug.Log (this.secondaryAbilitySelected.Ability4Level.ToString ());
			Debug.Log (this.secondaryAbilitySelected.Ability5Level.ToString ());

			Debug.Log (this.pointsRepository.Points.ToString ());

			Debug.Log (this.primaryWeaponSelected.uiCharacteristic.ToString ());
			Debug.Log (this.secondaryWeaponSelected.uiCharacteristic.ToString ());*/

            Ui_Manager.instance.ReceivingAbilitiesAndWeaponsUserInterface (newAbilitiesAndWeaponsParameters);
            Scene_Manager.instance.LoadNextLevel();

		}
		
	}

}