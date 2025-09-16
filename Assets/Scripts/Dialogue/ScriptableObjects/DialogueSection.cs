using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using UnityEngine;

[System.Serializable]
public struct DialogueSection
{
    [TextArea]
    public string[] dialogue;
    public int actionNumber;
    public bool endAfterDialogue;
    public BranchPoint branchPoint;
}
