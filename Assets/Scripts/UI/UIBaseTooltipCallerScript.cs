using UnityEngine;
using System.Collections;

namespace Spacchiamo {
	
	internal abstract class UIBaseTooltipCallerScript : MonoBehaviour {

		protected internal byte uiImageLogic;

		protected internal abstract void Awake ();

		protected internal virtual void Start () {

			switch (this.tag) {

			case PARAMETERS.PASSIVE:
				this.uiImageLogic = (byte)UIIMAGE.PASSIVE_ABILITY;
				break;
			case PARAMETERS.PRIMARY:
				this.uiImageLogic = (byte)UIIMAGE.PRIMARY_ABILITY;
				break;
			case PARAMETERS.SECONDARY:
				this.uiImageLogic = (byte)UIIMAGE.SECONDARY_ABILITY;
				break;
			default:
				Debug.LogWarning ("TI STAI DIMENTICANDO UN TAG!\n" + this.name);	//PROVVISORIO
				break;

			}

		}

		public abstract void SetTooltip ();

		public abstract void UnSetTooltip ();
		
	}

}