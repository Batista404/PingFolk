using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    [Header ("Configurações")]
    [SerializeField] bool WallCanDestroy = true, IsPlayerSide;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            if (IsPlayerSide)
            {
                MatchInfo.Instance.EnemyScore += 1;
            }
            else
            {
                MatchInfo.Instance.PlayerScore += 1;
            }
        }

        RemoveBall(other);
    }
    private void OnTriggerStay(Collider other)
    {
        RemoveBall(other);
    }


    // Método que Remove a bola
    void RemoveBall(Collider other)
    {
        if (other.tag == "Ball")
        {
            PingPong pingPong = other.GetComponent<PingPong>();
            if (pingPong.CanDestroy & WallCanDestroy)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
