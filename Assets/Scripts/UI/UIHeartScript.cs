using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Spacchiamo {

	internal class UIHeartScript : MonoBehaviour {

		private Image heartSprite;

		private void Awake () {

			this.heartSprite = this.gameObject.GetComponent <Image> ();

		}

		internal void SetFullHeart () {

			this.heartSprite.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		}

		internal void SetHalfHeart () {

			this.heartSprite.color = new Color (1.0f, 1.0f, 1.0f, 0.5f);

		}

		internal void SetEmptyHeart () {

			this.heartSprite.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);

		}
		
	}

}