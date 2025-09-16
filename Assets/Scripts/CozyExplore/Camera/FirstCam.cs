using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCam : MonoBehaviour
{
    [SerializeField] private GameObject Camera;
    [SerializeField] private Transform CameraTarget;
    [SerializeField] private Vector2 RotateMinMax;

    public float TurnSpeed;

    private float RotY;

    private void LateUpdate()
    {
        if (ExploreInfo.Instance.IsPaused || ExploreInfo.Instance.InDialogue)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            FollowTarget();
            MouseMove();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


    // Muda a posição, para acompanho o Target
    void FollowTarget()
    {
        Vector3 targetPosition = CameraTarget.transform.position;
        transform.position = targetPosition;
    }
    //Muda a direção 
    void MouseMove()
    {
        // get the mouse inputs
        float x = Input.GetAxis("Mouse X") * TurnSpeed;
        RotY += Input.GetAxis("Mouse Y") * TurnSpeed;
        // clamp the vertical rotation
        RotY = Mathf.Clamp(RotY, RotateMinMax.x, RotateMinMax.y);
        // rotate the camera

        float rotX = Camera.transform.eulerAngles.y + x;

        Camera.transform.eulerAngles = new Vector3(-RotY, rotX, 0);

        CameraTarget.transform.eulerAngles = new Vector3(0, rotX, 0);

    }
}
