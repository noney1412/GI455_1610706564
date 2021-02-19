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
using UnityEngine.UI;
using WebSocketSharp;
using TMPro;

namespace GI455.Week4
{
    public class ChatRoom : MonoBehaviour
    {
        [Header("Socket")]
        public WebSocket myself;
        public string url = "127.0.0.1";
        public string port = "8080";
        public Message message;

        [Header("Connection Panel")]
        public Image connectionPanel;
        public InputField roomName;
        public Button createRoom;
        public Button joinRoom;
        public Text report;

        [Header("Chat Panel")]
        public Text roomTitle;
        public Button leave;
        public Transform content;
        public VerticalLayoutGroup verticalContainerPrefab;
        public InputField messageInputField;
        public Button send;

        [Header("Popup Panel")]
        public Transform popupPanel;
        public Text reportError;
        public Button ok;

        enum ChatBoxOwner
        {
            myself,
            other
        }

        public struct Message
        {
            public string eventName;
            public string data;

            public Message(string eventName, string data)
            {
                this.eventName = eventName;
                this.data = data;
            }

            public string ToJson()
            {
                return JsonUtility.ToJson(this);
            }

            public bool isEmpty()
            {
                return string.IsNullOrEmpty(eventName + data);
            }

            public void Clear()
            {
                data = eventName = string.Empty;
            }
        }

        private void Start()
        {
            BindButtonOnStart();
            InitWebSocketOnStart();
        }

        private void BindButtonOnStart()
        {
            createRoom.onClick.AddListener(CreateRoom);
            joinRoom.onClick.AddListener(JoinRoom);
            send.onClick.AddListener(SendMessage);
            leave.onClick.AddListener(Leave);
            ok.onClick.AddListener(ClosePopup);
        }

        private void Update()
        {
            UpdateMessageEvent();
        }

        private void UpdateMessageEvent()
        {
            if (!message.isEmpty())
            {
                switch (message.eventName)
                {
                    case "createRoom":
                        if (message.data == "200")
                            roomTitle.text = roomName.text;
                        else if (message.data == "400")
                        {
                            ShowPopup("Room is already created");
                            myself.Close();
                        }
                        break;
                    case "joinRoom":
                        if (message.data == "200")
                            roomTitle.text = roomName.text;
                        else if (message.data == "400")
                        {
                            ShowPopup("Room is not found");
                            myself.Close();
                        }
                        break;
                    case "sendMessage":
                        Debug.Log($"create message {message.data}");
                        InstantiateChatBox(ChatBoxOwner.other, message.data);
                        break;
                }

                message.Clear();
            }

        }

        private void OnDestroy()
        {
            myself?.Close();
        }

        private void InitWebSocketOnStart()
        {
            if (string.IsNullOrEmpty(url + port))
            {
                url = "127.0.0.1";
                port = "8080";
            }

            if (myself?.ReadyState == WebSocketState.Open)
                return;

            try
            {
                myself = new WebSocket($"ws://{url}:{port}/");
            }

            catch (System.Exception e)
            {
                report.text = $"<color='red'>{e.Message}</color>";
                return;
            }

            if (myself.ReadyState == WebSocketState.Connecting)
            {
                report.text = "connecting...";
            }

            myself.OnOpen += (sender, e) =>
                        {
                            if (myself.ReadyState == WebSocketState.Open)
                            {
                                connectionPanel.gameObject.SetActive(false);
                            }
                        };

            myself.OnMessage += (sender, e) =>
                        {
                            Debug.Log(e.Data);
                            message = JsonUtility.FromJson<Message>(e.Data);
                        };

            myself.OnClose += (sender, e) =>
            {
                connectionPanel.gameObject.SetActive(true);

                switch (e.Code)
                {
                    case 1006:
                        report.text = $"<color='red'>[{e.Code}] Connection Failed</color>";
                        break;
                }

                Debug.LogWarning($"<color='red'>[{e.Code}] {e.Reason}</color>");
            };
        }

        private void CreateRoom()
        {
            if (string.IsNullOrEmpty(roomName.text))
            {
                report.text = "room name is empty!";
                return;
            }

            myself.Connect();
            myself.Send(new Message("createRoom", roomName.text).ToJson());
        }

        private void JoinRoom()
        {
            if (string.IsNullOrEmpty(roomName.text))
            {
                report.text = "room name is empty!";
                return;
            }

            myself.Connect();
            myself.Send(new Message("joinRoom", roomName.text).ToJson());
        }

        private void Leave()
        {
            if (myself?.ReadyState == WebSocketState.Open)
            {
                myself.Close();
                connectionPanel.gameObject.SetActive(true);
            }
        }

        private void SendMessage()
        {
            if (myself?.ReadyState == WebSocketState.Open)
            {
                myself.Send(new Message("sendMessage", roomName.text + "|" + messageInputField.text).ToJson());
                InstantiateChatBox(ChatBoxOwner.myself, messageInputField.text);
            }
        }

        private void InstantiateChatBox(ChatBoxOwner owner, string message)
        {
            VerticalLayoutGroup container = Instantiate<VerticalLayoutGroup>(verticalContainerPrefab, content);
            var chatBox = container.transform.GetChild(0);
            TextMeshProUGUI tmp = chatBox.GetChild(0).GetComponent<TextMeshProUGUI>();

            if (owner == ChatBoxOwner.myself)
            {
                container.childAlignment = TextAnchor.MiddleRight;
                tmp.text = message;
            }

            else if (owner == ChatBoxOwner.other)
            {
                container.childAlignment = TextAnchor.MiddleLeft;
                tmp.text = message;
            }
        }

        private void ClosePopup()
        {
            popupPanel.gameObject.SetActive(false);
        }

        private void ShowPopup(string message)
        {
            popupPanel.gameObject.SetActive(true);
            reportError.text = message;
        }
    }
}
