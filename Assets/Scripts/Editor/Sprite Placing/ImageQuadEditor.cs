using UnityEditor;
using UnityEngine;

namespace TemporaryGameCompany
{
    [CustomEditor(typeof(ImageQuad), editorForChildClasses:true)]
    public class ImageQuadEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = !Application.isPlaying;

            ImageQuad e = target as ImageQuad;
            if (GUILayout.Button("Resize"))
                e.UpdateMaterial();
            if (GUILayout.Button("Reposition"))
                e.Reposition();
        }
    }
}