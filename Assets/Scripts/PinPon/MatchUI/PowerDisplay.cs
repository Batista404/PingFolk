using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text TextUiPowes;

    private void Start()
    {
        TextUiPowes = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (MatchInfo.Instance == null)
        {
            Debug.LogError("MatchInfo.Instance está null! Verifique se o GameManager com DontDestroyOnLoad existe.");
            return;
        }

        TextUiPowes.text = $"Power:{MatchInfo.Instance.PlayerPower}";

        if (MatchInfo.Instance.PlayerPower == 25)
        {
            TextUiPowes.text = $"Power:{MatchInfo.Instance.PlayerPower} Press> Space bar";
        }
    }
}
