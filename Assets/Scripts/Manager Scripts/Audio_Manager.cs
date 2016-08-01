using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class Audio_Manager : MonoBehaviour
    {

		public AudioSource[] soundtrack;

        [HideInInspector]
        public static Audio_Manager instance = null;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }


		public void TakingSoundtrackRef () {

			soundtrack = this.transform.FindChild ("Soundtrack Audio").GetComponents <AudioSource> ();
		}


		#region Playing_Soundtrack_Audio
		public void PlayFirstLevelSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Level_play")
					track.Play ();

			}

		}

		public void PlaySecondLevelSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Level_play2")
					track.Play ();

			}

		}

		public void PlayThirdLevelSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Level_play3")
					track.Play ();

			}

		}

		public void PlayFourthLevelSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Level_play4")
					track.Play ();

			}

		}

		public void PlayFifthLevelSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Level_play5")
					track.Play ();

			}

		}

		public void PlayLobbyMenuSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Menu_lobby")
					track.Play ();

			}

		}

		public void PlayBossSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Music_Boss")
					track.Play ();

			}

		}

		public void PlayGameOverSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Musica_GameOver")
					track.Play ();

			}

		}
		#endregion

		#region Stop_Playing_Soundtrack_Audio
		public void StopFirstLevelSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Level_play")
					track.Stop ();

			}

		}

		public void StopSecondLevelSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Level_play2")
					track.Stop ();

			}

		}

		public void StopThirdLevelSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Level_play3")
					track.Stop ();

			}

		}

		public void StopFourthLevelSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Level_play4")
					track.Stop ();

			}

		}

		public void StopFifthLevelSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Level_play5")
					track.Stop ();

			}

		}

		public void StopLobbyMenuSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Menu_lobby")
					track.Stop ();

			}

		}

		public void StopBossSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Music_Boss")
					track.Stop ();

			}

		}

		public void StopGameOverSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Musica_GameOver")
					track.Stop ();

			}

		}
		#endregion
      
    }
}