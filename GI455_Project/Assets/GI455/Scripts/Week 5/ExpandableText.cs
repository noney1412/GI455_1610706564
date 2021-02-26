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

public class ExpandableText : MonoBehaviour
{
    private TextMeshProUGUI _message;
    private LayoutElement _layout;
    private ContentSizeFitter _contentSize;
    private RectTransform _rect;

    public string Text { get => _message.text; set { _message.text = value; } }
    public Vector2 SizeDelta { get => _rect.sizeDelta; set => _rect.sizeDelta = value; }
    public bool HasSizeChanged { get => _rect.hasChanged; }

    [Header("Custom")]
    [Range(0, 413.0f)]
    float preferredWidth;

    private void Start()
    {
        preferredWidth = 413.0f;

        _message = GetComponent<TextMeshProUGUI>();
        _layout = GetComponent<LayoutElement>();
        _contentSize = GetComponent<ContentSizeFitter>();
        _rect = GetComponent<RectTransform>();
    }

    private void OnGUI()
    {
        if (_rect.hasChanged && _rect.sizeDelta.x > preferredWidth)
        {
            _layout.preferredWidth = preferredWidth;
        }
    }
}