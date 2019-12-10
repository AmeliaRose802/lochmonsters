using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndUIScript : MonoBehaviour, IMessageListener
{

    public Text endTitle;
    public Text endMessage;

    

    private void Awake()
    {
        MessageSystem.instance.Subscribe(MessageType.SETUP_END, this);
    }

    private void OnDestroy()
    {
        MessageSystem.instance.Unsubscribe(MessageType.SETUP_END, this);
    }


    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Receive(IMessage message)
    {
        switch (message.GetMessageType())
        {
            case MessageType.SETUP_END:
                SetupEnd((EndGame)message);
            break;
        }
    }

    void SetupEnd(EndGame message)
    {
        endTitle.text = message.endMessages[(int)message.endType];
        endMessage.text = message.message;
    }
}
