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

		void RecordParameters ();

	}

	internal interface IDecreaseOnClick {

		void DecreaseNumberOfPoints ();

	}

	internal interface IIncreaseOnClick {

		void IncreaseNumberOfPoints ();

	}

}