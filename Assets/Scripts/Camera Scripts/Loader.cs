using UnityEngine;
using System.Collections;

namespace Spacchiamo
{	
	public class Loader : MonoBehaviour 
	{

		GameObject gameController;
		
		
		
		void Awake ()
		{
            //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null

            gameController = Resources.Load<GameObject>("Game Controller");

            if (Game_Controller.instance == null)
				
				//Instantiate gameManager prefab
				Instantiate(gameController);
		
	   }
	}
}