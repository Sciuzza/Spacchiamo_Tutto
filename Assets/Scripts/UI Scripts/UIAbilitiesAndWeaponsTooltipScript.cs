using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Spacchiamo {

	internal class UIAbilitiesAndWeaponsTooltipScript : UIBaseTooltipScript {

		private Button playButton;
		
		protected internal override void Awake () {
			
			this.descriptionText = new Text[(int)TEXTPARAMETERS.LENGTH];
			this.descriptionText [(int)TEXTPARAMETERS.TITLE] = GameObject.FindGameObjectWithTag (PARAMETERS.T_TITLE).GetComponent <Text> ();
			this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION] = GameObject.FindGameObjectWithTag (PARAMETERS.T_DESCRIPTION).GetComponent <Text> ();

			this.playButton = GameObject.FindGameObjectWithTag (PARAMETERS.PLAY).GetComponent <Button> ();
			
		}
		
		protected internal override void Start () {
			
			this.UnSetDescription ();
			
		}
		
		protected internal override void SetDescription (UIIMAGE imageHovered, ABILITY specificAbility = ABILITY.ABILITY_VOID, HAND specificWeapon = HAND.HAND_VOID) {
			
			switch (imageHovered) {
			
			case UIIMAGE.PASSIVE_ABILITY:
				this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.PASSIVE_ABILITY_TITLE;
				this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.PASSIVE_ABILITY_DESCRIPTION;
				break;
			case UIIMAGE.PRIMARY_ABILITY:
				this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.PRIMARY_ABILITY_TITLE;
				this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.PRIMARY_ABILITY_DESCRIPTION;
				break;
			case UIIMAGE.SECONDARY_ABILITY:
				this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.SECONDARY_ABILITY_TITLE;
				this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.SECONDARY_ABILITY_DESCRIPTION;
				break;
			case UIIMAGE.POINTS_FOR_ABILITY:
				this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.POINTS_FOR_ABILITY_TITLE;
				this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.POINTS_FOR_ABILITY_DESCRIPTION;
				break;
			case UIIMAGE.FIRST_WEAPON:
				this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.WEAPON1_TITLE;
				this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.WEAPON1_DESCRIPTION;
				break;
			case UIIMAGE.SECOND_WEAPON:
				this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.WEAPON2_TITLE;
				this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.WEAPON2_DESCRIPTION;
				break;
			case UIIMAGE.PLAY_GAME:
				if (this.playButton.interactable) {
					this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.PLAY_TITLE;
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.PLAY_DESCRIPTION;
				} else {
					this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.NO_PLAY_TITLE;
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.NO_PLAY_DESCRIPTION;
				}
				break;
			case UIIMAGE.NO_ABILITY:
				this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.NO_ABILITY_TITLE;
				switch (specificAbility) {
				case ABILITY.ABILITY_PASSIVE:
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.NO_PA_ABILITY_DESCRIPTION;
					break;
				case ABILITY.ABILITY_PRIMARY:
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.NO_PR_ABILITY_DESCRIPTION;
					break;
				case ABILITY.ABILITY_SECONDARY:
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.NO_SE_ABILITY_DESCRIPTION;
					break;
				default:
					Debug.LogWarning ("NESSUNA SPECIFICA ABILITA'");
					break;
				}
				break;
			case UIIMAGE.ABILITY_1:
				switch (specificAbility) {
				case ABILITY.ABILITY_PASSIVE:
					this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.PA_ABILITY_1_TITLE;
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.PA_ABILITY_1_DESCRIPTION;
					break;
				case ABILITY.ABILITY_PRIMARY:
					this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.PR_ABILITY_1_TITLE;
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.PR_ABILITY_1_DESCRIPTION;
					break;
				case ABILITY.ABILITY_SECONDARY:
					this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.SE_ABILITY_1_TITLE;
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.SE_ABILITY_1_DESCRIPTION;
					break;
				default:
					Debug.LogWarning ("NESSUNA SPECIFICA ABILITA'");
					break;
				}
				break;
			case UIIMAGE.ABILITY_2:
				switch (specificAbility) {
				case ABILITY.ABILITY_PASSIVE:
					this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.PA_ABILITY_2_TITLE;
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.PA_ABILITY_2_DESCRIPTION;
					break;
				case ABILITY.ABILITY_PRIMARY:
					this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.PR_ABILITY_2_TITLE;
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.PR_ABILITY_2_DESCRIPTION;
					break;
				case ABILITY.ABILITY_SECONDARY:
					this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.SE_ABILITY_2_TITLE;
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.SE_ABILITY_2_DESCRIPTION;
					break;
				default:
					Debug.LogWarning ("NESSUNA SPECIFICA ABILITA'");
					break;
				}
				break;
			case UIIMAGE.ABILITY_3:
				switch (specificAbility) {
				case ABILITY.ABILITY_PASSIVE:
					this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.PA_ABILITY_3_TITLE;
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.PA_ABILITY_3_DESCRIPTION;
					break;
				case ABILITY.ABILITY_PRIMARY:
					this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.PR_ABILITY_3_TITLE;
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.PR_ABILITY_3_DESCRIPTION;
					break;
				case ABILITY.ABILITY_SECONDARY:
					this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.SE_ABILITY_3_TITLE;
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.SE_ABILITY_3_DESCRIPTION;
					break;
				default:
					Debug.LogWarning ("NESSUNA SPECIFICA ABILITA'");
					break;
				}
				break;
			case UIIMAGE.ABILITY_4:
				switch (specificAbility) {
				case ABILITY.ABILITY_PASSIVE:
					this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.PA_ABILITY_4_TITLE;
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.PA_ABILITY_4_DESCRIPTION;
					break;
				case ABILITY.ABILITY_PRIMARY:
					this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.PR_ABILITY_4_TITLE;
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.PR_ABILITY_4_DESCRIPTION;
					break;
				case ABILITY.ABILITY_SECONDARY:
					this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.SE_ABILITY_4_TITLE;
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.SE_ABILITY_4_DESCRIPTION;
					break;
				default:
					Debug.LogWarning ("NESSUNA SPECIFICA ABILITA'");
					break;
				}
				break;
			case UIIMAGE.ABILITY_5:
				switch (specificAbility) {
				case ABILITY.ABILITY_PASSIVE:
					this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.PA_ABILITY_5_TITLE;
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.PA_ABILITY_5_DESCRIPTION;
					break;
				case ABILITY.ABILITY_PRIMARY:
					this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.PR_ABILITY_5_TITLE;
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.PR_ABILITY_5_DESCRIPTION;
					break;
				case ABILITY.ABILITY_SECONDARY:
					this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.SE_ABILITY_5_TITLE;
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.SE_ABILITY_5_DESCRIPTION;
					break;
				default:
					Debug.LogWarning ("NESSUNA SPECIFICA ABILITA'");
					break;
				}
				break;
			case UIIMAGE.WEAPON_1:
				this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.WEAPON_N_1_TITLE;
				switch (specificWeapon) {
				case HAND.HAND_ONE:
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.WEAPON_N_1_H_1_DESCRIPTION;
					break;
				case HAND.HAND_TWO:
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.WEAPON_N_1_H_2_DESCRIPTION;
					break;
				default:
					Debug.LogWarning ("NESSUNA SPECIFICA MANO");
					break;
				}
				break;
			case UIIMAGE.WEAPON_2:
				this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.WEAPON_N_2_TITLE;
				switch (specificWeapon) {
				case HAND.HAND_ONE:
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.WEAPON_N_2_H_1_DESCRIPTION;
					break;
				case HAND.HAND_TWO:
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.WEAPON_N_2_H_2_DESCRIPTION;
					break;
				default:
					Debug.LogWarning ("NESSUNA SPECIFICA MANO");
					break;
				}
				break;
			case UIIMAGE.WEAPON_3:
				this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.WEAPON_N_3_TITLE;
				switch (specificWeapon) {
				case HAND.HAND_ONE:
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.WEAPON_N_3_H_1_DESCRIPTION;
					break;
				case HAND.HAND_TWO:
					this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.WEAPON_N_3_H_2_DESCRIPTION;
					break;
				default:
					Debug.LogWarning ("NESSUNA SPECIFICA MANO");
					break;
				}
				break;
			default:
				Debug.LogError ("ERRORE!");
				break;
				
			}
			
		}
		
		protected internal override void UnSetDescription () {
			
			this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.VOID_STRING;
			this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.VOID_STRING;
			
		}

	}
		
}