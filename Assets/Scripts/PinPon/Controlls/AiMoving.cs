using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMoving : MonoBehaviour
{
    [SerializeField] Transform BallTransform;

    [Header("Configurações")]
    [Tooltip ("Velocidada para movimento")]
    [SerializeField] Vector2 MoveSpeed2;
    [SerializeField] Vector2 ErrorTax;

    [SerializeField] private JustRacket EnemyJR;

    private float PlayerZ;
    
    public int Force;

    void Start()
    {
        GameObject player = (GameObject.Find("Player"));// ficar na mesma distancia que o player
        Transform transformPlayer = player.transform;
        PlayerZ = -transformPlayer.position.z;
        
        EnemyJR = gameObject.GetComponent<JustRacket>();
    }
    void Update()
    {
        // 1) Encontra a primeira bola cujo PlayerBateu == true
        GameObject bolaAlvo = null;

        if (MatchInfo.Instance.Balls != null)
        {
            foreach (var ball in MatchInfo.Instance.Balls)
            {
                var pong = ball.GetComponent<PingPong>();
                if (pong != null && pong.PlayerBateu)
                {
                    bolaAlvo = ball;
                    break; // se quiser só a primeira
                }
            }
        }

        // 2) Se não encontrou nenhuma bola ativa, sai
        if (bolaAlvo == null) return;

        // 3) Calcula posição alvo, tremida
        Transform tBola = bolaAlvo.transform;
        float errorX = Random.Range(ErrorTax.x, ErrorTax.y);
        float errorY = Random.Range(ErrorTax.x, ErrorTax.y);
        Vector3 targetPos = new Vector3(
            tBola.position.x + errorX,
            tBola.position.y + errorY,
            PlayerZ
        );

        // 4) Sorteia uma velocidade
        float speed = Random.Range(MoveSpeed2.x, MoveSpeed2.y);

        // 5) Move-se em direção ao target
        transform.position = Vector3.MoveTowards(
            current: transform.position,
            target: targetPos,
            maxDistanceDelta: speed * Time.deltaTime
        );
    }
    
}
