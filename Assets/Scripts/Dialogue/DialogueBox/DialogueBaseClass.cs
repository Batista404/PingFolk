using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        protected IEnumerator WriteText(string input, TMP_Text textHolder, float textSpeed )
        {
            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }
}
