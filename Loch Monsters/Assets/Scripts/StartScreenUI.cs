using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenUI : MonoBehaviour
{
    public InputField nameField;

    NetworkManagerScript net;
    
    // Start is called before the first frame update
    void Start()
    {
        net = NetworkManagerScript.instance;
    }

    public void StartClicked()
    {
        string name = nameField.text;
        Debug.Log(name);
        net.EstablishConnection(name);
        
    }
}
