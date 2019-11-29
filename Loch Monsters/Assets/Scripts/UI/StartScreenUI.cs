using UnityEngine;
using UnityEngine.UI;

public class StartScreenUI : MonoBehaviour
{
    public InputField nameField;
    public Color playerColor;

    public void StartClicked()
    {
        GameManager.instance.LaunchGame(nameField.text, playerColor);
    }
}
