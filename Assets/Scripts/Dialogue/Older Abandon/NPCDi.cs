using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abandonado
public class NPCDi : MonoBehaviour
{
    [SerializeField] bool FirstInteraction = true;
    [SerializeField] int RepeatStartPosition;

    public string NpcName;
    public DialogueAsset DialogueAsset;
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