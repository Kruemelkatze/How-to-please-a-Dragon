using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    [CustomEditor(typeof(ShelfManager))]
    public class ShelfEditor : UnityEditor.Editor
    {
        private static int _amount = 200;
        private static int _index = 1;
        private static int _upgrade = 1;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (!EditorApplication.isPlaying)
                return;

            ShelfManager myScript = (ShelfManager) target;
            _amount = EditorGUILayout.IntField("Amount to Edit", _amount);
            
            _index = EditorGUILayout.IntField("Shelf index for upgrade", _index);
            _upgrade = EditorGUILayout.IntField("upgrade number 0-3", _upgrade);

            if (GUILayout.Button("+ Amount"))
            {
                myScript.Add(_amount);
            }

            if (GUILayout.Button("- Amount"))
            {
                myScript.Subtract(_amount);
            }

            if (GUILayout.Button("Select Left Shelf"))
            {
                myScript.SelectLeft();
            }

            if (GUILayout.Button("Select Right Shelf"))
            {
                myScript.SelectRight();
            }
            
            if (GUILayout.Button("Upgrade Shelf"))
            {
                myScript.UpgradeShelf(_index, _upgrade);
            }
        }
    }
}