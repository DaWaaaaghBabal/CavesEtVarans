using System;
using UnityEngine;
using UnityEngine.UI;

namespace CavesEtVarans.gui
{
    public class FloatingText : MonoBehaviour
    {
        public float lifeTime;
        public float fadeTime;
        public float speed;
        private float alphaSlope;
        private float timePast;
        //[SerializeField]
        public Text txt;
        
        public String DisplayedText {
            set {
                txt.text = value;
            } get {
                return txt.text;
            }
        }

        public Color Color {
            set {
                txt.color = value;
            }
            get {
                return txt.color;
            }
        }
        
        void Start() {
            alphaSlope = -1f / (fadeTime);
            timePast = 0;
        }
   
        void Update() {
            FadeOut();
			MoveUp();
        }

        private void FadeOut() {
            timePast += Time.deltaTime;
            float alpha = 1 + (timePast - lifeTime) * alphaSlope;
            if (alpha <= 0) Destroy(gameObject);
            else {
                Color col = Color;
                col.a = Math.Min(1, alpha);
                Color = col;
            }
		}

		private void MoveUp() {
			transform.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;
		}
	}
}
