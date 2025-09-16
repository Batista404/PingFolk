using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate2DBoard : MonoBehaviour
{

    [SerializeField] private bool FreezeXZAxis = true;
    // Update is called once per frame
    void LateUpdate()
    {
        if (FreezeXZAxis)
        {
            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}
