#region metadata
/**
GI455_Project 
by CHANON_PANPILA_1610706564
author: https://github.com/noney1412

NOTE: 
    - Unity Version (2020.2.3f1), .NET 4.6, C# 8.0
**/
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <todo>
/// 1. Clear text on disable 
/// 2. Open Popup and Set Message
/// 3. Close Popup
/// </todo>
public class PopupDialog : MonoBehaviour
{
    public Text report;
    public Button ok;

    private void OnEnable()
    {
        ok.onClick.AddListener(ClosePopup);
    }

    public void OpenPopup(string message)
    {
        gameObject.SetActive(true);
        report.text = message;
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        report.text = "";
        ok.onClick.RemoveListener(ClosePopup);
    }
}