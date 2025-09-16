using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustRacket : MonoBehaviour
{
    public int BaseForce;
    [SerializeField] private bool IsPlayerSide;

    [SerializeField] DirectionPointer PlayerDirection;
    private void OnTriggerEnter(Collider other)
    {
        RacketWork(other);
    }
    private void OnTriggerStay(Collider other)
    {
        RacketWork(other);
    }


    // Método que funciona como uma raquete
    void RacketWork(Collider other)
    {
        
        Rigidbody otherRigid = other.GetComponent<Rigidbody>();
        PingPong otherPingPong = otherRigid.GetComponent<PingPong>();
        int force = Random.Range(1, 6);
        float direcao = BaseForce * force;

        if (IsPlayerSide)
        {
            otherPingPong.PlayerBateu = true;
        }
        else
        {
            otherPingPong.PlayerBateu = false;
            direcao = -direcao;
        }


        //Explicacao Vector(Esquerda ou direita, cima ou baixo, velocidade)
        float randomX;
        if (IsPlayerSide)
        {
            // Tem que ir em uma direção entre -20 e 20, usando a força do player
            randomX = PlayerDirection.Force;
        }
        else
            randomX = Random.Range(-15, 15); // muda a direção

        float randomY = Random.Range(-5, 0); // frente ou baixo
        otherRigid.AddForce(new Vector3(randomX, randomY, direcao), ForceMode.Impulse);

        //Debug.Log("Player Bateu " + other.name);
    }
}
