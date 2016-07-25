using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Spacchiamo {

	internal class UIHUDTooltipScript : UIBaseTooltipScript {

		protected internal override void Awake () {

			this.descriptionText = new Text[(int)TEXTPARAMETERS.LENGTH];
			this.descriptionText [(int)TEXTPARAMETERS.TITLE] = GameObject.FindGameObjectWithTag (PARAMETERS.T_TITLE).GetComponent <Text> ();
			this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION] = GameObject.FindGameObjectWithTag (PARAMETERS.T_DESCRIPTION).GetComponent <Text> ();

		}

		protected internal override void Start () {

			this.UnSetDescription ();
			this.gameObject.SetActive (false);

		}

		protected internal override void SetDescription (UIIMAGE imageHovered, ABILITY specificAbility = ABILITY.ABILITY_VOID, HAND specificHand = HAND.HAND_VOID) {

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
			case UIIMAGE.POTION:
				this.descriptionText [(int)TEXTPARAMETERS.TITLE].text = PARAMETERS.POTION_TITLE;
				this.descriptionText [(int)TEXTPARAMETERS.DESCRIPTION].text = PARAMETERS.POTION_DESCRIPTION;
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