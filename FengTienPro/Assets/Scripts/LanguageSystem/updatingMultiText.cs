﻿using UnityEngine;
using UnityEngine.UI;
namespace MinYanGame.Core
{
    [RequireComponent(typeof(Text))]
    public class updatingMultiText : MonoBehaviour
    {
        public string multiId;
        public string currentString;
        private Text multiText;
        public void Set()
        {
            if(multiText == null)
                multiText = GetComponent<Text>();
            if (multiText == null)
            {
                Debug.LogWarning("doesn't have text component");
                return;
            }
            if (multiId == "")
            {
                Debug.LogWarning("multiId is null");
                return;
            }
            multiText.text = UpdateLanguage(GameController.Instance.language, GameController.Instance.MultiLang);
        }
        public string UpdateLanguage(Language language, muitiLang muiti)
        {
            for (int i = 0; i < muiti.dataArray.Length; i++)
            {
                if (multiId == muiti.dataArray[i].Id)
                {
                    currentString = language == Language.Chinese ? muiti.dataArray[i].Chinese : muiti.dataArray[i].English;
                }
            }
            return currentString;
        }

        //public void UpdateText(Language language,muitiLang muiti)
        //{
        //    for (int i = 0; i < muiti.dataArray.Length; i++)
        //    {
        //        if (multiId == muiti.dataArray[i].Id)
        //        {
        //            currentString = language == Language.Chinese ? muiti.dataArray[i].Chinese : muiti.dataArray[i].English;
        //        }
        //    }
        //    multiText.text = currentString;
        //}
    }
}
