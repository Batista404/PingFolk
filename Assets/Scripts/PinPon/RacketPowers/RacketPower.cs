using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketPower : MonoBehaviour
{
    public int NeededPower;

    public bool IsPlayer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (!IsPlayer)
            {
                MatchInfo.Instance.PlayerPower += 1;

                MatchInfo.Instance.PlayerPower = Mathf.Clamp(MatchInfo.Instance.PlayerPower, 0, NeededPower);
            }
            else
            {
                MatchInfo.Instance.EnemyPower += 1;

                MatchInfo.Instance.EnemyPower = Mathf.Clamp(MatchInfo.Instance.EnemyPower, 0, NeededPower);
            }
        }
    }
}
