using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Spacchiamo {

	internal class UIAbilitiesAndWeaponsTooltipCallerScript : UIBaseTooltipCallerScript {

		protected internal UIAbilitiesAndWeaponsTooltipScript tooltip;

		private ushort ability1Level = 0;
		private ushort ability2Level = 0;
		private ushort ability3Level = 0;
		private ushort ability4Level = 0;
		private ushort ability5Level = 0;

		private UIIMAGE uiImageCharacteristic;
		private Image image;

		internal ushort Ability1Level {

			get {

				return this.ability1Level;

			}

			set {

				this.ability1Level = value;

			}

		}

		internal ushort Ability2Level {

			get {

				return this.ability2Level;

			}

			set {

				this.ability2Level = value;

			}

		}

		internal ushort Ability3Level {

			get {

				return this.ability3Level;

			}

			set {

				this.ability3Level = value;

			}

		}

		internal ushort Ability4Level {

			get {

				return this.ability4Level;

			}

			set {

				this.ability4Level = value;

			}

		}

		internal ushort Ability5Level {

			get {

				return this.ability5Level;

			}

			set {

				this.ability5Level = value;

			}

		}

		internal UIIMAGE uiCharacteristic {

			get {

				return this.uiImageCharacteristic;

			}

		}

		protected internal override void Awake () {

			this.tooltip = GameObject.FindGameObjectWithTag (PARAMETERS.TOOLTIP).GetComponent <UIAbilitiesAndWeaponsTooltipScript> ();

			this.uiImageCharacteristic = UIIMAGE.NULL;
			this.image = this.GetComponent <Image> ();

		}

		protected internal override void Start () {

            switch (this.tag)
            {

                case PARAMETERS.PLAY:
                    this.uiImageLogic = UIIMAGE.PLAY_GAME;
                    break;
                default:
                    //Debug.LogWarning("TI STAI DIMENTICANDO UN TAG!\n" + this.name);
                    break;

            }

        }

        public void InitializingOwnLogic()
        {

            switch (this.tag)
            {

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
                default:
                    Debug.LogWarning("TI STAI DIMENTICANDO UN TAG!\n" + this.name);
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
				case UIIMAGE.NULL:
					this.image.color = new Color (1f, 1f, 1f);	//WHITE
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
				case UIIMAGE.NULL:
					this.image.color = new Color (1f, 1f, 1f);	//WHITE
					break;
				default:
					Debug.LogError ("NON RIESCO A CAPIRE CHE ARMA SONO");
					break;
				}
				break;
			default:
				Debug.LogError ("CARATTERISTICA NON CARICABILE\n" + this.uiImageLogic);
				break;

			}

		}

		internal void LevelUpAbility (UIIMAGE specificUIImage, ABILITY specificAbility) {

			switch (specificAbility) {

			case ABILITY.ABILITY_PASSIVE:

				switch (specificUIImage) {

				case UIIMAGE.ABILITY_1:
					ability1Level++;
					break;
				case UIIMAGE.ABILITY_2:
					ability2Level++;
					break;
				case UIIMAGE.ABILITY_3:
					ability3Level++;
					break;
				case UIIMAGE.ABILITY_4:
					ability4Level++;
					break;
				case UIIMAGE.ABILITY_5:
					ability5Level++;
					break;
				default:
					Debug.LogError ("NON RIESCO A CAPIRE QUALE ABILITA' PASSIVA DEVO LIVELLARE!");
					break;

				}

				break;

			case ABILITY.ABILITY_PRIMARY:

				switch (specificUIImage) {

				case UIIMAGE.ABILITY_1:
					ability1Level++;
					break;
				case UIIMAGE.ABILITY_2:
					ability2Level++;
					break;
				case UIIMAGE.ABILITY_3:
					ability3Level++;
					break;
				case UIIMAGE.ABILITY_4:
					ability4Level++;
					break;
				case UIIMAGE.ABILITY_5:
					ability5Level++;
					break;
				default:
					Debug.LogError ("NON RIESCO A CAPIRE QUALE ABILITA' PRIMARIA DEVO LIVELLARE!");
					break;

				}

				break;

			case ABILITY.ABILITY_SECONDARY:

				switch (specificUIImage) {

				case UIIMAGE.ABILITY_1:
					ability1Level++;
					break;
				case UIIMAGE.ABILITY_2:
					ability2Level++;
					break;
				case UIIMAGE.ABILITY_3:
					ability3Level++;
					break;
				case UIIMAGE.ABILITY_4:
					ability4Level++;
					break;
				case UIIMAGE.ABILITY_5:
					ability5Level++;
					break;
				default:
					Debug.LogError ("NON RIESCO A CAPIRE QUALE ABILITA' SECONDARIA DEVO LIVELLARE!");
					break;

				}

				break;
			
			default:
				Debug.LogError ("NON RIESCO A CAPIRE QUALE TIPOLOGIA DI ABILITA' DEVO LIVELLARE!");
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