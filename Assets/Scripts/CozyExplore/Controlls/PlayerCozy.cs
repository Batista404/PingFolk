using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCozy : MonoBehaviour
{
    [SerializeField] private CozyMoving CozyMoving;
    void Start()
    {
        CozyMoving = GetComponent<CozyMoving>();
    }

    // Update is called once per frame
    void Update()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        bool space = Input.GetKey(KeyCode.Space);

        CozyMoving.SetInputs(h, v, space);

    }
}
