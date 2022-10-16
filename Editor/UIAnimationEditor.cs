using UnityEngine;
using UnityEditor;

namespace UITween
{
    [CustomEditor(typeof(UIAnimation), true)]
    [CanEditMultipleObjects]
    public class UIAnimationEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawDefaultInspector();
            GUILayout.Space(20);
            if (GUILayout.Button("Play"))
            {
                UIAnimation anim = (UIAnimation)target;
                anim.Stop();
                anim.Play();
            }
            if (GUILayout.Button("Stop"))
            {
                UIAnimation anim = (UIAnimation)target;
                anim.Stop();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}