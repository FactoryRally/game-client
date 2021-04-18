using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboRally.Objects {
	[ExecuteAlways]
	public class Grid : MonoBehaviour {

		[ReadOnly]
		public Material GridMaterial;

		public float TileUnitSize = 1;

		public bool IsFocused;

		private Vector3 LastScale;

		void Awake() {
			GridMaterial = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
		}

		void Update() {
			GridMaterial.SetVector("_Tiling", new Vector4(transform.localScale.x / TileUnitSize, transform.localScale.z / TileUnitSize, 0, 0));

			if(transform.localScale != LastScale && !(
					transform.localScale.x % TileUnitSize == 0f &&
					transform.localScale.y % TileUnitSize == 0f &&
					transform.localScale.z % TileUnitSize == 0f)) {
				float x = LastScale.x;
				float y = LastScale.y;
				float z = LastScale.z;
				if(transform.localScale.x > LastScale.x) {
					x = Mathf.Round(transform.localScale.x / TileUnitSize) * TileUnitSize + TileUnitSize;
				} else if(transform.localScale.x < LastScale.x) {
					x = Mathf.Round(transform.localScale.x / TileUnitSize) * TileUnitSize - TileUnitSize;
				}
				if(transform.localScale.y > LastScale.y) {
					y = Mathf.Round(transform.localScale.y / TileUnitSize) * TileUnitSize + TileUnitSize;
				} else if(transform.localScale.y < LastScale.y) {
					y = Mathf.Round(transform.localScale.y / TileUnitSize) * TileUnitSize - TileUnitSize;
				}
				if(transform.localScale.z > LastScale.z) {
					z = Mathf.Round(transform.localScale.z / TileUnitSize) * TileUnitSize + TileUnitSize;
				} else if(transform.localScale.z < LastScale.z) {
					z = Mathf.Round(transform.localScale.z / TileUnitSize) * TileUnitSize - TileUnitSize;
				}
				transform.localScale = new Vector3(x, y, z);
			}
			LastScale = transform.localScale;

			transform.position = new Vector3(
				transform.localScale.x / 2 - 0.5f,
				0,
				transform.localScale.z / 2 - 0.5f
			);
		}

		private void OnMouseOver() {
			IsFocused = true;
		}

		private void OnMouseExit() {
			IsFocused = false;
		}
	}
}