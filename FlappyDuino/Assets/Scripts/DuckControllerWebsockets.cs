using System;
using UnityEngine;
using WebSocketSharp;

public class DuckControllerWebsockets : MonoBehaviour
{
    [SerializeField] private string _address;
    private WebSocket _websocket;

    private void Start()
    {
        _websocket = new WebSocket(_address);
        _websocket.OnMessage += HandleMessage;
        _websocket.OnError += HandleError;
        _websocket.OnClose += HandleClose;
        _websocket.Connect();
    }

    private void OnDestroy()
    {
        _websocket.OnMessage -= HandleMessage;
        _websocket.OnError -= HandleError;
        _websocket.OnClose -= HandleClose;
        _websocket.Close();
    }

    private void HandleMessage(object sender, MessageEventArgs e)
    {
        var message = System.Text.ASCIIEncoding.Default.GetString(e.RawData);
        Debug.Log($"Received {message}");
    }

    private void HandleError(object sender, ErrorEventArgs e)
    {
        Debug.LogError($"Websocket error: {e.Message}");
    }

    private void HandleClose(object sender, CloseEventArgs e)
    {
        Debug.Log($"Websocket Closed");
    }
}