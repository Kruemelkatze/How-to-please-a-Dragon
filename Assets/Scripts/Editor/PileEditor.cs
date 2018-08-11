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
		var amount = EditorGUILayout.FloatField("Amount to Edit", 20);
		if(GUILayout.Button("+ Amount"))
		{
			myScript.Add(amount);
		}	
		
		if(GUILayout.Button("- Amount"))
		{
			var actualSubtracted = myScript.Subtract(amount);
			Debug.Log("Subtracted " + actualSubtracted);
		}	
		
	}
}