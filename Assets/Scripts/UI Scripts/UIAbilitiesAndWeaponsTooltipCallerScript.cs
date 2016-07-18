using UnityEngine;
using System.Collections;

namespace Spacchiamo {

	internal class UIAbilitiesAndWeaponsTooltipCallerScript : UIBaseTooltipCallerScript {

		private UIAbilitiesAndWeaponsTooltipScript tooltip;

		protected internal override void Awake () {

			this.tooltip = GameObject.FindGameObjectWithTag (PARAMETERS.TOOLTIP).GetComponent <UIAbilitiesAndWeaponsTooltipScript> ();

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
			case PARAMETERS.POINTS:
				this.uiImageLogic = UIIMAGE.POINTS_FOR_ABILITY;
				break;
			case PARAMETERS.WEAPON1:
				this.uiImageLogic = UIIMAGE.FIRST_WEAPON;
				break;
			case PARAMETERS.WEAPON2:
				this.uiImageLogic = UIIMAGE.SECOND_WEAPON;
				break;
			case PARAMETERS.PLAY:
				this.uiImageLogic = UIIMAGE.PLAY_GAME;
				break;
			default:
				Debug.LogWarning ("TI STAI DIMENTICANDO UN TAG!\n" + this.name);	//PROVVISORIO
				break;

			}

		}

		public override void SetTooltip () {

			this.tooltip.SetDescription (this.uiImageLogic);

		}

		public override void UnSetTooltip () {

			this.tooltip.UnSetDescription ();

		}
		
	}

}