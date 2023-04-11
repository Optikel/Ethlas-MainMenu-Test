using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PageSelector : MonoBehaviour
{
    [Range(6, 100)]
    [SerializeField]  private int m_MaxPages;
    [SerializeField] private Button m_LeftButton;
    [SerializeField] private Button m_RightButton;
    [SerializeField] private Button m_FirstPageBtn;
    [SerializeField] private Button m_LastPageBtn;
    [SerializeField] private GameObject m_LeftEllipsis;
    [SerializeField] private GameObject m_RightEllipsis;
    [SerializeField] private List<Button> m_CenterButtons;

    private int _PageIndex;
    private Color _NormalColor;
    private Color _SelectedColor;
    private List<Button> _AllNumberButtons = new List<Button>();
    
    private UnityEvent<int> _OnPageSelected = new UnityEvent<int>();
    public UnityEvent<int> OnPageSelected => _OnPageSelected;
    void Start()
    {
        _NormalColor = m_FirstPageBtn.colors.normalColor;
        _SelectedColor = m_FirstPageBtn.colors.selectedColor;

        m_LeftButton.onClick.AddListener(() => SelectPage(_PageIndex - 1));
        m_RightButton.onClick.AddListener(() => SelectPage(_PageIndex + 1));

        m_FirstPageBtn.GetComponentInChildren<TMPro.TMP_Text>().text = "1";
        m_FirstPageBtn.onClick.AddListener(() => SelectPage(0));
        
        m_LastPageBtn.GetComponentInChildren<TMPro.TMP_Text>().text = m_MaxPages.ToString();
        m_LastPageBtn.onClick.AddListener(() => SelectPage(m_MaxPages - 1));

        _AllNumberButtons.Add(m_FirstPageBtn);
        _AllNumberButtons.AddRange(m_CenterButtons);
        _AllNumberButtons.Add(m_LastPageBtn);

        SelectPage(0, true);
    }

    private void SelectPage(int pageIndex, bool ignoreCheck = false)
    {
        pageIndex = Mathf.Clamp(pageIndex, 0, m_MaxPages - 1);
        EventSystem.current.SetSelectedGameObject(null);

        if (_PageIndex == pageIndex && !ignoreCheck) return;

        _PageIndex = pageIndex;

        //Reset Center Buttons
        foreach (var btn in m_CenterButtons)
        {
            btn.gameObject.SetActive(true);
        }

        //Set Centerbutton and ellipsis according to pageindex
        //Get CenterButton PageIndex
        int startCenterCount = 0;
        if (_PageIndex < 4)
        {
            m_LeftEllipsis.SetActive(false);
            m_RightEllipsis.SetActive(true);

            startCenterCount = 1;
        }
        else if(_PageIndex > m_MaxPages - 5)
        {
            m_LeftEllipsis.SetActive(true);
            m_RightEllipsis.SetActive(false);

            startCenterCount = m_MaxPages - 5;
        }
        else
        {
            m_LeftEllipsis.SetActive(true);
            m_RightEllipsis.SetActive(true);

            m_CenterButtons[m_CenterButtons.Count - 1].gameObject.SetActive(false);
            startCenterCount = _PageIndex - 1;
        }


        //Set Active Center Button Listeners & Text
        int i = 1;
        foreach(var btn in m_CenterButtons)
        {
            if (btn.gameObject.activeSelf)
            {
                int pageSelect = startCenterCount + i;
                btn.GetComponentInChildren<TMPro.TMP_Text>().text = pageSelect.ToString();
                
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => SelectPage(pageSelect - 1));
                i++;
            }
        }

        foreach(var btn in _AllNumberButtons)
        {
            int idx = int.Parse(btn.GetComponentInChildren<TMPro.TMP_Text>().text) - 1;

            ColorBlock cb = btn.colors;
            if(idx == pageIndex)
                cb.normalColor = _SelectedColor;
            else
                cb.normalColor = _NormalColor;

            btn.colors = cb;
        }
        _OnPageSelected?.Invoke(pageIndex);
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
    }
}
