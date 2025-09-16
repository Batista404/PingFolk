using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLaunch : MonoBehaviour
{
    [SerializeField] private GameObject Ball;
    [SerializeField] private Rigidbody RBBall;

    private void Start()
    {
        if (Ball != null)
        {
            RBBall = Ball.GetComponent<Rigidbody>();
            // Start “held”
            RBBall.isKinematic = true;
        }
    }

    private void Update()
    {

        if (Ball != null && RBBall != null && RBBall.isKinematic)
        {
            // Follow the hand when held
            Ball.transform.position = transform.position;
        }

        if (Ball != null && Input.GetMouseButtonDown(0))
        {
            // Release and launch
            RBBall.isKinematic = false;
        }
    }
}
