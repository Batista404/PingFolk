using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CozyMoving : MonoBehaviour
{
    [Header("configurável")]
    [SerializeField] private float Speed;
    [SerializeField] private float MinSpeed;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float GroundDistance;
    [SerializeField] private float JumpForce;
    [SerializeField] private GameObject Sphere;

    [Header("não configurável")]
    [SerializeField] private Vector3 Inputs;
    [SerializeField] private bool IsJump, IsOnGround;
    [SerializeField] private Rigidbody SphereRb;
    [SerializeField] float SVelocity; // Velocidade da Esfera


    private void Start()
    {
        SphereRb = Sphere.GetComponent<Rigidbody>();

        Sphere.transform.parent = null;
    }

    private void Update()
    {
        SVelocity = SphereRb.velocity.magnitude;
        transform.position = Sphere.transform.position;
    }

    private void FixedUpdate()
    {
        if (ExploreInfo.Instance.IsPaused || ExploreInfo.Instance.InDialogue)
        {
            SphereRb.velocity = Vector3.zero;
        }
        else
        {

            if (IsOnGround)
            {
                SphereRb.AddRelativeForce(Inputs * Speed);

                // Pulo
                if (IsJump)
                {
                    SphereRb.AddRelativeForce(new Vector3(Inputs.x, JumpForce, Inputs.z), ForceMode.Impulse);
                }
                SphereRb.velocity = Vector3.ClampMagnitude(SphereRb.velocity, MaxSpeed);

            }
            else
            {
                SphereRb.velocity = Vector3.ClampMagnitude(SphereRb.velocity, 10000);
            }
        }
    }
    public void SetInputs(float x, float z, bool jump)
    {
        Inputs = new Vector3(x, 0, z).normalized;
        IsJump = jump;

    }

    private void OnCollisionEnter(Collision collision)
    {
        IsOnGround = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        IsOnGround = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        IsOnGround = false;
    }

}
