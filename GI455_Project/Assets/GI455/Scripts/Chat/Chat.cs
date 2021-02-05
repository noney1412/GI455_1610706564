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
        public static WebSocket me;

        [Header("Connection Panel")]
        public InputField url;
        public InputField port;
        public Button connect;
        public Text report;

        [Header("Chat Panel")]
        public InputField messageBox;
        public Button send;

        private void Start()
        {
            // try
            // {
            //     me = new WebSocket($"ws://{url.text}:{port.text}/");
            // }
            // catch (System.Exception e)
            // {
            //     report.text = e.Message;
            // }

            connect.onClick.AddListener(Connect);
        }

        public void Connect()
        {
            if (string.IsNullOrEmpty(url.text + port.text))
            {
                url.text = "127.0.0.1";
                port.text = "8080";
            }

            if (me?.ReadyState == WebSocketState.Open)
                return;

            try
            {
                me = new WebSocket($"ws://{url.text}:{port.text}/");
            }
            catch (System.Exception e)
            {
                report.text = $"<color='red'>{e.Message}</color>";
                return;
            }

            if (me.ReadyState == WebSocketState.Connecting)
            {
                report.text = "connecting...";
            }

            me.OnOpen += (sender, e) =>
            {
                if (me.ReadyState == WebSocketState.Open)
                {
                    report.alignment = TextAnchor.MiddleCenter;
                    report.text = "<color='green'>connected!</color>";
                    connect.interactable = false;
                    url.interactable = false;
                    port.interactable = false;
                }
            };

            me.OnMessage += (sender, e) =>
            {

            };

            me.OnClose += (sender, e) =>
            {
                switch (e.Code)
                {
                    case 1006:
                        report.text = $"<color='red'>[{e.Code}] Connection Failed</color>";
                        break;
                }

                Debug.LogWarning($"<color='red'>[{e.Code}] {e.Reason}</color>");
            };

            me.Connect();
        }

        private void OnDestroy()
        {
            me?.Close();
        }
    }
}