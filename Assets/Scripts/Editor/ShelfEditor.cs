using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    [CustomEditor(typeof(ShelfManager))]
    public class ShelfEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        
            ShelfManager myScript = (ShelfManager)target;
            var amount = EditorGUILayout.IntField("Amount to Edit", 20);
            
            if(GUILayout.Button("+ Amount"))
            {
                myScript.Add(amount);
            }	
		
            if(GUILayout.Button("- Amount"))
            {
                myScript.Subtract(amount);
            }	
        }
    }
}