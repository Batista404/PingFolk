using System.Collections.Generic;
using UnityEngine;

public class MatchInfo : MonoBehaviour
{
    // Instância estática para acesso global
    public static MatchInfo Instance { get; private set; }

    [Header("necessário")] // Por enquanto é necessário atribuir manualmente

    [SerializeField] private GameObject PointsDisplay; // Usada apenas para poder esconder, dependendo do modo escolhido
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject WinMenu;

    // Variáveis compartilhadas
    public int PlayerScore, EnemyScore, PlayerPower, EnemyPower; // Poderia ser uma propriedade?

    public int WhoWinned; // 1 =  Player, 2 = Enemy, 3 = NoOne 

    public bool IsFinished, IsPaused;

    public List<GameObject> Balls;

    // Variáveis privadas

    private PingPong LastBallInfo;

    private bool BallSide; // False = Indo para o Enemy e True = Indo para o Player

    private void Awake()
    {
        Instance = this;

        /*
        // Se ainda não existe instância, define e torna persistente
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else // já existe, então destroi o game object extra
        {
            Destroy(gameObject);
        }
        */

        // Reset
        IsFinished = false;
        IsPaused = false;
        PauseGame();

        switch (GameData.Instance.GameMode)
        {
            case 1: // Modo mais pontos, até a ultima bola
                PointsDisplay.SetActive(true);
                break;
            case 2: // Modo ultima bola
                PointsDisplay.SetActive(false);
                break;
            default:
                Debug.Log("Não foi selecionado um modo");
                break;
        }
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
            PauseGame();
        }

        if (!IsFinished)
            GamemodeUpdates();

        SetWinMenu(IsFinished);
    }
    void PauseGame()
    {
        if (IsPaused)
        {
            Time.timeScale = 0f;
            PauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
        }
    }

    private void SetWinMenu(bool finished)
    {
        WinMenu.SetActive(finished);
    }

    private void GamemodeUpdates()
    {
        switch (GameData.Instance.GameMode)
        {
            case 1: // Modo mais pontos, até a ultima bola

                if (Balls.Count == 0)
                {
                    if (PlayerScore > EnemyScore)
                    {
                        Debug.Log("Player venceu");
                        WhoWinned = 1;
                        IsFinished = true;
                    }
                    else if (EnemyScore > PlayerScore)
                    {
                        Debug.Log("Enemy venceu");
                        WhoWinned = 2;
                        IsFinished = true;
                    }
                    else
                    {
                        Debug.Log("Acabou não sei quem venceu");
                        WhoWinned = 3;
                        IsFinished = true;
                    }
                }
                break;
            case 2: // Modo ultima bola

                if (Balls.Count != 0)
                    LastBallInfo = Balls[0].GetComponent<PingPong>();

                BallSide = LastBallInfo.PlayerBateu;

                if (Balls.Count == 0)
                {
                    if (BallSide)
                    {
                        Debug.Log("Player venceu");
                        IsFinished = true;
                        WhoWinned = 1;
                    }
                    else
                    {
                        Debug.Log("Enemy venceu");
                        IsFinished = true;
                        WhoWinned = 2;
                    }
                }

                break;
            default:
                Debug.Log("Não foi selecionado um modo");
                break;
        }
    }
}
