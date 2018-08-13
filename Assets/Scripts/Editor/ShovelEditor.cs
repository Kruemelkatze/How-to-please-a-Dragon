using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    [CustomEditor(typeof(ShovelScript))]
    public class ShovelEditor : UnityEditor.Editor
    {
     
        public int _shovelAmount = 50;
        public int _shovelUpgrade = 0;
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (!EditorApplication.isPlaying)
                return;

            ShovelScript myScript = (ShovelScript) target;
            _shovelAmount = EditorGUILayout.IntField("Shovel Amount", _shovelAmount);
            
            _shovelUpgrade = EditorGUILayout.IntField("upgrade shovel 0-2", _shovelUpgrade);

            if (GUILayout.Button("change Shovel Amount"))
            {
                myScript.ShovelAmount = _shovelAmount;
            }


            if (GUILayout.Button("Upgrade Shovel"))
            {
                myScript.UpgradeShovel(_shovelUpgrade);;
            }

            if (GUILayout.Button("Downgrade Shovel"))
            {
                myScript.DowngradeShovel(_shovelUpgrade);
            }
            
        }
    }
}