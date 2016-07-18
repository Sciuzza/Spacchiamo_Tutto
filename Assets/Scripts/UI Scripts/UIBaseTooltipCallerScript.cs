using UnityEngine;
using System.Collections;

namespace Spacchiamo {
	
	internal abstract class UIBaseTooltipCallerScript : MonoBehaviour, IHoverable {

		protected internal UIIMAGE uiImageLogic;

		protected internal abstract void Awake ();

		protected internal abstract void Start ();

		public abstract void SetTooltip ();

		public abstract void UnSetTooltip ();
		
	}

}