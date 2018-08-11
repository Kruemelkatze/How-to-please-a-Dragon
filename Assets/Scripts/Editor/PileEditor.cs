using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Pile))]
public class PileEditor : Editor
{
	private static int _amount = 200; 
	
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
        
		Pile myScript = (Pile)target;
		_amount = EditorGUILayout.IntField("Amount to Edit", _amount);
		if(GUILayout.Button("+ Amount"))
		{
			myScript.Add(_amount);
		}	
		
		if(GUILayout.Button("- Amount"))
		{
			var actualSubtracted = myScript.Subtract(_amount);
			Debug.Log("Subtracted " + actualSubtracted);
		}	
		
	}
}