using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PingPong : MonoBehaviour
{

    [SerializeField] float BallSpeed, MaxBallSpeed;
    [SerializeField] GameObject Ball;

    public Rigidbody BallRB;
    public bool PlayerBateu = false, CanDestroy = true;
    public float Direcao;

    // Start is called before the first frame update
    void Start()
    {
        
        BallRB = GetComponent<Rigidbody>();
        //Explicacao Vector(Esquerda ou direita, cima ou baixo, velocidade)
        BallRB.AddForce(new Vector3(0,0, Direcao), ForceMode.Impulse);

        Ball = this.gameObject;

        MatchInfo.Instance.Balls.Add(Ball);
    }

    // Update is called once per frame
    void Update()
    {
        BallSpeed = BallRB.velocity.magnitude;
        

        // Para a bola não ficara parada no mesmo lugar 
        if (PlayerBateu)
        {
            BallRB.AddForce(new Vector3(0, 0, 5), ForceMode.Acceleration);
        }
        else
            BallRB.AddForce(new Vector3(0, 0, -5), ForceMode.Acceleration);
        
        if (!BallRB.isKinematic)
            BallRB.velocity = Vector3.ClampMagnitude(BallRB.velocity, MaxBallSpeed);
        else
        {
            PlayerBateu = false;
        }
    }
    private void OnDestroy()
    {
        MatchInfo.Instance.Balls.Remove(Ball);
    }
}
