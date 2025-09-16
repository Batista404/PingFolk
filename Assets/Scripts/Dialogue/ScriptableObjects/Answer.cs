using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Answer
{
    public string answerLabel;
    public int nextElement; // Qual será o o proximo dialogue section

    public int actionNumber;
}
