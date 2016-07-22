using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Spacchiamo {

	internal class UIHeartScript : MonoBehaviour {

		private Image heartSprite;

		private void Awake () {

			this.heartSprite = this.GetComponent <Image> ();

		}

		internal void SetFullHeart () {

			this.heartSprite.fillAmount = 1.0f;
		}

		internal void SetHalfHeart () {

			this.heartSprite.fillAmount = 0.5f;

		}

		internal void SetEmptyHeart () {

			this.heartSprite.fillAmount = 0.0f;

		}
		
	}

}