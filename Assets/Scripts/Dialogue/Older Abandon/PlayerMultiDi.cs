using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abandonado

public class PlayerMultiDi : MonoBehaviour
{
    [SerializeField] float talkDistance = 5;
    bool inConversation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        if (inConversation)
        {
            DialogueBoxControllerMulti.instance.SkipLine();
        }
        else
        {
            if (Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit hitInfo, talkDistance))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out NPCMultiDi npc))
                {
                    DialogueBoxControllerMulti.instance.StartDialogue(npc.DialogueTree, npc.StartPosition, npc.NpcName, npc.MyImage);
                }
            }
        }
    }

    void JoinConversation()
    {
        inConversation = true;
    }

    void LeaveConversation()
    {
        inConversation = false;
    }

    private void OnEnable()
    {
        DialogueBoxControllerMulti.OnDialogueStarted += JoinConversation;
        DialogueBoxControllerMulti.OnDialogueEnded += LeaveConversation;
    }

    private void OnDisable()
    {
        DialogueBoxControllerMulti.OnDialogueStarted -= JoinConversation;
        DialogueBoxControllerMulti.OnDialogueEnded -= LeaveConversation;
    }
}
