using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Dragon))]
public class DragonEditor : Editor
{
	private static int _amount = 200;

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if (!EditorApplication.isPlaying)
			return;

		Dragon myScript = (Dragon) target;
		_amount = EditorGUILayout.IntField("Amount", _amount);

		if (GUILayout.Button("Bring Loot"))
		{
			myScript.BringBackLoot(_amount);
			Debug.Log("Brought back loot: " + _amount);
		}
	}
}