using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour, IMessageListener
{
    public Text timeDisplay;
    public Text lengthDisplay;
    int length;

    void Start()
    {
        length = GlobalConsts.DEFAULT_LENGTH;
        MessageSystem.instance.Subscribe(MessageType.ADD_PLAYER_SEGMENT, this);
    }

    private void OnDestroy()
    {
        MessageSystem.instance.Unsubscribe(MessageType.ADD_PLAYER_SEGMENT, this);
    }

    public void Receive(IMessage message)
    {
        switch(message.GetMessageType())
        {
            case MessageType.ADD_PLAYER_SEGMENT:
                UpdateLengthDisplay();
                break;
        }
    }

    void UpdateLengthDisplay()
    {
        length++;
        lengthDisplay.text = "Length: " + length;
    }

    // Update is called once per frame
    void Update()
    {
        timeDisplay.text = "Time: " + GameManager.instance.gameTime/1000f;
    }
}
