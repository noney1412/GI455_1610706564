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


public class MessageUpContainer : MonoBehaviour
{
    [Header("Message Box and Message")]
    public RectTransform messageBox;
    public RectTransform message;

    [Header("Custom")]

    [Range(0, 30)]
    public float marginBottom;
    [Range(100, 413)]
    public float preferedWidth;

    [Range(0, 50)]
    public float verticalMargin;



    [SerializeField]
    private ContentSizeFitter _messageSizeFilter;

    private void Start()
    {
        preferedWidth = 413.0f;
        verticalMargin = 30.0f;
        _messageSizeFilter = message.GetComponent<ContentSizeFitter>();

        // chunk update
    }

    private void OnGUI()
    {
        if (message.hasChanged)
        {
            messageBox.sizeDelta = message.sizeDelta + new Vector2(31, marginBottom);
            GetComponent<RectTransform>().sizeDelta = messageBox.sizeDelta + new Vector2(0, verticalMargin);

            if (messageBox.sizeDelta.x < preferedWidth)
            {
                _messageSizeFilter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            }
            else
            {
                _messageSizeFilter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            }

        }
    }
}