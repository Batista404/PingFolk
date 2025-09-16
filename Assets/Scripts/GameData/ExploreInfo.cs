using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreInfo : MonoBehaviour
{
    public static ExploreInfo Instance { get; private set; }

    [Header("Necessário")]
    [SerializeField] private GameObject PauseMenu;

    public int Teste = 141;
    public bool IsFinished, IsPaused, InDialogue;

    void Start()
    {
        
    }
    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
            PauseGame();
        }
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
}
