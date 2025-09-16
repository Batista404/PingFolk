using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using OpenCover.Framework.Model;

public class DialogueBoxControllerMulti : MonoBehaviour
{
    public static DialogueBoxControllerMulti instance;

    [SerializeField] Image NpcPortrait;
    [SerializeField] TextMeshProUGUI DialogueText;
    [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] GameObject DialogueBox;
    [SerializeField] GameObject AnswerBox;
    [SerializeField] Button[] AnswerObjects; 

    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;

    bool SkipLineTriggered;
    bool AnswerTriggered;
    int AnswerIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void StartDialogue(DialogueTree dialogueTree, int startSection, string name, Sprite sprite)
    {
        ResetBox();
        NameText.text = name + "...";
        NpcPortrait.sprite = sprite;
        DialogueBox.SetActive(true);
        OnDialogueStarted?.Invoke();
        StartCoroutine(RunDialogue(dialogueTree, startSection));
    }

    IEnumerator RunDialogue(DialogueTree dialogueTree, int section)
    {
        ExploreInfo.Instance.InDialogue = true;
        for (int i = 0; i < dialogueTree.sections[section].dialogue.Length; i++)
        {
            if (dialogueTree.sections[section].actionNumber != 0)
            {
                SelectedAction(dialogueTree.sections[section].actionNumber);
            }
            else
                DialogueText.text = dialogueTree.sections[section].dialogue[i];
            while (SkipLineTriggered == false)
            {
                yield return null;
            }
            SkipLineTriggered = false;
        }

        if (dialogueTree.sections[section].endAfterDialogue) // Aqui que desativa a UI
        {
            OnDialogueEnded?.Invoke();
            DialogueBox.SetActive(false);
            yield break;
        }

        DialogueText.text = dialogueTree.sections[section].branchPoint.question;

        ShowAnswers(dialogueTree.sections[section].branchPoint);

        while (AnswerTriggered == false)
        {
            yield return null;
        }
        AnswerBox.SetActive(false);
        AnswerTriggered = false;

        // Ações ?
        if (dialogueTree.sections[section].branchPoint.answers[AnswerIndex].actionNumber != 0)
        {
            SelectedAction(dialogueTree.sections[section].branchPoint.answers[AnswerIndex].actionNumber);
        }


        //answers[answerIndex] pega o index, para ai pegar o next element
        StartCoroutine(RunDialogue(dialogueTree, dialogueTree.sections[section].branchPoint.answers[AnswerIndex].nextElement));
    }

    void ResetBox()
    {
        StopAllCoroutines();
        DialogueBox.SetActive(false);
        AnswerBox.SetActive(false);
        SkipLineTriggered = false;
        AnswerTriggered = false;
    }

    void ShowAnswers(BranchPoint branchPoint)
    {
        // Reveals the aselectable answers and sets their text values
        AnswerBox.SetActive(true);
        for (int i = 0; i < AnswerObjects.Length; i++)
        {
            if (i < branchPoint.answers.Length)
            {
                //Debug.Log("if "+i);
                AnswerObjects[i].GetComponentInChildren<TextMeshProUGUI>().text = branchPoint.answers[i].answerLabel;
                AnswerObjects[i].gameObject.SetActive(true);
            }
            else
            {
                //Debug.Log("else " + i);
                AnswerObjects[i].gameObject.SetActive(false);
            }
        }
    }

    public void SkipLine()
    {
        SkipLineTriggered = true;
    }
    // o valor no botão(On_click(value)) tem que começar do 0, já que ele vai pegar o index da resposta, para ai encontrar o nextElement
    public void AnswerQuestion(int answer)
    {
        ExploreInfo.Instance.InDialogue = false;
        //Debug.Log("BOTÃO "+ answer);
        AnswerIndex = answer;
        AnswerTriggered = true;
    }

    // Verificar a ação
    private void SelectedAction(int value)
    {
        switch (value)
        {
            case 1:
                preco();
                break;
            case 2:
                preco1();
                break;
        }
    }

    // Ações
    private void preco()
    {
        DialogueText.text = $"Quer algo? Teste {ExploreInfo.Instance.Teste}";
    }
    private void preco1()
    {
        ExploreInfo.Instance.Teste += 5;
        DialogueText.text = $"Sem Papo";
    }
}
