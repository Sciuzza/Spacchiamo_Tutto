using UnityEngine;
using System.Collections;

namespace Spacchiamo {

	internal class UIFirstTooltipCallerScript : UIBaseTooltipCallerScript {

		private UIBaseTooltipScript tooltip;

		protected internal override void Awake () {

			this.tooltip = FindObjectOfType <UIBaseTooltipScript> ();

		}

		public override void SetTooltip () {

			this.tooltip.SetDescription (this.uiImageLogic);

		}

		public override void UnSetTooltip () {

		}
		
	}

}