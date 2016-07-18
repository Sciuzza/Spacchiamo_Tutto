using System.Collections;

namespace Spacchiamo {

	internal enum TEXTPARAMETERS : byte {TITLE, DESCRIPTION, LENGTH};
	internal enum UIIMAGE : byte {PASSIVE_ABILITY, PRIMARY_ABILITY, SECONDARY_ABILITY, POINTS_FOR_ABILITY, FIRST_WEAPON, SECOND_WEAPON, PLAY_GAME};

	internal static class PARAMETERS {


		internal const string VOID_STRING = "";


		internal const string TOOLTIP = "UITO";
		internal const string T_TITLE = "UITI";
		internal const string T_DESCRIPTION = "UIDE";


		internal const string PASSIVE = "UIPA";
		internal const string PRIMARY = "UIPR";
		internal const string SECONDARY = "UISE";
		internal const string POINTS = "UIPO";


		internal const string WEAPON1 = "UIFW";
		internal const string WEAPON2 = "UISW";


		internal const string PLAY = "UIPL";


		internal const string PASSIVE_ABILITY_TITLE = "PASSIVE ABILITY";
		internal const string PRIMARY_ABILITY_TITLE = "PRIMARY ABILITY";
		internal const string SECONDARY_ABILITY_TITLE = "SECONDARY ABILITY";
		internal const string POINTS_FOR_ABILITY_TITLE = "POINTS";


		internal const string WEAPON1_TITLE = "PRIMARY WEAPON";
		internal const string WEAPON2_TITLE = "SECONDARY WEAPON";


		internal const string NO_PLAY_TITLE = "NO PLAY";
		internal const string PLAY_TITLE = "PLAY";


		internal const string PASSIVE_ABILITY_DESCRIPTION = "Passive stuff";
		internal const string PRIMARY_ABILITY_DESCRIPTION = "Primary stuff";
		internal const string SECONDARY_ABILITY_DESCRIPTION = "Secondary stuff";
		internal const string POINTS_FOR_ABILITY_DESCRIPTION = "Points stuff";


		internal const string WEAPON1_DESCRIPTION = "Primary weapon stuff";
		internal const string WEAPON2_DESCRIPTION = "Secondary weapon stuff";


		internal const string NO_PLAY_DESCRIPTION = "No play stuff";
		internal const string PLAY_DESCRIPTION = "Play stuff";


	}

}