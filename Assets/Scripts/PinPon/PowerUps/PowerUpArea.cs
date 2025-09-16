using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpArea : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] Vector2 FromV2, ToV2, TimerMinMax;
    [SerializeField] List<GameObject> PowerUps;

    [SerializeField] Vector3 RandomVector3;

    private bool SpawnActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float timer = Random.Range(TimerMinMax.x, TimerMinMax.y);

        int whatPowerUp = Random.Range(0, PowerUps.Count);

        if (!MatchInfo.Instance.IsFinished)
        {
            if (!SpawnActive)
                StartCoroutine(TimeToSpawn(timer, whatPowerUp));
        }
    }


    IEnumerator TimeToSpawn(float Time, int PowerNumb)
    {
        SpawnActive = true;
        yield return new WaitForSeconds(Time);

        // Sorteia X e Y dentro dos limites
        float x = Random.Range(FromV2.x, ToV2.x);
        float y = Random.Range(FromV2.y, ToV2.y);

        Vector3 pos = new Vector3(x, y, 0f);

        Quaternion rot = Quaternion.identity;
        Instantiate(PowerUps[PowerNumb], pos, rot);
        
        SpawnActive = false;
    }
}
