using UnityEditor;

[CustomEditor(typeof(AnimatedButton))]
public class AnimatedButtonEditor : UnityEditor.UI.ButtonEditor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Animation", EditorStyles.boldLabel);
        AnimatedButton targetButton = (AnimatedButton)target;
        targetButton.m_ExpandTime = EditorGUILayout.FloatField("Expand Time", targetButton.m_ExpandTime);
        targetButton.m_ExpandFactor = EditorGUILayout.FloatField("Expand Factor", targetButton.m_ExpandFactor);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Button", EditorStyles.boldLabel);
        base.OnInspectorGUI();
    }
}
