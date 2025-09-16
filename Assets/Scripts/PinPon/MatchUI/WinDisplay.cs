using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using TMPro;
using UnityEngine;

public class WinDisplay : MonoBehaviour
{
    [SerializeField] private GameObject WinText;

    private bool SingleShoot;

    private void Start()
    {
        SingleShoot = false;
    }

    private void Update()
    {
        if ((!SingleShoot) && (MatchInfo.Instance.IsFinished))
        {
            WinTextWrite();
            SingleShoot = true;
        }

    }

    void WinTextWrite()
    {
        DialogueLine dialogueLine = WinText.GetComponent<DialogueLine>();
        switch (MatchInfo.Instance.WhoWinned)
        {
            case 1:
                dialogueLine.input = ("You Win!");
                break;
            case 2:
                dialogueLine.input = ("You Lose!");
                break;
            case 3:
                dialogueLine.input =("Anybody Win!");
                break;
        }
        dialogueLine.WriteMessage();
    }
}

