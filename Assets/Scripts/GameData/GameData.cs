using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    public int GameMode;
    public int PPowerSelected; // 1 = Super Atack, 2 = Big Racket, 3 = Stop all Balls
    public float Difficult; // 1 = Facil, 2 = M�dio, 3 = Dif�cil

    private void Awake()
    {
        
        // Se ainda n�o existe inst�ncia, define e torna persistente
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else // j� existe, ent�o destroi o game object extra
        {
            Destroy(gameObject);
        }
        
    }
}
