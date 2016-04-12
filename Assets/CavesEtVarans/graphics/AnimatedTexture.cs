using UnityEngine;

namespace CavesEtVarans.graphics
{
	public class AnimatedTexture : MonoBehaviour {

		public float framerate;
		public bool randomStart;
		public string sequenceName;
		private Texture[] sequence;
		private float frame;
		private Material material;
		// Use this for initialization
		void Start () {
			Object[] objects = Resources.LoadAll("Sequences/" + sequenceName, typeof(Texture));
			sequence = new Texture[objects.Length];
			for (int i = 0; i < objects.Length; i++) {
				sequence[i] = (Texture)objects[i];
			}
			frame = randomStart ? Random.Range(0, sequence.Length) : 0;
			material = GetComponent<MeshRenderer>().material;
		}
	
		// Update is called once per frame
		void Update () {
			int f = (int) frame;
			material.mainTexture = sequence[f % sequence.Length];
			frame+= framerate * Time.deltaTime;
		}
	}
}