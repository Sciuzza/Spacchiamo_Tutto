using UnityEngine;
using System.Collections;

namespace Spacchiamo {																			//Va deciso un nome decente al Namespace

	public enum NUM : byte {ZERO, FOUR = 4};													//Serie di numeri costanti interi
	public enum BUTTON : byte {UP, DOWN, LEFT, RIGHT};											//Lista dei possibili input del giocatore; è solo una lista numerica, nulla più
	public enum PLAYER_PHASE : byte {MOVING};													//Lista delle possibili fasi del giocatore; è solo una lista numerica, nulla più

	public class PlayerMovement : MonoBehaviour {

		private const float DIST = 1.01f;														//Distanza AL QUADRATO
        

		private PLAYER_PHASE actualPlayerPhase;													//Variabile che conterrà l'effettiva fase del giocatore (numericamente parlando)
		private KeyCode[] playerInputKey;														//Riferimento ad enum Keycode, il quale assegna numeri ai tasti fisici di Input
		private Vector2 distance = new Vector2 (-100,-100), direction;													//Vettori distanza e direzione (versore)
		private Cell_Interaction cellReference, newCellReference;                               //Riferimenti alla propria cella ed alla cella desiderata
        private bool isMoving = false;
        Grid_Manager gridManagerLinking;                                                                                  

		private void Awake () {

			this.actualPlayerPhase = PLAYER_PHASE.MOVING;										//Gli ho dato una fase a caso; non è detto che l'assegnamento resti in questo metodo
			this.playerInputKey = new KeyCode[(int)NUM.FOUR];									//Piccolo array di 4 input (praticamente le quattro direzioni del personaggio)
            
            
            gridManagerLinking = GameObject.Find("Grid Manager").GetComponent<Grid_Manager>();
            //Segue l'assegnazione degli Input dall'enum Keycode; si possono segliere solo
            //quelli che Keycode prevede (non ci possiamo inventare gli input)

            this.playerInputKey [(int)BUTTON.UP] = KeyCode.W;
			this.playerInputKey [(int)BUTTON.DOWN] = KeyCode.S;
			this.playerInputKey [(int)BUTTON.LEFT] = KeyCode.A;
			this.playerInputKey [(int)BUTTON.RIGHT] = KeyCode.D;

		}


		//Metodo per assegnare il riferimento alla propria cella
		public void AssignCellReference (Cell_Interaction comingCellReference) {

			this.cellReference = comingCellReference;

		}

		private void Update () {


            //Calcoli matematici per spostarsi dalla propria cella a quella voluta, sempre se sussista quella voluta

            if (isMoving)
            {

                    this.distance = this.newCellReference.transform.position - this.transform.position;
                    this.direction = this.distance.normalized;
                    this.transform.position = (Vector2)this.transform.position + this.direction * Time.deltaTime;
                
                if (this.distance.magnitude < 0.1f)
                {
                    this.transform.position = new Vector3(this.newCellReference.transform.position.x, this.newCellReference.transform.position.y, 0);
                    this.AssignCellReference(this.newCellReference);
                    this.newCellReference = null;
                    isMoving = false;
                }
            }


			//Valutazione delle fasi del giocatore
			if (this.actualPlayerPhase == PLAYER_PHASE.MOVING && !isMoving) {

                //A seconda del comando desiderato, il giocatore si sposterà sulla griglia nella direzione desiderata
                if (Input.GetKey(this.playerInputKey[(int)BUTTON.UP]))
                {
                    this.newCellReference = gridManagerLinking.CheckingUpCell(this.cellReference);
                    if (this.newCellReference != null)
                        isMoving = true;
                    else
                        Debug.Log("Cannot Move there");
                }
                else if (Input.GetKey(this.playerInputKey[(int)BUTTON.DOWN]))
                {
                    this.newCellReference = gridManagerLinking.CheckingDownCell(this.cellReference);
                    if (this.newCellReference != null)
                        isMoving = true;
                    else
                        Debug.Log("Cannot Move there");
                }
                else if (Input.GetKey(this.playerInputKey[(int)BUTTON.LEFT]))
                {
                    this.newCellReference = gridManagerLinking.CheckingLeftCell(this.cellReference);
                    if (this.newCellReference != null)
                        isMoving = true;
                    else
                        Debug.Log("Cannot Move there");
                }
                else if (Input.GetKey(this.playerInputKey[(int)BUTTON.RIGHT]))
                {
                    this.newCellReference = gridManagerLinking.CheckingRightCell(this.cellReference);
                    if (this.newCellReference != null)
                        isMoving = true;
                    else
                        Debug.Log("Cannot Move there");
                }
                
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