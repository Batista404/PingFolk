using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Colocado na camera 
public class PlayerDi : MonoBehaviour
{
    [SerializeField]private float talkDistance = 5; 
    [SerializeField]private bool inConversation;

    [SerializeField] NPCMultiDi NPCMultiSys;
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
            {
                DialogueBoxControllerMulti.instance.SkipLine();
                //Debug.Log("no multi");

                DialogueBoxController.instance.SkipLine(); // NPCDi Abandonado
                //Debug.Log("não foi no multi");
            }
        }
        else
        {
            if (Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit hitInfo, talkDistance))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out NPCDi npc)) // NPCDi Abandonado
                {
                    DialogueBoxController.instance.StartDialogue(npc.DialogueAsset.dialogue, npc.StartPosition, npc.NpcName, npc.MyImage);
                }
                else if (hitInfo.collider.gameObject.TryGetComponent(out NPCMultiDi npcM))
                {
                    NPCMultiSys = npcM;
                    DialogueBoxControllerMulti.instance.StartDialogue(npcM.DialogueTree, npcM.StartPosition, npcM.NpcName, npcM.MyImage);
                }
            }
        }
    }

    void JoinConversation()
    {
        NPCMultiSys.InDialogue = true;
        inConversation = true;
        ExploreInfo.Instance.InDialogue = true;

    }

    void LeaveConversation()
    {
        NPCMultiSys.InDialogue = false;
        inConversation = false;
        ExploreInfo.Instance.InDialogue = false;
    }

    //Configurações de Start na Singleton
    private void OnEnable()
    {
            DialogueBoxControllerMulti.OnDialogueStarted += JoinConversation;
            DialogueBoxControllerMulti.OnDialogueEnded += LeaveConversation;

            DialogueBoxController.OnDialogueStarted += JoinConversation; // NPCDi Abandonado
            DialogueBoxController.OnDialogueEnded += LeaveConversation; // NPCDi Abandonado
    }

    private void OnDisable()
    {
            DialogueBoxControllerMulti.OnDialogueStarted -= JoinConversation;
            DialogueBoxControllerMulti.OnDialogueEnded -= LeaveConversation;

            DialogueBoxController.OnDialogueStarted -= JoinConversation; // NPCDi Abandonado
            DialogueBoxController.OnDialogueEnded -= LeaveConversation; // NPCDi Abandonado

    }
}
