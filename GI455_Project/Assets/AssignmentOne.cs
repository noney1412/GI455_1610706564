#region metadata
/**
#PRODUCT# 
by #COMPANY#
author: https://github.com/noney1412
**/
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GI455
{
    public class AssignmentOne : MonoBehaviour
    {
        public List<string> infoList = new List<string>();

        public Text infoTextList;
        public InputField inputField;
        public Button submitButton;
        public Text outputText;

        private void Start()
        {
            var defaultList = new List<string>() { "Unity", "Unreal", "Resident Evil", "Google", "MongoDB" };
            infoList = defaultList;
            infoTextList.text = string.Join("\n", infoList);

            submitButton.onClick.AddListener(OnSubmit);
        }

        public void OnSubmit()
        {
            bool isFound = infoList.Contains(inputField.text);

            var hightlight = isFound ? "green" : "red";
            var result = isFound ? "is found" : "is not found";
            outputText.text = $" [ <color={hightlight}>{inputField.text}</color> ] {result} ";
        }
    }
}