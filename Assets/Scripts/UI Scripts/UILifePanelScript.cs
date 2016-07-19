using UnityEngine;
using System.Collections;

namespace Spacchiamo {

	internal class UILifePanelScript : MonoBehaviour {

		private UIHeartScript[] hearts;

		private void Awake () {

			this.hearts = this.GetComponentsInChildren <UIHeartScript> ();

		}

		internal void UISetLife (int life = 20) {

			foreach (UIHeartScript heartScript in this.hearts) {
				
				switch (life) {

				case 20:
					heartScript.SetFullHeart ();
					break;
				case 19:
					if (heartScript.name == "Heart 9") {
						heartScript.SetHalfHeart ();
					}
					break;
				case 18:
					if (heartScript.name == "Heart 9") {
						heartScript.SetEmptyHeart ();
					}
					break;
				case 17:
					if (heartScript.name == "Heart 9") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 8") {
						heartScript.SetHalfHeart ();
					}
					break;
				case 16:
					if (heartScript.name == "Heart 9") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 8") {
						heartScript.SetEmptyHeart ();
					}
					break;
				case 15:
					if (heartScript.name == "Heart 9") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 8") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 7") {
						heartScript.SetHalfHeart ();
					}
					break;
				case 14:
					if (heartScript.name == "Heart 9") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 8") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 7") {
						heartScript.SetEmptyHeart ();
					}
					break;
				case 13:
					if (heartScript.name == "Heart 9") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 8") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 7") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 6") {
						heartScript.SetHalfHeart ();
					}
					break;
				case 12:
					if (heartScript.name == "Heart 9") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 8") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 7") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 6") {
						heartScript.SetEmptyHeart ();
					}
					break;
				case 11:
					if (heartScript.name == "Heart 9") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 8") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 7") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 6") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 5") {
						heartScript.SetHalfHeart ();
					}
					break;
				case 10:
					if (heartScript.name == "Heart 9") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 8") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 7") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 6") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 5") {
						heartScript.SetEmptyHeart ();
					}
					break;
				case 9:
					if (heartScript.name == "Heart 9") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 8") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 7") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 6") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 5") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 4") {
						heartScript.SetHalfHeart ();
					}
					break;
				case 8:
					if (heartScript.name == "Heart 9") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 8") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 7") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 6") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 5") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 4") {
						heartScript.SetEmptyHeart ();
					}
					break;
				case 7:
					if (heartScript.name == "Heart 9") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 8") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 7") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 6") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 5") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 4") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 3") {
						heartScript.SetHalfHeart ();
					}
					break;
				case 6:
					if (heartScript.name == "Heart 9") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 8") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 7") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 6") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 5") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 4") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 3") {
						heartScript.SetEmptyHeart ();
					}
					break;
				case 5:
					if (heartScript.name == "Heart 9") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 8") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 7") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 6") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 5") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 4") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 3") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 2") {
						heartScript.SetHalfHeart ();
					}
					break;
				case 4:
					if (heartScript.name == "Heart 9") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 8") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 7") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 6") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 5") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 4") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 3") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 2") {
						heartScript.SetEmptyHeart ();
					}
					break;
				case 3:
					if (heartScript.name == "Heart 9") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 8") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 7") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 6") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 5") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 4") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 3") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 2") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 1") {
						heartScript.SetHalfHeart ();
					}
					break;
				case 2:
					if (heartScript.name == "Heart 9") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 8") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 7") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 6") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 5") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 4") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 3") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 2") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 1") {
						heartScript.SetEmptyHeart ();
					}
					break;
				case 1:
					if (heartScript.name == "Heart 9") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 8") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 7") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 6") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 5") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 4") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 3") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 2") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 1") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 0") {
						heartScript.SetHalfHeart ();
					}
					break;
				case 0:
					if (heartScript.name == "Heart 9") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 8") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 7") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 6") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 5") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 4") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 3") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 2") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 1") {
						heartScript.SetEmptyHeart ();
					}
					if (heartScript.name == "Heart 0") {
						heartScript.SetEmptyHeart ();
					}
					break;
				default:
					break;

				}

			}

		}

	}

}