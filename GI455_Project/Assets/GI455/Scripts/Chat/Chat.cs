#region metadata
/**
GI455_Project 
by CHANON_PANPILA_1610706564
author: https://github.com/noney1412

NOTE: 
    - Unity Version (2020.2.1f1), .NET 4.6, C# 8.0
**/
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

namespace GI455.ChatSystem
{
    public class Chat : MonoBehaviour
    {
        [Header("Socket")]
        public WebSocket me;

        [Header("Connection Panel")]
        public InputField url;
        public InputField port;
        public Button connect;
        public Text report;

        private void Start()
        {
            connect.onClick.AddListener(Connect);
        }

        private void OnDestroy()
        {
            me?.Close();
        }

        public void Connect()
        {
            if (string.IsNullOrEmpty(url.text + port.text))
            {
                url.text = "127.0.0.1";
                port.text = "8080";
            }

            me = new WebSocket($"ws://{url.text}:{port.text}/");


            if (me.ReadyState == WebSocketState.Connecting)
            {
                report.text = "connecting...";
            }

            me.OnOpen += (sender, e) =>
            {
                if (me.ReadyState == WebSocketState.Open)
                {
                    report.text = "connected!";
                    Debug.Log(me.IsAlive);
                    Debug.Log(me.WaitTime);
                }
            };

            Debug.Log(me.IsAlive);
            Debug.Log(me.WaitTime);

            me.OnMessage += (sender, e) =>
            {

            };

            me.OnClose += (sender, e) =>
            {
                report.text = $"<color='red'>[{e.Code}] {e.Reason}</color>";
                Debug.LogWarning($"<color='red'>[{e.Code}] {e.Reason}</color>");
            };

            me.Connect();

            me.Send("เชื่อมต่อ");
        }
    }
}