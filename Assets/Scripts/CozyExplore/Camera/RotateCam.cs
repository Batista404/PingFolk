using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RotateCam : MonoBehaviour
{
    [SerializeField] private Transform Camera;
    [SerializeField] private Transform Target;

    [SerializeField] private float Sensitivity = 0.2f;  // Sensibilidade do movimento

    [SerializeField] private Vector3 LastMousePos;
    private float RotX = 0f;
    private float RotY = 0f;
    [SerializeField] private Vector2 DistanceMinMax;

    private void LateUpdate()
    {
        transform.position = Target.transform.position;
    }
    void Update()
    {
        CamCollisionAndOclussion();

        if (Input.GetMouseButtonDown(1))
        {
            LastMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Vector3 currentMousePos = Input.mousePosition;
            Vector3 delta = currentMousePos - LastMousePos;

            // Acumula rotação
            RotY += delta.x * Sensitivity; // gira em torno de Y ao mover horizontalmente
            RotX -= delta.y * Sensitivity; // gira em torno de X ao mover verticalmente

            // Limita ângulo X entre -90 e +90 graus (opcional)
            RotX = Mathf.Clamp(RotX, -90f, 90f);

            

            // Aplica rotação preservando o eixo Z
            transform.rotation = Quaternion.Euler(RotX, RotY, 0f);

            Target.rotation = Quaternion.Euler(0f, RotY, 0f);

            LastMousePos = currentMousePos;
        }
        else
            Cursor.lockState = CursorLockMode.None;
    }

    void CamCollisionAndOclussion()
    {
        Ray ray = new Ray(Camera.transform.position, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 10);
        if(Physics.Raycast(ray, out hit, 10f))
        {

            Debug.Log("RayCast acertou " + hit.collider);
        }

    }
}
