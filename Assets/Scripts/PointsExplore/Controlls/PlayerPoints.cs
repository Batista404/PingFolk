using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    [SerializeField] ExploMoves ExploMoves;

    private void Awake()
    {
        ExploMoves = GetComponent<ExploMoves>();
    }

    void Update()
    {
        
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (((h != 0) ^ (v != 0)) && !ExploMoves.IsWalking)
            ExploMoves.SetInput(h, v);

        ExploMoves.InputUpdateed(h, v);

    }
}
