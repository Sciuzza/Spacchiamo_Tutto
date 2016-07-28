using UnityEngine;
using System.Collections;

namespace Spacchiamo {

	public class UIAbilitiesAndWeaponsCanvasScript : MonoBehaviour {

		private UIAbilitiesAndWeaponsTooltipCallerScript passiveAbilitySlot, primaryAbilitySlot, secondaryAbilitySlot, primaryWeaponSlot, secondaryWeaponSlot;
		private UIAbilitiesAndWeaponsPointsScript pointsRepository;

		private void Awake () {

			this.passiveAbilitySlot = GameObject.FindGameObjectWithTag (PARAMETERS.PASSIVE).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();
			this.primaryAbilitySlot = GameObject.FindGameObjectWithTag (PARAMETERS.PRIMARY).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();
			this.secondaryAbilitySlot = GameObject.FindGameObjectWithTag (PARAMETERS.SECONDARY).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();
			this.pointsRepository = GameObject.FindGameObjectWithTag (PARAMETERS.POINTS).GetComponent <UIAbilitiesAndWeaponsPointsScript> ();
			this.primaryWeaponSlot = GameObject.FindGameObjectWithTag (PARAMETERS.WEAPON1).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();
			this.secondaryWeaponSlot = GameObject.FindGameObjectWithTag (PARAMETERS.WEAPON2).GetComponent <UIAbilitiesAndWeaponsTooltipCallerScript> ();

		}

		/*private void Update () {

			if (Input.GetKeyDown (KeyCode.R))
				InitializeAbilitiesAndWeaponsCanvas (null);

		}*/

		internal void InitializeAbilitiesAndWeaponsCanvas (AbilitiesAndWeaponsInitialization initializationParameters) {

			//TEST
			/*if (initializationParameters == null)
				initializationParameters = new AbilitiesAndWeaponsInitialization ();*/
			//END_TEST

			this.passiveAbilitySlot.Ability1Level = initializationParameters.passiveAbility1Level;
			this.passiveAbilitySlot.Ability2Level = initializationParameters.passiveAbility2Level;
			this.passiveAbilitySlot.Ability3Level = initializationParameters.passiveAbility3Level;
			this.passiveAbilitySlot.Ability4Level = initializationParameters.passiveAbility4Level;
			this.passiveAbilitySlot.Ability5Level = initializationParameters.passiveAbility5Level;

			this.primaryAbilitySlot.Ability1Level = initializationParameters.primaryAbility1Level;
			this.primaryAbilitySlot.Ability2Level = initializationParameters.primaryAbility2Level;
			this.primaryAbilitySlot.Ability3Level = initializationParameters.primaryAbility3Level;
			this.primaryAbilitySlot.Ability4Level = initializationParameters.primaryAbility4Level;
			this.primaryAbilitySlot.Ability5Level = initializationParameters.primaryAbility5Level;

			this.secondaryAbilitySlot.Ability1Level = initializationParameters.secondaryAbility1Level;
			this.secondaryAbilitySlot.Ability2Level = initializationParameters.secondaryAbility2Level;
			this.secondaryAbilitySlot.Ability3Level = initializationParameters.secondaryAbility3Level;
			this.secondaryAbilitySlot.Ability4Level = initializationParameters.secondaryAbility4Level;
			this.secondaryAbilitySlot.Ability5Level = initializationParameters.secondaryAbility5Level;

			this.pointsRepository.Points = initializationParameters.levelUpPoints;
			this.pointsRepository.UpdateUpgradeVisibility ();

			this.passiveAbilitySlot.LoadCharacteristic (initializationParameters.passiveAbilityUICharacteristic);
			this.primaryAbilitySlot.LoadCharacteristic (initializationParameters.primaryAbilityUICharacteristic);
			this.secondaryAbilitySlot.LoadCharacteristic (initializationParameters.secondaryAbilityUICharacteristic);

			this.primaryWeaponSlot.LoadCharacteristic (initializationParameters.primaryWeaponUICharacteristic);
			this.secondaryWeaponSlot.LoadCharacteristic (initializationParameters.secondaryWeaponUICharacteristic);

		}
		
	}

}