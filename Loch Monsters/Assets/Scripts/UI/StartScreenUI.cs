using System;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using RandomGenerator.Scripts;
using RandomGenerator.Scripts.FantasyNameGenerators;

public class StartScreenUI : MonoBehaviour, IMessageListener
{
    public FlexibleColorPicker colorPicker;
    public InputField nameField;
    public InputField serverIPInput;
    public InputField serverPortInput;
    public Text messageDisplay;

    IPAddress address;

    private void Start()
    {
        MessageSystem.instance.Subscribe(MessageType.CONNECT_FAILED, this);
        nameField.text = new OrcNameGenerator().Generate(new System.Random());
        var newColor = new Color(UnityEngine.Random.Range(.1f, 1f), UnityEngine.Random.Range(.1f, 1f), UnityEngine.Random.Range(.1f, 1f));
        colorPicker.startingColor = newColor;

        //if any IP has been saved in player prefs put in in automatically. Players are likely to be reusing the same server. 
        if (PlayerPrefs.HasKey("lastIP"))
        {
            serverIPInput.text = PlayerPrefs.GetString("lastIP");
        }

        if (PlayerPrefs.HasKey("lastPort"))
        {
            serverPortInput.text = PlayerPrefs.GetString("lastPort");
        }


    }

    private void OnDestroy()
    {
        MessageSystem.instance.Unsubscribe(MessageType.CONNECT_FAILED, this);
    }

    public void Receive(IMessage message)
    {
        switch (message.GetMessageType())
        {
            case MessageType.CONNECT_FAILED:
                ShowErrorMessage(((ConnectFailed)message).error);
                break;
        }
    }

    void ShowErrorMessage(string message)
    {
        messageDisplay.text = "Error: "+ message.Split(':')[1];
        messageDisplay.gameObject.SetActive(true);
        
    }

    public void StartClicked()
    {
        int port = 5555;

        if(nameField.text.Length == 0)
        {
            messageDisplay.text = "Error: Please enter a name";
            messageDisplay.gameObject.SetActive(true);
        }
        else if (IPAddress.TryParse(serverIPInput.text, out address) && int.TryParse(serverPortInput.text, out port))
        {
            PlayerPrefs.SetString("lastIP", serverIPInput.text);
            PlayerPrefs.SetString("lastPort", serverPortInput.text);
            MessageSystem.instance.DispatchMessage(new LaunchGame(serverIPInput.text, port, nameField.text, colorPicker.color));

        }
        else
        {
            messageDisplay.text = "Error: The input does not contain a valid IP and port";
            messageDisplay.gameObject.SetActive(true);
        }

       
    }
}
