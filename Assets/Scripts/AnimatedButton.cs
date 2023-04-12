using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;


public class AnimatedButton : Button
{
    [SerializeField] internal float m_ExpandFactor;
    [SerializeField] internal float m_ExpandTime;

    Vector3 _InitialScale;

    protected override void Start()
    {
        base.Start();
        _InitialScale = transform.localScale;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        if (!interactable) return;

        LeanTween.cancel(gameObject);

        LeanTween.scale(gameObject, _InitialScale * m_ExpandFactor, m_ExpandTime);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        LeanTween.cancel(gameObject);

        float ratio = transform.localScale.x / (_InitialScale * m_ExpandFactor).x;
        float time = m_ExpandTime * ratio;
        LeanTween.scale(gameObject, _InitialScale, time);

    }
}

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
