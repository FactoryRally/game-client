using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InputManager))]
public class InputManagerEditor : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		InputManager im = (InputManager) target;
		if(GUILayout.Button("Apply")) {
			im.SaveBindings();
		}
		if(GUILayout.Button("Load")) {
			im.LoadBindings();
		}
	}

}
