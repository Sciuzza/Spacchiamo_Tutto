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


		#region Playing_And_Stopping_Soundtrack_Audio
		public void PlayFirstLevelSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Level_play")
					track.Play ();
				else
					track.Stop ();

			}

		}

		public void PlaySecondLevelSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Level_play2")
					track.Play ();
				else
					track.Stop ();

			}

		}

		public void PlayThirdLevelSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Level_play3")
					track.Play ();
				else
					track.Stop ();

			}

		}

		public void PlayFourthLevelSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Level_play4")
					track.Play ();
				else
					track.Stop ();

			}

		}

		public void PlayFifthLevelSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Level_play5")
					track.Play ();
				else
					track.Stop ();

			}

		}

		public void PlayLobbyMenuSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Menu_lobby")
					track.Play ();
				else
					track.Stop ();

			}

		}

		public void PlayBossSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Music_Boss")
					track.Play ();
				else
					track.Stop ();

			}

		}

		public void PlayGameOverSoundtrack () {

			foreach (var track in soundtrack) {

				if (track.clip.name == "Musica_GameOver")
					track.Play ();
				else
					track.Stop ();

			}

		}
		#endregion
      
    }
}