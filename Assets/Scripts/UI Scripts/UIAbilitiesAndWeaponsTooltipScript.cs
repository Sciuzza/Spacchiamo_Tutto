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
			
			this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.VOID_STRING;
			this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.VOID_STRING;
			
		}
		
		protected internal override void SetDescription (UIIMAGE imageHovered) {
			
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
			default:
				Debug.LogError ("ERRORE!");	//PROVVISORIO
				break;
				
			}
			
		}
		
		protected internal override void UnSetDescription () {
			
			this.Start ();
			
		}

	}
		
}