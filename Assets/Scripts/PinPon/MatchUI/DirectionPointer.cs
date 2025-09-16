using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionPointer : MonoBehaviour
{
    [SerializeField] private GameObject Pointer;
    
    [SerializeField] private Vector2  MouseV2;
    [SerializeField] private Vector2 ScreenV2;

    public float Force;
    private float PastMouseX;

    private RectTransform PoRectT;

    private void Start()
    {
        PoRectT = Pointer.GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Variação do mouse no frame atual
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        // Podemos calcular um módulo da velocidade
        float speed = new Vector2(deltaX, deltaY).magnitude;
        speed = Math.Clamp(speed, -10f, 10);

        // Debug.Log($"ΔX: {deltaX:F2}  ΔY: {deltaY:F2}  Speed: {speed:F2}");

        PoRectT.sizeDelta = new Vector2(speed * 5, PoRectT.sizeDelta.y);

        // Direção do pointeiro
        MouseV2 = Input.mousePosition;

        ScreenV2 = new Vector2(Screen.width/2, Screen.height/2);

        // 1. Vetor que vai do centro até o mouse
        Vector2 dir = MouseV2 - ScreenV2;

        // 2. Calcula o ângulo em radianos entre o eixo X e esse vetor
        float angleRad = Mathf.Atan2(dir.y, dir.x);

        // 3. Converte para graus
        float angleDeg = angleRad * Mathf.Rad2Deg;

        // 4. Normaliza para [0, 360)
        if (angleDeg < 0) angleDeg += 360f;

        // 5
        Pointer.transform.rotation = Quaternion.Euler(0f, 180f, -angleDeg);

        // Força
        
        if (MouseV2.x < PastMouseX)
            Force = -5f * speed;
        else
            Force = 5f * speed;

        PastMouseX = MouseV2.x;

    }
}
