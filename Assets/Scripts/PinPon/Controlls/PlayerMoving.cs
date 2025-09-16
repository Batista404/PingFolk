using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    [SerializeField] private JustRacket PlayerJR; //JR, JustRacket
    // Infos
    private Vector3 MousePosition;
    private Vector3 WorldPosition;

    public int Force;

    private float DistanceCamera;
    void Start()
    {
        DistanceCamera = (-Camera.main.transform.position.z + transform.position.z);

        PlayerJR = gameObject.GetComponent<JustRacket>();
    }

    void Update()
    {

        PlayerJR.BaseForce = Force; // Muda o valor da raquete

        // Movimento com a posíção do mouse
        MousePosition = Input.mousePosition;
        MousePosition.z = Camera.main.nearClipPlane + DistanceCamera;

        WorldPosition = Camera.main.ScreenToWorldPoint(MousePosition);

        transform.position = WorldPosition;
    }
}
