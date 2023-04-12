using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class AnimatedButton : Button
{
    [SerializeField] public float m_ExpandFactor;
    [SerializeField] public float m_ExpandTime;

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
