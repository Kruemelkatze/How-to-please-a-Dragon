using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Pile))]
public class PileEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
        
	
		Pile myScript = (Pile)target;
		var amount = EditorGUILayout.FloatField("Amount to Add", 20);
		if(GUILayout.Button("Add amount to level"))
		{
			myScript.Add(amount);
		}		
		
	}
}