using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardEntry : MonoBehaviour
{
    [SerializeField] private Image m_PositionImage;
    [SerializeField] private TMP_Text m_PositionText;

    [SerializeField] private Image m_PlayerSprite; //Wont be used because dont have :D
    [SerializeField] private TMP_Text m_PlayerName;
    [SerializeField] private TMP_Text m_Score;
    [SerializeField] private TMP_Text m_PlayTime;

    public void Initialize(LeaderboardData data, int position)
    {
        if (position < 3)
        {
            m_PositionImage.gameObject.SetActive(true);
            m_PositionText.gameObject.SetActive(false);

            m_PositionImage.sprite = LeaderboardManager.Instance.Top3Sprites[position];
        }
        else
        {
            m_PositionImage.gameObject.SetActive(false);
            m_PositionText.gameObject.SetActive(true);

            m_PositionText.text = string.Format("{0}.", position + 1);
        }

        if (data != null)
        {
            m_PlayerName.text = data.name;
            m_Score.text = data.score;
            m_PlayTime.text = data.playtime;
        }
    }
}
