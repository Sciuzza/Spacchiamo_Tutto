using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Spacchiamo {

	internal class UIAbilitiesAndWeaponsTooltipCallerScript : UIBaseTooltipCallerScript {

		protected internal UIAbilitiesAndWeaponsTooltipScript tooltip;

		private UIIMAGE uiImageCharacteristic;
		private Image image;

		internal UIIMAGE uiCharacteristic {

			get {

				return uiImageCharacteristic;

			}

		}

		protected internal override void Awake () {

			this.tooltip = GameObject.FindGameObjectWithTag (PARAMETERS.TOOLTIP).GetComponent <UIAbilitiesAndWeaponsTooltipScript> ();

			this.uiImageCharacteristic = UIIMAGE.NULL;
			this.image = this.GetComponent <Image> ();

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
				Debug.LogWarning ("TI STAI DIMENTICANDO UN TAG!\n" + this.name);
				break;

			}

		}

		internal void LoadCharacteristic (UIIMAGE passedCharacteristic) {

			this.uiImageCharacteristic = passedCharacteristic;

			switch (this.uiImageLogic) {

			case UIIMAGE.PASSIVE_ABILITY:
				switch (this.uiImageCharacteristic) {
				case UIIMAGE.NO_ABILITY:
					this.image.color = new Color (1f, 1f, 1f);	//WHITE
					break;
				case UIIMAGE.ABILITY_1:
					this.image.color = new Color (1f, 1f, 0f);	//YELLOW
					break;
				case UIIMAGE.ABILITY_2:
					this.image.color = new Color (0f, 1f, 1f);	//CYAN
					break;
				case UIIMAGE.ABILITY_3:
					this.image.color = new Color (1f, 0f, 1f);	//MAGENTA
					break;
				case UIIMAGE.ABILITY_4:
					this.image.color = new Color (192f/255f, 64f/255f, 0f);	//TYPE OF ORANGE
					break;
				case UIIMAGE.ABILITY_5:
					this.image.color = new Color (0.5f, 0.5f, 0.5f);	//GREY
					break;
				default:
					Debug.LogError ("NON RIESCO A CAPIRE CHE ABILITA' SONO");
					break;
				}
				break;
			case UIIMAGE.PRIMARY_ABILITY:
				switch (this.uiImageCharacteristic) {
				case UIIMAGE.NO_ABILITY:
					this.image.color = new Color (1f, 1f, 1f);	//WHITE
					break;
				case UIIMAGE.ABILITY_1:
					this.image.color = new Color (1f, 1f, 0f);	//YELLOW
					break;
				case UIIMAGE.ABILITY_2:
					this.image.color = new Color (0f, 1f, 1f);	//CYAN
					break;
				case UIIMAGE.ABILITY_3:
					this.image.color = new Color (1f, 0f, 1f);	//MAGENTA
					break;
				case UIIMAGE.ABILITY_4:
					this.image.color = new Color (192f/255f, 64f/255f, 0f);	//TYPE OF ORANGE
					break;
				case UIIMAGE.ABILITY_5:
					this.image.color = new Color (0.5f, 0.5f, 0.5f);	//GREY
					break;
				default:
					Debug.LogError ("NON RIESCO A CAPIRE CHE ABILITA' SONO");
					break;
				}
				break;
			case UIIMAGE.SECONDARY_ABILITY:
				switch (this.uiImageCharacteristic) {
				case UIIMAGE.NO_ABILITY:
					this.image.color = new Color (1f, 1f, 1f);	//WHITE
					break;
				case UIIMAGE.ABILITY_1:
					this.image.color = new Color (1f, 1f, 0f);	//YELLOW
					break;
				case UIIMAGE.ABILITY_2:
					this.image.color = new Color (0f, 1f, 1f);	//CYAN
					break;
				case UIIMAGE.ABILITY_3:
					this.image.color = new Color (1f, 0f, 1f);	//MAGENTA
					break;
				case UIIMAGE.ABILITY_4:
					this.image.color = new Color (192f/255f, 64f/255f, 0f);	//TYPE OF ORANGE
					break;
				case UIIMAGE.ABILITY_5:
					this.image.color = new Color (0.5f, 0.5f, 0.5f);	//GREY
					break;
				default:
					Debug.LogError ("NON RIESCO A CAPIRE CHE ABILITA' SONO");
					break;
				}
				break;
			case UIIMAGE.FIRST_WEAPON:
				switch (this.uiImageCharacteristic) {
				case UIIMAGE.WEAPON_1:
					this.image.color = new Color (1f, 1f, 0f);	//YELLOW
					break;
				case UIIMAGE.WEAPON_2:
					this.image.color = new Color (0f, 1f, 1f);	//CYAN
					break;
				case UIIMAGE.WEAPON_3:
					this.image.color = new Color (1f, 0f, 1f);	//MAGENTA
					break;
				default:
					Debug.LogError ("NON RIESCO A CAPIRE CHE ARMA SONO");
					break;
				}
				break;
			case UIIMAGE.SECOND_WEAPON:
				switch (this.uiImageCharacteristic) {
				case UIIMAGE.WEAPON_1:
					this.image.color = new Color (1f, 1f, 0f);	//YELLOW
					break;
				case UIIMAGE.WEAPON_2:
					this.image.color = new Color (0f, 1f, 1f);	//CYAN
					break;
				case UIIMAGE.WEAPON_3:
					this.image.color = new Color (1f, 0f, 1f);	//MAGENTA
					break;
				default:
					Debug.LogError ("NON RIESCO A CAPIRE CHE ARMA SONO");
					break;
				}
				break;
			default:
				Debug.LogError ("CARATTERISTICA NON CARICABILE");
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