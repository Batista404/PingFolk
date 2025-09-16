using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMultiDi : MonoBehaviour
{
    [SerializeField] bool FirstInteraction = true;
    [SerializeField] int RepeatStartPosition;

    public bool InDialogue = false;
    public string NpcName;
    public DialogueTree DialogueTree;
    public Sprite MyImage;

    [HideInInspector]
    public int StartPosition
    {
        get
        {
            if (FirstInteraction)
            {
                FirstInteraction = false;
                return 0;
            }
            else
            {
                return RepeatStartPosition;
            }
        }
    }
}