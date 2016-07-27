using System.Collections;

namespace Spacchiamo {

	internal enum TEXTPARAMETERS : byte {TITLE, DESCRIPTION, LENGTH};
	internal enum UIIMAGE : byte {NULL, PASSIVE_ABILITY, PRIMARY_ABILITY, SECONDARY_ABILITY, POINTS_FOR_ABILITY, FIRST_WEAPON, SECOND_WEAPON, PLAY_GAME, NO_ABILITY, ABILITY_1, ABILITY_2, ABILITY_3, ABILITY_4, ABILITY_5, WEAPON_1, WEAPON_2, WEAPON_3, POTION};
	internal enum ABILITY : byte {ABILITY_PASSIVE, ABILITY_PRIMARY, ABILITY_SECONDARY, ABILITY_VOID};
	internal enum HAND : byte {HAND_ONE = 1, HAND_TWO, HAND_VOID};


	public class AbilitiesAndWeaponsInitialization {

		internal ushort passiveAbility1Level = 0;
		internal ushort passiveAbility2Level = 0;
		internal ushort passiveAbility3Level = 0;
		internal ushort passiveAbility4Level = 0;
		internal ushort passiveAbility5Level = 0;

		internal ushort primaryAbility1Level = 0;
		internal ushort primaryAbility2Level = 0;
		internal ushort primaryAbility3Level = 0;
		internal ushort primaryAbility4Level = 0;
		internal ushort primaryAbility5Level = 0;

		internal ushort secondaryAbility1Level = 0;
		internal ushort secondaryAbility2Level = 0;
		internal ushort secondaryAbility3Level = 0;
		internal ushort secondaryAbility4Level = 0;
		internal ushort secondaryAbility5Level = 0;

		internal ushort levelUpPoints = 0;

		internal UIIMAGE passiveAbilityUICharacteristic = UIIMAGE.NO_ABILITY;
		internal UIIMAGE primaryAbilityUICharacteristic = UIIMAGE.NO_ABILITY;
		internal UIIMAGE secondaryAbilityUICharacteristic = UIIMAGE.NO_ABILITY;

		internal UIIMAGE primaryWeaponUICharacteristic = UIIMAGE.NULL;
		internal UIIMAGE secondaryWeaponUICharacteristic = UIIMAGE.NULL;

	}


	internal static class PARAMETERS {


		internal const string VOID_STRING = "";


		internal const string TOOLTIP = "UITO";
		internal const string T_TITLE = "UITI";
		internal const string T_DESCRIPTION = "UIDE";


		internal const string PASSIVE = "UIPA";
		internal const string PRIMARY = "UIPR";
		internal const string SECONDARY = "UISE";
		internal const string POINTS = "UIPO";


		internal const string ABILITY_N_0 = "UIA0";
		internal const string ABILITY_N_1 = "UIA1";
		internal const string ABILITY_N_2 = "UIA2";
		internal const string ABILITY_N_3 = "UIA3";
		internal const string ABILITY_N_4 = "UIA4";
		internal const string ABILITY_N_5 = "UIA5";


		internal const string UPGRADE = "UIUP";


		internal const string WEAPON1 = "UIFW";
		internal const string WEAPON2 = "UISW";


		internal const string WEAPON_N_1 = "UIW1";
		internal const string WEAPON_N_2 = "UIW2";
		internal const string WEAPON_N_3 = "UIW3";


		internal const string PLAY = "UIPL";


		internal const string POTION_TAG = "UIPT";


		internal const string PASSIVE_ABILITY_TITLE = "PASSIVE ABILITY";
		internal const string PRIMARY_ABILITY_TITLE = "PRIMARY ABILITY";
		internal const string SECONDARY_ABILITY_TITLE = "SECONDARY ABILITY";
		internal const string POINTS_FOR_ABILITY_TITLE = "POINTS";


		internal const string NO_ABILITY_TITLE = "NO ABILITY";
		internal const string PA_ABILITY_1_TITLE = "PASSIVE ABILITY 1";
		internal const string PA_ABILITY_2_TITLE = "PASSIVE ABILITY 2";
		internal const string PA_ABILITY_3_TITLE = "PASSIVE ABILITY 3";
		internal const string PA_ABILITY_4_TITLE = "PASSIVE ABILITY 4";
		internal const string PA_ABILITY_5_TITLE = "PASSIVE ABILITY 5";
		internal const string PR_ABILITY_1_TITLE = "PRIMARY ABILITY 1";
		internal const string PR_ABILITY_2_TITLE = "PRIMARY ABILITY 2";
		internal const string PR_ABILITY_3_TITLE = "PRIMARY ABILITY 3";
		internal const string PR_ABILITY_4_TITLE = "PRIMARY ABILITY 4";
		internal const string PR_ABILITY_5_TITLE = "PRIMARY ABILITY 5";
		internal const string SE_ABILITY_1_TITLE = "SECONDARY ABILITY 1";
		internal const string SE_ABILITY_2_TITLE = "SECONDARY ABILITY 2";
		internal const string SE_ABILITY_3_TITLE = "SECONDARY ABILITY 3";
		internal const string SE_ABILITY_4_TITLE = "SECONDARY ABILITY 4";
		internal const string SE_ABILITY_5_TITLE = "SECONDARY ABILITY 5";


		internal const string WEAPON1_TITLE = "PRIMARY WEAPON";
		internal const string WEAPON2_TITLE = "SECONDARY WEAPON";


		internal const string WEAPON_N_1_TITLE = "WEAPON 1";
		internal const string WEAPON_N_2_TITLE = "WEAPON 2";
		internal const string WEAPON_N_3_TITLE = "WEAPON 3";


		internal const string NO_PLAY_TITLE = "NO PLAY";
		internal const string PLAY_TITLE = "PLAY";


		internal const string POTION_TITLE = "POTION";


		internal const string PASSIVE_ABILITY_DESCRIPTION = "Passive stuff";
		internal const string PRIMARY_ABILITY_DESCRIPTION = "Primary stuff";
		internal const string SECONDARY_ABILITY_DESCRIPTION = "Secondary stuff";
		internal const string POINTS_FOR_ABILITY_DESCRIPTION = "Points stuff";


		internal const string NO_PA_ABILITY_DESCRIPTION = "No passive ability stuff";
		internal const string NO_PR_ABILITY_DESCRIPTION = "No primary ability stuff";
		internal const string NO_SE_ABILITY_DESCRIPTION = "No secondary ability stuff";
		internal const string PA_ABILITY_1_DESCRIPTION = "Passive ability 1 stuff";
		internal const string PA_ABILITY_2_DESCRIPTION = "Passive ability 2 stuff";
		internal const string PA_ABILITY_3_DESCRIPTION = "Passive ability 3 stuff";
		internal const string PA_ABILITY_4_DESCRIPTION = "Passive ability 4 stuff";
		internal const string PA_ABILITY_5_DESCRIPTION = "Passive ability 5 stuff";
		internal const string PR_ABILITY_1_DESCRIPTION = "Primary ability 1 stuff";
		internal const string PR_ABILITY_2_DESCRIPTION = "Primary ability 2 stuff";
		internal const string PR_ABILITY_3_DESCRIPTION = "Primary ability 3 stuff";
		internal const string PR_ABILITY_4_DESCRIPTION = "Primary ability 4 stuff";
		internal const string PR_ABILITY_5_DESCRIPTION = "Primary ability 5 stuff";
		internal const string SE_ABILITY_1_DESCRIPTION = "Secondary ability 1 stuff";
		internal const string SE_ABILITY_2_DESCRIPTION = "Secondary ability 2 stuff";
		internal const string SE_ABILITY_3_DESCRIPTION = "Secondary ability 3 stuff";
		internal const string SE_ABILITY_4_DESCRIPTION = "Secondary ability 4 stuff";
		internal const string SE_ABILITY_5_DESCRIPTION = "Secondary ability 5 stuff";


		internal const string WEAPON1_DESCRIPTION = "Primary weapon stuff";
		internal const string WEAPON2_DESCRIPTION = "Secondary weapon stuff";


		internal const string WEAPON_N_1_H_1_DESCRIPTION = "Weapon 1 hand 1 stuff";
		internal const string WEAPON_N_2_H_1_DESCRIPTION = "Weapon 2 hand 1 stuff";
		internal const string WEAPON_N_3_H_1_DESCRIPTION = "Weapon 3 hand 1 stuff";
		internal const string WEAPON_N_1_H_2_DESCRIPTION = "Weapon 1 hand 2 stuff";
		internal const string WEAPON_N_2_H_2_DESCRIPTION = "Weapon 2 hand 2 stuff";
		internal const string WEAPON_N_3_H_2_DESCRIPTION = "Weapon 3 hand 2 stuff";


		internal const string NO_PLAY_DESCRIPTION = "No play stuff";
		internal const string PLAY_DESCRIPTION = "Play stuff";


		internal const string POTION_DESCRIPTION = "Potion stuff";


	}

}