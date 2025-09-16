using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DDi : MonoBehaviour
{
    [SerializeField]private float talkDistance = 5; 
    [SerializeField]private bool inConversation;

    [SerializeField]private int mask;
    [SerializeField]NPCMultiDi NPCMultiSys;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        Debug.DrawRay(transform.position, -transform.up * talkDistance, Color.green);
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
            mask = LayerMask.GetMask("NPC");
            if (Physics.Raycast(new Ray(transform.position, -transform.up), out RaycastHit hitInfo, talkDistance, mask))
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
        if(GameObject.Find("ExploreInfo") == true)
            ExploreInfo.Instance.InDialogue = true;

    }

    void LeaveConversation()
    {
        NPCMultiSys.InDialogue = false;
        inConversation = false;
        if (GameObject.Find("ExploreInfo") == true)
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
