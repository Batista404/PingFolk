using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        [SerializeField] float textSpeed;
        public string input;
        
        
        private TMP_Text textHolder;


        private void Awake()
        {
            textHolder = GetComponent<TMP_Text>();

            //StartCoroutine(WriteText(input, textHolder));
        }

        public void WriteMessage()
        {
            textHolder.text = "";
            StartCoroutine(WriteText(input, textHolder, textSpeed));
        }

    }
}