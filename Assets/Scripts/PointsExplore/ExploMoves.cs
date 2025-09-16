using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ExploMoves : MonoBehaviour
{
    [SerializeField] private float Speed;

    private Rigidbody Rb;
    private Vector3 TargetPosition;
    private Vector3 WaypointPosition;
    private Vector2 InputUpdate = Vector2.zero;

    public bool IsWalking;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        // Se você não quer que a física rotacione o objeto:
        Rb.freezeRotation = true;
        TargetPosition = transform.position;
    }

    // 2)
    private void Update()
    {
        MoveTo();
    }

    // 1) Captura do input
    // 2) Ativar o movimento
    // 3) Se colidir com o waypoint
    // 4) Parar

    // 1)
    public void SetInput(float inputH, float inputV)
    {
        IsWalking = true;

        TargetPosition = new Vector3(inputH * 1000, transform.position.y, inputV * 1000);
    }
    public void InputUpdateed(float inputH, float inputV)
    {
        InputUpdate = new Vector3(inputH,inputV).normalized;
    }

    // 2)
    private void MoveTo()
    {
        transform.position = Vector3.MoveTowards(
            current: transform.position,
            target: TargetPosition,
            maxDistanceDelta: Speed * Time.deltaTime
        );
    }

    // 3)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            TargetPosition = other.transform.position;
            WaypointPosition = TargetPosition;
        }
        else if (other.CompareTag("Wall"))
        {
            Debug.Log("barreira, funciona");
            TargetPosition = WaypointPosition;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            if (transform.position == WaypointPosition)
                IsWalking = false;
        }
        float horizontal = InputUpdate.x * 2;
        float vertical = InputUpdate.y;
        ToWaypoint toWP = other.gameObject.GetComponent<ToWaypoint>();
        if (toWP != null)
        {
            Debug.Log($"Tá rodando, {vertical} & {horizontal}");

            if (vertical != 0f)
            {
                if (toWP.DirectionWP == vertical)
                {
                    TargetPosition = toWP.PositionWP;
                }
                else if (toWP.DirectionWP == vertical)
                {
                    TargetPosition = toWP.PositionWP;
                }
            }
            else if (horizontal != 0f)
            {
                if (toWP.DirectionWP == horizontal)
                {
                    TargetPosition = toWP.PositionWP;
                }
                else if (toWP.DirectionWP == horizontal)
                {
                    TargetPosition = toWP.PositionWP;
                }
            }
        }
    }
}

    
