using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersInput : MonoBehaviour
{
    [SerializeField] private int PowerTimes, PowerTimesMax, BallStopperForce;

    [SerializeField] private PlayerMoving PlayerMov; 

    // Configurações para o Enemy, para ele ter poderes too
    [SerializeField] private AiMoving AiMov;
    [SerializeField] private int EPowerSelected;

    [SerializeField]private RacketPower MyRacketPower;
    private int RacketBaseForce;
    private Vector3 MySize; 

    private bool PowerActive;

    private void Awake()
    {
        MyRacketPower = GetComponent<RacketPower>();
    }
    private void Start()
    {
        if (MyRacketPower.IsPlayer)
        {
            PlayerMov = GetComponent<PlayerMoving>();
            RacketBaseForce = PlayerMov.Force;
        }
        else
        {
            AiMov = GetComponent<AiMoving>();
            RacketBaseForce = AiMov.Force;
        }

       
        MySize = transform.localScale;

    }
    private void Update()
    {
        PowerCall();
    }
    private void OnTriggerEnter(Collider other)
    {
        int selectedPower = MyRacketPower.IsPlayer ? GameData.Instance.PPowerSelected : EPowerSelected;

        switch (selectedPower)
        {
            case 1:SuperAtack(other);
                break;
            case 2:BigRacket(other);
                break;
            case 3:BallStopper();
                break;
            case 4:SuperDefense();
                break;
            default:
                Debug.Log("não foi selecionado um poder");
                break;
        }
    }

    // Chama os poderes
    private void PowerCall()
    {
        int selectedPower = MyRacketPower.IsPlayer ? GameData.Instance.PPowerSelected : EPowerSelected;

        if (MyRacketPower.IsPlayer)
        {
            // Verifica se está com o suficiente para ativar
            if (MatchInfo.Instance.PlayerPower == MyRacketPower.NeededPower && !PowerActive && Input.GetKey(KeyCode.Space))
            {
                PowerActive = true;
                switch (GameData.Instance.PPowerSelected)
                {
                    case 2:BigRacket(); break;
                    case 3:BallStopper(); break;
                    case 4:SuperDefense(); break;
                }
            }
            else if (MatchInfo.Instance.PlayerPower < 25)
            {
                PowerTimes = PowerTimesMax;
            }
        }
        else
        {
            if (MatchInfo.Instance.EnemyPower == MyRacketPower.NeededPower && !PowerActive)
            {
                PowerActive = true;
                switch (EPowerSelected)
                {
                    case 2: BigRacket(); break;
                    case 3: BallStopper(); break;
                    case 4: SuperDefense(); break;
                }
            }
            else if (MatchInfo.Instance.PlayerPower < 25)
            {
                PowerTimes = PowerTimesMax;
            }
        }
    }

    //Poderes
    private void SuperAtack(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (PowerActive)
            {
                PowerTimes = PowerTimes - 1; Debug.Log($"SuperAtack, Power Times ={PowerTimes}");

                if (MyRacketPower.IsPlayer) PlayerMov.Force = RacketBaseForce * 5;
                else AiMov.Force = RacketBaseForce * 5;

                if (PowerTimes <= 0)
                {
                    PowerActive = false;
                    ResetPower();
                    if (MyRacketPower.IsPlayer)PlayerMov.Force = RacketBaseForce;
                    else AiMov.Force = RacketBaseForce;
                }
            }
        }
    }
    private void BigRacket()
    {
        if (PowerActive)
        {
            Debug.Log($"BigRacket, Power Times ={PowerTimes}");
            transform.localScale = MySize * 2;
        }
    }
    private void BigRacket(Collider other)
    {
        if (PowerActive)
        {
            transform.localScale = MySize * 2;
            if (other.CompareTag("Ball"))
            {
                PowerTimes = PowerTimes - 1;Debug.Log($"BigRacket, Power Times ={PowerTimes}");
                if (PowerTimes <= 0)
                {
                    PowerActive = false;
                    ResetPower();
                    transform.localScale = MySize;
                }
            }
        }
    }
    private void BallStopper()
    {
        if (PowerActive)
        {
            foreach (GameObject ball in MatchInfo.Instance.Balls)
            {
                Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
                rigidbody.velocity = new Vector3(0,0,0);
                Debug.Log($"BallStopper, Power Times ={PowerTimes}");
            }
            PowerActive = false;
            ResetPower();
        }
    }
    private void SuperDefense()
    {
        if (PowerActive)
        {
            foreach (GameObject ball in MatchInfo.Instance.Balls)
            {
                Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
                PingPong pingpong = ball.GetComponent<PingPong>();
                if (MyRacketPower.IsPlayer)
                {
                    pingpong.PlayerBateu = true;
                    rigidbody.AddForce(new Vector3(0,0, BallStopperForce), ForceMode.Impulse);
                }
                else
                {
                    pingpong.PlayerBateu = false;
                    rigidbody.AddForce(new Vector3(0, 0, -BallStopperForce), ForceMode.Impulse);
                }
                ResetPower();
                Debug.Log($"SuperDefense, Power Times ={PowerTimes}");
            }
            PowerActive = false;
        }
    }

    // Reseta o valor dos poderes quando já usados
    private void ResetPower()
    {
        if (MyRacketPower.IsPlayer)
        {
            MatchInfo.Instance.PlayerPower = 0;
        }
        else
        {
            MatchInfo.Instance.EnemyPower = 0;
        }
    }
}
