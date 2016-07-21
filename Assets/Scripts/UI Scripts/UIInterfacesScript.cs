namespace Spacchiamo {

	internal interface IHoverable {

		void SetTooltip ();

		void UnSetTooltip ();

	}

	internal interface ICharacteristicRecognizable {

		void SetCharacteristic (int passedCharacteristic);

	}

	internal interface IDeductLoad {

		void DeductCharacteristicToLoad ();

	}

	internal interface ICheckPlayability {

		void CheckPlayability ();

	}

	internal interface IDecreaseOnClick {

		void DecreaseNumberOfPoints ();

	}

}