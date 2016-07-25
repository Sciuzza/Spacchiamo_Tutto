using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Spacchiamo {

	internal class UIHUDAbilitiesScript : UIBaseTooltipCallerScript, IActivateAnAbilityOnClick {

		private Image image;
		private Button primaryAbilityButton, secondaryAbilityButton, potionAbilityButton;
		private UIHUDTooltipScript tooltip;

		protected internal override void Awake () {

			this.image = this.GetComponent <Image> ();

			this.primaryAbilityButton = GameObject.FindGameObjectWithTag (PARAMETERS.PRIMARY).GetComponent <Button> ();
			this.secondaryAbilityButton = GameObject.FindGameObjectWithTag (PARAMETERS.SECONDARY).GetComponent <Button> ();
			this.potionAbilityButton = GameObject.FindGameObjectWithTag (PARAMETERS.POTION_TAG).GetComponent <Button> ();

			this.tooltip = this.GetComponentInParent <Canvas> ().gameObject.GetComponentInChildren <UIHUDTooltipScript> (true);

		}

		protected internal override void Start () {

			switch (this.tag) {

			case PARAMETERS.PASSIVE:
				this.uiImageLogic = UIIMAGE.PASSIVE_ABILITY;
				break;
			case PARAMETERS.PRIMARY:
				this.uiImageLogic = UIIMAGE.PRIMARY_ABILITY;
				break;
			case PARAMETERS.SECONDARY:
				this.uiImageLogic = UIIMAGE.SECONDARY_ABILITY;
				break;
			case PARAMETERS.POTION_TAG:
				this.uiImageLogic = UIIMAGE.POTION;
				break;
			default:
				Debug.LogWarning ("TI STAI DIMENTICANDO UN TAG!\n" + this.name);
				break;

			}

		}

		private void Update () {

			if (Input.GetKeyDown (KeyCode.Alpha1))
				this.ActivateAbility (1);

			if (Input.GetKeyDown (KeyCode.Alpha2))
				this.ActivateAbility (2);

			if (Input.GetKeyDown (KeyCode.Alpha3))
				this.ActivateAbility (3);

			if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.Mouse1))
				this.ActivateAbility (4);

		}

		public override void SetTooltip () {

			this.tooltip.SetDescription (this.uiImageLogic);

		}

		public override void UnSetTooltip () {

			this.tooltip.UnSetDescription ();

		}

		public void ActivateAbility (int i) {

			switch (i) {

			case 1:
				//ATTIVAZIONE ABILITA' PRIMARIA
				this.primaryAbilityButton.interactable = false;
				this.secondaryAbilityButton.interactable = true;
				break;
			case 2:
				//ATTIVAZIONE ABILITA' SECONDARIA
				this.primaryAbilityButton.interactable = true;
				this.secondaryAbilityButton.interactable = false;
				break;
			case 3:
				//ATTIVAZIONE POZIONE
				break;
			case 4:
				//UNLOCKING
				this.primaryAbilityButton.interactable = true;
				this.secondaryAbilityButton.interactable = true;
				break;
			default:
				Debug.LogError ("NON RIESCO AD ATTIVARE ALCUNA ABILITA'!");
				break;

			}

		}
		
	}

}