using UnityEngine;
using System.Collections;


namespace Spacchiamo
{
    [ExecuteInEditMode]
    public class EditModeTag : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            if (this.name.Contains("Enemy1"))
                this.tag = "Enemy1";
            else if (this.name.Contains("Enemy2"))
                this.tag = "Enemy2";
            else if (this.name.Contains("Enemy3"))
                this.tag = "Enemy3";
            else if (this.name.Contains("Enemy4"))
                this.tag = "Enemy4";
            else if (this.name.Contains("Enemy5"))
                this.tag = "Enemy5";
            else if (this.name.Contains("Enemy6"))
                this.tag = "Enemy6";
            else if (this.name.Contains("Enemy7"))
                this.tag = "Enemy7";
            else if (this.name.Contains("Falo"))
                this.tag = "Falo";
            else if (this.name.Contains("Trainer"))
                this.tag = "Trainer";
            else if (this.name.Contains("Uscita"))
                this.tag = "Finish";
            else if (this.name.Contains("Player"))
                this.tag = "Player";
        }

        // Update is called once per frame
        void Update()
        {
            if (this.name.Contains("Enemy1"))
                this.tag = "Enemy1";
            else if (this.name.Contains("Enemy2"))
                this.tag = "Enemy2";
            else if (this.name.Contains("Enemy3"))
                this.tag = "Enemy3";
            else if (this.name.Contains("Enemy4"))
                this.tag = "Enemy4";
            else if (this.name.Contains("Enemy5"))
                this.tag = "Enemy5";
            else if (this.name.Contains("Enemy6"))
                this.tag = "Enemy6";
            else if (this.name.Contains("Enemy7"))
                this.tag = "Enemy7";
            else if (this.name.Contains("Falo"))
                this.tag = "Falo";
            else if (this.name.Contains("Trainer"))
                this.tag = "Trainer";
            else if (this.name.Contains("Uscita"))
                this.tag = "Finish";
            else if (this.name.Contains("Player"))
                this.tag = "Player";
        }
    }
}