using UnityEngine;
using System.Collections;

public class Permanent : MonoBehaviour {


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

  
}
