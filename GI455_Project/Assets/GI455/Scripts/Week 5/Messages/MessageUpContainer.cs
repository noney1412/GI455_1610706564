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
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <Todo>
/// 1. custom massage box margin.
/// 2. custom message up container margin.
/// 3. alight the message whether left or right.
/// </Todo>
public class MessageUpContainer : MonoBehaviour
{
    [Header("Message Box and Message")]
    public ExpandableText message;
    public RectTransform messageBox;

    [Header("Custom")]
    public Vector2 messageBoxMargin;
    public Vector2 containerMargin;

    private VerticalLayoutGroup _layout;

    private void Start()
    {
        _layout = GetComponent<VerticalLayoutGroup>();
    }

    private void OnGUI()
    {
        if (message.HasSizeChanged)
        {
            messageBox.sizeDelta = message.SizeDelta + messageBoxMargin;
            GetComponent<RectTransform>().sizeDelta = messageBox.sizeDelta + containerMargin;
        }
    }

    public void AlignLeft()
    {
        _layout.childAlignment = TextAnchor.UpperLeft;
    }

    public void AlignRight()
    {
        _layout.childAlignment = TextAnchor.UpperRight;
    }
}