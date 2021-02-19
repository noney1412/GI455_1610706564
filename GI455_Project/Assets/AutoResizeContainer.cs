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

public class AutoResizeContainer : MonoBehaviour
{
    public RectTransform content;
    private RectTransform _rect;

    private void Start()
    {
        _rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (content.hasChanged)
        {
            _rect.sizeDelta = content.sizeDelta + new Vector2(30, 30);
        }
    }
}