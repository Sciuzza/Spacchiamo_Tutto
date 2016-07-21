using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Spacchiamo {

	internal class UIAbilitiesAndWeaponsPointsScript : MonoBehaviour, IDecreaseOnClick {

		private ushort numberOfPoints;
		private Text pointsText;
		private UIAbilitiesAndWeaponsUpgradeButtonScript[] upgradeButtons;

		internal ushort Points {

			set {

				this.numberOfPoints = value;

			}

			get {

				return this.numberOfPoints;

			}

		}

		private void Awake () {

			this.pointsText = this.GetComponentInChildren <Text> ();
			this.upgradeButtons = this.GetComponentInParent <Canvas> ().gameObject.GetComponentsInChildren <UIAbilitiesAndWeaponsUpgradeButtonScript> (true);

		}

		//METODO TEMPORANEO
		private void Start () {

			this.Points = 10;
			this.UpdateUpgradeVisibility ();

		}

		internal void UpdateUpgradeVisibility () {

			this.pointsText.text = this.numberOfPoints.ToString ();

			if (this.numberOfPoints > 0) {
				
				foreach (var upgradeGO in this.upgradeButtons)
					upgradeGO.gameObject.SetActive (true);

			} else {

				foreach (var upgradeGO in this.upgradeButtons)
					upgradeGO.gameObject.SetActive (false);

			}

		}

		public void DecreaseNumberOfPoints () {

			if (this.numberOfPoints > 0) {
				
				this.numberOfPoints--;
				this.UpdateUpgradeVisibility ();

			}

		}
		
	}

}