using UnityEngine;
using TMPro;  
public class PointsDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text TextUiPoints;

    private void Start()
    {
        TextUiPoints = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (MatchInfo.Instance == null)
        {
            Debug.LogError("MatchInfo.Instance est� null! Verifique se o GameManager com DontDestroyOnLoad existe.");
            return;
        }

        if (TextUiPoints == null)
        {
            Debug.LogError("textUiPoints n�o foi atribu�do no Inspector!");
            return;
        }

        TextUiPoints.text = $"P: {MatchInfo.Instance.PlayerScore} e E: {MatchInfo.Instance.EnemyScore}";
    }

}
