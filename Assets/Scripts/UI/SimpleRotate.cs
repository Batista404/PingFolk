using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    [SerializeField] private float RotateSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + RotateSpeed, 0);
    }
}
