using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance { get; private set; }

    [Header("Leaderboard Data")]
    [SerializeField] private LeaderboardEntry m_EntryPrefab;
    [SerializeField] private Transform m_BoardContainer;
    [SerializeField] private Sprite[] m_Top3Sprite = new Sprite[3];
    [Range(10, 15)]
    [SerializeField] private int m_MaxEntries;
    [Header("Leaderboard Selection")]
    [SerializeField] private PageSelector m_PageSelector;
    
    private List<LeaderboardEntry> _EntryList = new List<LeaderboardEntry>();
    private List<LeaderboardData> _DataList = new List<LeaderboardData>();

    public Sprite[] Top3Sprites => m_Top3Sprite;
    private void Awake()
    {
        if (!Instance) Instance = this;
        else Debug.LogError("MULTIPLE INSTANCE - LeaderboardManager");
        }
    void Start()
    {
        ReloadEntry();
        FetchLeaderboardData();

        m_PageSelector.OnPageSelected.AddListener(LoadPage);
    }

    private void LoadPage(int idx)
    {
        int startPosition = (idx) * m_MaxEntries;
        //int lastPosition = (idx + 1) * m_MaxEntries;

        for (int i = 0; i < m_MaxEntries; i++)
        {
            if (startPosition + i < _DataList.Count)
                _EntryList[i].Initialize(_DataList[startPosition + i], startPosition + i);
            else
                _EntryList[i].Initialize(null, startPosition + i);
        }
    }

    void FetchLeaderboardData()
    {
        StartCoroutine(RequestLeaderboardData());

        IEnumerator RequestLeaderboardData()
        {
            string path = Path.Combine(Application.streamingAssetsPath, "Leaderboard.json");
            UnityWebRequest req = UnityWebRequest.Get(path);

            yield return req.SendWebRequest();

            if (req.error != null)
            {
                Debug.LogError("uwrReader error: " + req.error);
                Debug.LogError(path);
            }

            var jobj = JObject.Parse(req.downloadHandler.text);

            var objArr = JsonConvert.DeserializeObject<List<LeaderboardData>>(jobj["data"].ToString());

            foreach (var obj in objArr)
            {
                _DataList.Add(obj);
            }

            for (int i = 0; i < m_MaxEntries; i++)
            {
                if (i < _DataList.Count)
                    _EntryList[i].Initialize(_DataList[i], i);
                else
                    _EntryList[i].Initialize(null, i);
            }
        }
    }

    void ReloadEntry()
    {
        foreach(Transform child in m_BoardContainer)
        {
            if (child.name != "Header")
                GameObject.Destroy(child.gameObject);
        }

        for(int i = 0; i < m_MaxEntries; i++)
        {
            var entry = Instantiate(m_EntryPrefab, parent: m_BoardContainer);
            _EntryList.Add(entry);
        }
    }
}

[Serializable]
public class LeaderboardData
{
    public string name;
    public string score;
    public string playtime;
}