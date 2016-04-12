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
        //public int edgeNumber;

        private HashSet<GameObject> edges;
      
        void Start() {
            edges = new HashSet<GameObject>();
              /*edges = new TileEdge[edgeNumber];
            corners = new TileEdge[edgeNumber + 1];
            int angle = 180; // @TODO fix import so that the half-turn offset isn't needed.
            for (int i = 0; i < edgeNumber; i++)
            {
                Quaternion euler = Quaternion.Euler(0, angle, 0);
                edges[i] = Create(edgePrefab, euler);
                corners[i] = Create(cornerPrefab, euler);
                angle -= edgeAngle;
            }
            corners[edgeNumber] = corners[0];*/
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

            /*
            // Old system
            edges[edge].Highlight();
            corners[edge].Highlight();
            corners[edge + 1].Highlight();*/
        }

        public void ClearEdges()
        {
            foreach(GameObject obj in edges)
            {
                Destroy(obj);
            }
            /*
            foreach (TileEdge edge in edges) {
                edge.Clear();
            }
            foreach (TileEdge corner in corners) {
                corner.Clear();
            }*/
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (debug) Debug.Log(Tile);
            if (eventData.button == PointerEventData.InputButton.Left)
                GUIEventHandler.Get().HandleClickOnTile(this);
		}
    }
}
