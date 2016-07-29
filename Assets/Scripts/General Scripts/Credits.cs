using UnityEngine;
using System.Collections;

namespace Spacchiamo
{
    public class Credits : MonoBehaviour
    {
        Vector2 upDirection = Vector2.up;
        public float alpha;
        public Color text;
      

        // Use this for initialization
        void Start()
        {
            text = this.GetComponent<TextMesh>().color;
        }

        // Update is called once per frame
        void Update()
        {
           
            this.transform.position = (Vector2)this.transform.position + upDirection * Time.deltaTime * 1;

            if (this.transform.localPosition.y > 15)
            {
                alpha = text.a;
                alpha -= 1 * Time.deltaTime;
                ChangingAlpha();
            }
            if (this.GetComponent<TextMesh>().color.a <= 0)
            {
                if (this.name == "Programmer2")
                    Game_Controller.instance.creditsEnded = true;

                Destroy(this.gameObject);
            }
        }

        public void ChangingAlpha()
        {
            Color textTemp = text;
            textTemp.a = alpha;
            text = textTemp;
            this.GetComponent<TextMesh>().color = text;

        }
    }
}
