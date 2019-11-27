using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Text timeDisplay;

    // Update is called once per frame
    void Update()
    {
        timeDisplay.text = "Time:" + GameManager.instance.gameTime/1000f;
    }
}
