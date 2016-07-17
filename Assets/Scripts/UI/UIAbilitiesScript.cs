using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Spacchiamo {
	
	internal abstract class UIAbilitiesScript : MonoBehaviour {

		private UISingleTooltipScript singleTooltip;

		protected internal virtual void Awake () {

			this.singleTooltip = this.GetComponentInChildren <UISingleTooltipScript> (true);

		}

		public virtual void SetAbilityTooltipVisible () {

			this.singleTooltip.gameObject.SetActive (true);

		}

		public virtual void SetAbilityTooltipInvisible () {

			this.singleTooltip.gameObject.SetActive (false);

		}
		
	}

}