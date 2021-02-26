#region metadata
/**
#PRODUCT# 
by #COMPANY#
author: https://github.com/noney1412

NOTE: 
    - Unity Version (2020.2.1f1), .NET 4.6, C# 8.0
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
        public List<string> infoList;

        public Text info;
        public InputField input;
        public Button submit;
        public Text output;

        private void Start()
        {
            infoList = new List<string>() { "Unity", "Unreal", "Resident Evil", "Google", "MongoDB" };
            info.text = string.Join("\n", infoList);
            submit.onClick.AddListener(OnSubmit);
        }

        public void OnSubmit()
        {
            bool isFound = infoList.Contains(input.text);

            var hightlight = isFound ? "green" : "red";
            var result = isFound ? "is found" : "is not found";
            output.text = $" [ <color={hightlight}>{input.text}</color> ] {result} ";
        }
    }
}