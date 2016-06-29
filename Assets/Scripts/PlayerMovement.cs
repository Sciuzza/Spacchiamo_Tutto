using UnityEngine;
using System.Collections;

namespace ToBeDecided {																			//Va deciso un nome decente al Namespace

	public enum NUM : byte {ZERO, FOUR = 4};													//Serie di numeri costanti interi
	public enum BUTTON : byte {UP, DOWN, LEFT, RIGHT};											//Lista dei possibili input del giocatore; è solo una lista numerica, nulla più
	public enum PLAYER_PHASE : byte {MOVING};													//Lista delle possibili fasi del giocatore; è solo una lista numerica, nulla più

	public class PlayerMovement : MonoBehaviour {

		private const float DIST = 0.01f;														//Distanza AL QUADRATO

		private PLAYER_PHASE actualPlayerPhase;													//Variabile che conterrà l'effettiva fase del giocatore (numericamente parlando)
		private KeyCode[] playerInputKey;														//Riferimento ad enum Keycode, il quale assegna numeri ai tasti fisici di Input
		private Vector3 distance, direction;													//Vettori distanza e direzione (versore)
		private SCRIPTCHENONSO cellReference, newCellReference;									//Riferimenti alla propria cella ed alla cella desiderata

		private void Awake () {

			this.actualPlayerPhase = PLAYER_PHASE.MOVING;										//Gli ho dato una fase a caso; non è detto che l'assegnamento resti in questo metodo
			this.playerInputKey = new KeyCode[(int)NUM.FOUR];									//Piccolo array di 4 input (praticamente le quattro direzioni del personaggio)


			//Segue l'assegnazione degli Input dall'enum Keycode; si possono segliere solo
			//quelli che Keycode prevede (non ci possiamo inventare gli input)

			this.playerInputKey [(int)BUTTON.UP] = KeyCode.W;
			this.playerInputKey [(int)BUTTON.DOWN] = KeyCode.S;
			this.playerInputKey [(int)BUTTON.LEFT] = KeyCode.A;
			this.playerInputKey [(int)BUTTON.RIGHT] = KeyCode.D;

		}


		//Metodo per assegnare il riferimento alla propria cella
		public void AssignCellReference (SCRIPTCHENONSO comingCellReference) {

			this.cellReference = comingCellReference;

		}

		private void Update () {


			//Calcoli matematici per spostarsi dalla propria cella a quella voluta, sempre se sussista quella voluta
			if (this.newCellReference != null) {
				this.distance = this.newCellReference.transform.position - this.cellReference.transform.position;
				this.direction = this.distance.normalized;
				this.transform.position = this.transform.position + this.direction * Time.deltaTime;
				if (this.distance.sqrMagnitude < DIST) {
					this.transform.position = this.newCellReference.tranform.position;
					this.AssignCellReference (this.newCellReference);
					this.newCellReference = null;
				}
			}


			//Valutazione delle fasi del giocatore
			switch (this.actualPlayerPhase) {
			case PLAYER_PHASE.MOVING:
				//A seconda del comando desiderato, il giocatore si sposterà sulla griglia nella direzione desiderata
				if (Input.GetKeyDown (this.playerInputKey [(int)BUTTON.UP]))
					//this.newCellReference = this.cellReference.GetUpperCellReference ();								Logica di Marco
					//this.newCellReference = MetodoCheRitornaLaNuovaCellaSu(this.cellReference.y);						Logica di Cristiano
				if (Input.GetKeyDown (this.playerInputKey [(int)BUTTON.DOWN]))
					//this.newCellReference = this.cellReference.GetLowerCellReference ();								Logica di Marco
					//this.newCellReference = MetodoCheRitornaLaNuovaCellaGiù(this.cellReference.y);					Logica di Cristiano
				if (Input.GetKeyDown (this.playerInputKey [(int)BUTTON.LEFT]))
					//this.newCellReference = this.cellReference.GetLeftCellReference ();								Logica di Marco
					//this.newCellReference = MetodoCheRitornaLaNuovaCellaSinistra(this.cellReference.x);				Logica di Cristiano
				if (Input.GetKeyDown (this.playerInputKey [(int)BUTTON.RIGHT]))
					//this.newCellReference = this.cellReference.GetRightCellReference ();								Logica di Marco
					//this.newCellReference = MetodoCheRitornaLaNuovaCellaDestra(this.cellReference.x);					Logica di Cristiano
				break;
			default:
				Debug.Log ("Qualcosa non sta funzionando.");
				break;
			}
			
		}

	}

}


/*
 * Logica di Marco:
 * -il giocatore chiama la cella su cui sussiste, indicandogli la "direzione" desiderata
 * -la cella chiama la griglia, indicandogli la posizione matriciale utile per soddisfare la richiesta del player
 * -la griglia cerca la cella originariamente cercata dal player:
 * --se non esiste OPPURE non è "calpestabile" --> NULL
 * --se esiste ED è calpestabile --> Riferimento alla cella
 * -il risultato viene passato alla cella richiedente la propria cella compagna
 * -la cella restituisce il risultato al giocatore
 * FINE LOGICA
 */