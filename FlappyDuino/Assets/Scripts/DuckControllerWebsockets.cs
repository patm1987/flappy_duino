using System;
using UnityEngine;
using WebSocketSharp;

public class DuckControllerWebsockets : MonoBehaviour
{
    [SerializeField] private string _address;
    [SerializeField] private BirdMovement _movement;
    [SerializeField] private int _flyThreshold = 200;
    private WebSocket _websocket;
    [SerializeField] private Color _color = Color.white;

    private int _currentFlight = 0;

    private void Awake()
    {
        _websocket = new WebSocket(_address);
        _websocket.OnMessage += HandleMessage;
        _websocket.OnError += HandleError;
        _websocket.OnClose += HandleClose;
        _websocket.Connect();
    }

    private void Update()
    {
        _movement.Flying = _currentFlight > _flyThreshold;
    }

    private void OnDestroy()
    {
        _websocket.OnMessage -= HandleMessage;
        _websocket.OnError -= HandleError;
        _websocket.OnClose -= HandleClose;
        _websocket.Close();
    }

    public void SetColor(Color c)
    {
        if (_color != c)
        {
            _color = c;
            _websocket.Send($"c:{(byte) (_color.r * 255f)},{(byte) (_color.g * 255f)},{(byte) (_color.b * 255f)}\n");
        }
    }

    private void HandleMessage(object sender, MessageEventArgs e)
    {
        var message = System.Text.ASCIIEncoding.Default.GetString(e.RawData);
        if (message.StartsWith("r:"))
        {
            int.TryParse(message.Substring(2), out _currentFlight);
        }
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