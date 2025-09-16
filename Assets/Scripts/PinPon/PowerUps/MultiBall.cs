using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MultiBall : MonoBehaviour
{
    [SerializeField] GameObject PingPongBall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag ==  "Ball")
        {
            SpawnBall();

            Destroy(gameObject);
        }
    }
    void SpawnBall()
    {
        Vector3 pos = transform.position;

        // Instancia o prefab em 'pos' sem rotação extra
        Instantiate(PingPongBall, pos, Quaternion.identity);
    }
}
