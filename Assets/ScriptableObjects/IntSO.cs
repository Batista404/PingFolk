using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntSO : ScriptableObject
{
    [SerializeField] private int value;

    public string IntName;

    public int Value { get => Value;}

    public void AddAmountBy(int amount)
    {
        value += amount;
        //value = Mathf.Clamp(value, 0, value);
    }


    public void ResetAmount(int amount)
    {
        value = amount;
    }
}
