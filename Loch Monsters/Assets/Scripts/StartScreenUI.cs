using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("Game");
        
        
        
    }
}
