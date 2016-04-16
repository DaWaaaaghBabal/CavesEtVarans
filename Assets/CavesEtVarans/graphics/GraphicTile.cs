using System;
using CavesEtVarans.battlefield;
using CavesEtVarans.gui;
using UnityEngine;
using UnityEngine.EventSystems;
using CavesEtVarans.character;
using System.Collections.Generic;

namespace CavesEtVarans.graphics
{
	/* This is the representation, in the Unity world, of a battlefield Tile. It handles all user-side interactions : display, click management...
     */
	public class GraphicTile : MonoBehaviour, IPointerClickHandler
	{
		public Tile Tile { set; get; }
		public float size;
        public bool debug;
        public GameObject edgePrefab;
        public GameObject cornerPrefab;
        public int edgeAngle;
        
        private HashSet<GameObject> edges;
      
        void Start() {
            edges = new HashSet<GameObject>();
        }

        private TileEdge Create(GameObject prefab, int angle)
        {
            GameObject obj = (GameObject)Instantiate(prefab, transform.position, Quaternion.Euler(0, angle, 0));
            edges.Add(obj);
            return obj.GetComponent<TileEdge>();
        }

        public void HighlightEdge(int edge)
        {
            int angle = 180 - edgeAngle * edge;
            Create(edgePrefab, angle);
            Create(cornerPrefab, angle);
            Create(cornerPrefab, angle - edgeAngle);
        }

        public void ClearEdges()
        {
            foreach(GameObject obj in edges)
            {
                Destroy(obj);
            }
            edges.Clear();
        }
        
        public void OnPointerClick(PointerEventData eventData) {
            if (debug) {
                Debug.Log(Tile);
            }
            if (eventData.button == PointerEventData.InputButton.Left)
                GUIEventHandler.Get().HandleClickOnTile(this);
		}
    }
}
