using UnityEngine;
using System.Collections;

namespace Spacchiamo {

	public class UILifePanelScript : MonoBehaviour {

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
					
					if (heartScript.name == "Heart 9")
						heartScript.SetHalfHeart ();

					else 
						heartScript.SetFullHeart ();
					
					break;

				case 18:
					
					if (heartScript.name == "Heart 9")
						heartScript.SetEmptyHeart ();
					
					else
						heartScript.SetFullHeart ();
					
					break;

				case 17:
					
					if ( (heartScript.name == "Heart 9") || (heartScript.name == "Heart 8") ) {
						
						if (heartScript.name == "Heart 8")
							heartScript.SetHalfHeart ();

						else
							heartScript.SetEmptyHeart ();

					} else
						heartScript.SetFullHeart ();
					
					break;

				case 16:
					
					if ( (heartScript.name == "Heart 9") || (heartScript.name == "Heart 8") )
						heartScript.SetEmptyHeart ();

					else
						heartScript.SetFullHeart ();
					
					break;

				case 15:
					
					if ( (heartScript.name == "Heart 9") || (heartScript.name == "Heart 8") || (heartScript.name == "Heart 7") ) {

						if (heartScript.name == "Heart 7")
							heartScript.SetHalfHeart ();

						else
							heartScript.SetEmptyHeart ();

					} else
						heartScript.SetFullHeart ();
					
					break;

				case 14:
					
					if ( (heartScript.name == "Heart 9") || (heartScript.name == "Heart 8") || (heartScript.name == "Heart 7") )
						heartScript.SetEmptyHeart ();

					else
						heartScript.SetFullHeart ();
					
					break;

				case 13:
					
					if ( (heartScript.name == "Heart 9") || (heartScript.name == "Heart 8") || (heartScript.name == "Heart 7") || (heartScript.name == "Heart 6") ) {

						if (heartScript.name == "Heart 6")
							heartScript.SetHalfHeart ();

						else
							heartScript.SetEmptyHeart ();

					} else
						heartScript.SetFullHeart ();
					
					break;

				case 12:
					
					if ( (heartScript.name == "Heart 9") || (heartScript.name == "Heart 8") || (heartScript.name == "Heart 7") || (heartScript.name == "Heart 6") )
						heartScript.SetEmptyHeart ();

					else
						heartScript.SetFullHeart ();
					
					break;

				case 11:
					
					if ( (heartScript.name == "Heart 9") || (heartScript.name == "Heart 8") || (heartScript.name == "Heart 7") || (heartScript.name == "Heart 6") || (heartScript.name == "Heart 5") ) {

						if (heartScript.name == "Heart 5")
							heartScript.SetHalfHeart ();

						else
							heartScript.SetEmptyHeart ();

					} else
						heartScript.SetFullHeart ();
					
					break;

				case 10:
					
					if ( (heartScript.name == "Heart 9") || (heartScript.name == "Heart 8") || (heartScript.name == "Heart 7") || (heartScript.name == "Heart 6") || (heartScript.name == "Heart 5") )
						heartScript.SetEmptyHeart ();

					else
						heartScript.SetFullHeart ();
					
					break;

				case 9:
					
					if ( !(heartScript.name == "Heart 0") && !(heartScript.name == "Heart 1") && !(heartScript.name == "Heart 2") && !(heartScript.name == "Heart 3") ) {

						if (heartScript.name == "Heart 4")
							heartScript.SetHalfHeart ();

						else
							heartScript.SetEmptyHeart ();

					} else
						heartScript.SetFullHeart ();
					
					break;

				case 8:
					
					if ( !(heartScript.name == "Heart 0") && !(heartScript.name == "Heart 1") && !(heartScript.name == "Heart 2") && !(heartScript.name == "Heart 3") )
						heartScript.SetEmptyHeart ();

					else
						heartScript.SetFullHeart ();
					
					break;

				case 7:
					
					if ( !(heartScript.name == "Heart 0") && !(heartScript.name == "Heart 1") && !(heartScript.name == "Heart 2") ) {

						if (heartScript.name == "Heart 3")
							heartScript.SetHalfHeart ();

						else
							heartScript.SetEmptyHeart ();

					} else
						heartScript.SetFullHeart ();
					
					break;

				case 6:
					
					if ( !(heartScript.name == "Heart 0") && !(heartScript.name == "Heart 1") && !(heartScript.name == "Heart 2") )
						heartScript.SetEmptyHeart ();

					else
						heartScript.SetFullHeart ();
					
					break;

				case 5:
					
					if ( !(heartScript.name == "Heart 0") && !(heartScript.name == "Heart 1") ) {

						if (heartScript.name == "Heart 2")
							heartScript.SetHalfHeart ();

						else
							heartScript.SetEmptyHeart ();

					} else
						heartScript.SetFullHeart ();
					
					break;

				case 4:
					
					if ( !(heartScript.name == "Heart 0") && !(heartScript.name == "Heart 1") )
						heartScript.SetEmptyHeart ();

					else
						heartScript.SetFullHeart ();
					
					break;

				case 3:
					
					if ( !(heartScript.name == "Heart 0") ) {

						if (heartScript.name == "Heart 1")
							heartScript.SetHalfHeart ();

						else
							heartScript.SetEmptyHeart ();

					} else
						heartScript.SetFullHeart ();
					
					break;

				case 2:
					
					if ( !(heartScript.name == "Heart 0") )
						heartScript.SetEmptyHeart ();

					else
						heartScript.SetFullHeart ();
					
					break;

				case 1:
					
					if (heartScript.name == "Heart 0")
						heartScript.SetHalfHeart ();

					else
						heartScript.SetEmptyHeart ();
					
					break;

				case 0:
					
					heartScript.SetEmptyHeart ();

					break;

				default:
					
					Debug.LogError ("NON RIESCO A MODIFICARE IL FEEDBACK SULLA VITA!");

					break;

				}

			}

		}

	}

}