using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenUI : MonoBehaviour
{
    public InputField nameField;
    

    public void StartClicked()
    {
        GameManager.instance.LaunchGame(nameField.text);
    }
}
