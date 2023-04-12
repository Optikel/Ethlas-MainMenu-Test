using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject m_LoginOverlay;
    [SerializeField] float m_OpenDuration;
    
    [Header("Header")]
    [SerializeField] private Button m_LoginButton;

    [Header("Footer")]
    [SerializeField] private Button m_PlayButton;
    [SerializeField] private Button m_WinButton;
    [SerializeField] private Button m_DailyButton;
    [SerializeField] private Button m_QuestsButton;
    [SerializeField] private Button m_MoreButton;
    
    void Start()
    {
        m_LoginButton.onClick.AddListener(OpenLoginOverlay);
        m_WinButton.onClick.AddListener(OpenLoginOverlay);
        m_DailyButton.onClick.AddListener(OpenLoginOverlay);
        m_QuestsButton.onClick.AddListener(OpenLoginOverlay);
        m_WinButton.onClick.AddListener(OpenLoginOverlay);
    }

    void Update()
    {
        
    }

    void OpenLoginOverlay()
    {
        LeanTween.alphaCanvas(m_LoginOverlay.GetComponent<CanvasGroup>(), 0, 0);
        m_LoginOverlay.SetActive(true);

        LeanTween.alphaCanvas(m_LoginOverlay.GetComponent<CanvasGroup>(), 1, m_OpenDuration);
    }
}
