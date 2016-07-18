using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Spacchiamo {

	internal abstract class UIBaseTooltipScript : MonoBehaviour {

		protected internal Text[] descriptionText;

		protected internal abstract void Awake ();

		protected internal abstract void Start ();

		protected internal abstract void SetDescription (UIIMAGE imageHovered);

		protected internal abstract void UnSetDescription ();
		
	}

}