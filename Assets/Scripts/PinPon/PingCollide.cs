using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingCollide : MonoBehaviour
{
    [SerializeField] PingPong PingPong;
    [SerializeField] private int ForcaMesa;
    void Start()
    {
        PingPong = GetComponentInParent<PingPong>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Table")
        {
            float randomN = Random.Range(0, ForcaMesa); // quica
            PingPong.BallRB.AddForce(new Vector3(0, randomN, 0), ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Table")
        {
            float randomN = Random.Range(0, ForcaMesa); // muda a direção
            PingPong.BallRB.AddForce(new Vector3(0, randomN, 0), ForceMode.Impulse);
        }
    }
}
