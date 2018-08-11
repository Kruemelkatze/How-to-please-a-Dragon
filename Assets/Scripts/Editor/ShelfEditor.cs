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

            if (!EditorApplication.isPlaying)
                return;

            ShelfManager myScript = (ShelfManager) target;
            var amount = EditorGUILayout.IntField("Amount to Edit", 20);

            if (GUILayout.Button("+ Amount"))
            {
                myScript.Add(amount);
            }

            if (GUILayout.Button("- Amount"))
            {
                myScript.Subtract(amount);
            }

            if (GUILayout.Button("Get Left Shelf"))
            {
                myScript.SelectLeft();
            }

            if (GUILayout.Button("Get Right Shelf"))
            {
                myScript.SelectRight();
            }
        }
    }
}