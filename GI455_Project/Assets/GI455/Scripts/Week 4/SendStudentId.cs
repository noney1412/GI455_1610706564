#region metadata
/**
GI455_Project 
by CHANON_PANPILA_1610706564
author: https://github.com/noney1412

NOTE: 
    - Unity Version (2020.2.3f1), .NET 4.6, C# 8.0
**/
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class SendStudentId : MonoBehaviour
{
    public struct SendStudentID
    {
        public string eventName;
        public string studentID;
    }
    private void Start()
    {
        var myself = new WebSocket("wss://gi455-305013.an.r.appspot.com/");

        myself.OnMessage += OnSend;

        myself.Connect();

        if (myself.ReadyState == WebSocketState.Open)
            myself.Send(JsonUtility.ToJson(new SendStudentID()
            {
                eventName = "GetStudentData",
                studentID = "1610706564"
            }));
    }

    private void OnSend(object sender, MessageEventArgs e)
    {
        var data = e.Data;
        print(data);
    }
}