using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using WebSocketSharp;

public class WebsocketController : MonoBehaviour
{
    [SerializeField] private string _address = "ws://localhost:8765/";
    [SerializeField] private int _threshold = 500;
    private WebSocket _websocket;
    [SerializeField] private Color _color = Color.white;

    public ExceedsThresholdEvent OnExceedsThreshold = new ExceedsThresholdEvent();

    private readonly Queue<Action> _actionQueue = new Queue<Action>();

    private void Awake()
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

    void Update()
    {
        while (_actionQueue.Any())
        {
            _actionQueue.Dequeue()();
        }
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
        var message = System.Text.Encoding.Default.GetString(e.RawData);
        if (message.StartsWith("r:"))
        {
            int resistance;
            if (int.TryParse(message.Substring(2), out resistance))
            {
                _actionQueue.Enqueue(() => OnExceedsThreshold.Invoke(resistance > _threshold));
            }
        }
    }

    private void HandleError(object sender, ErrorEventArgs e)
    {
        Debug.LogError($"Websocket error: {e.Message}");
    }

    private void HandleClose(object sender, CloseEventArgs e)
    {
        Debug.Log("Websocket Closed");
    }

    [System.Serializable]
    public class ExceedsThresholdEvent : UnityEvent<bool>
    {
    }
}