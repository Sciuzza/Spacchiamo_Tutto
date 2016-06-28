using UnityEngine;
using System.Collections;

namespace ToBeDecided {																			//Va deciso un nome decente al Namespace

	public enum NUM : byte {ZERO, FOUR = 4};													//Serie di numeri costanti interi
	public enum PLAYER_INPUT : byte {UP_BUTTON, DOWN_BUTTON, LEFT_BUTTON, RIGHT_BUTTON};		//Lista dei possibili input del giocatore; è solo una lista numerica, nulla più
	public enum PLAYER_PHASE : byte {MOVING};													//Lista delle possibili fasi del giocatore; è solo una lista numerica, nulla più

	public class Interaction_Env : MonoBehaviour {

		private PLAYER_PHASE actualPlayerPhase;													//Variabile che conterrà l'effettiva fase del giocatore (numericamente parlando)
		private KeyCode[] playerInputKey;														//Riferimento ad enum Keycode, il quale assegna numeri ai tasti fisici di Input

		private void Awake () {

			actualPlayerPhase = PLAYER_PHASE.MOVING;											//Gli ho dato una fase a caso; non è detto che l'assegnamento resti in questo metodo
			playerInputKey = new KeyCode[(int)NUM.FOUR];										//Piccolo array di 4 input (praticamente le quattro direzioni del personaggio)


			//Segue l'assegnazione degli Input dall'enum Keycode; si possono segliere solo
			//quelli che Keycode prevede (non ci possiamo inventare gli input)

			/*playerInputKey [(int)PLAYER_INPUT.UP_BUTTON] = KeyCode.W;
			playerInputKey [(int)PLAYER_INPUT.DOWN_BUTTON] = KeyCode.S;
			playerInputKey [(int)PLAYER_INPUT.LEFT_BUTTON] = KeyCode.A;
			playerInputKey [(int)PLAYER_INPUT.RIGHT_BUTTON] = KeyCode.D;*/

		}

		private void Update () {

			switch (actualPlayerPhase) {
			case PLAYER_PHASE.MOVING:
				/*if (Input.GetKeyDown (playerInputKey [(int)PLAYER_INPUT.UP_BUTTON])			//controllo degli Input per Frame Update
				if (Input.GetKeyDown (playerInputKey [(int)PLAYER_INPUT.DOWN_BUTTON])
				if (Input.GetKeyDown (playerInputKey [(int)PLAYER_INPUT.LEFT_BUTTON])
				if (Input.GetKeyDown (playerInputKey [(int)PLAYER_INPUT.RIGHT_BUTTON])*/
				break;
			default:
				Debug.Log ("Qualcosa non sta funzionando.");
				break;
			}

		}

	}

}