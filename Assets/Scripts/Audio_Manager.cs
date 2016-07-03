﻿using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class Audio_Manager : MonoBehaviour
    {

        [HideInInspector]
        public static Audio_Manager instance = null;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}