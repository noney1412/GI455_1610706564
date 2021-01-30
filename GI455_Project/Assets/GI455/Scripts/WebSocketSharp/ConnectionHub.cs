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
using WebSocketSharp;

namespace GI455.WebSocketSharp
{
    public class ConnectionHub : MonoBehaviour
    {
        private void Start()
        {
            using (var ws = new WebSocket("ws://dragonsnest.far/Laputa"))
            {
                ws.OnMessage += (sender, e) =>
                    Debug.Log("Laputa says: " + e.Data);

                ws.Connect();
                ws.Send("BALUS");
            }
        }
    }
}