using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UIHealth : MonoBehaviour
{
    public TMP_Text currentText;

    // Start is called before the first frame update
    void Start()
    {
        currentText.text = "Current Health: 10";
    }

    // Update is called once per frame
    public void updateScore(int point)
    {
        string text = "Current Health: ";
        text += point.ToString();
        currentText.text = text;
    }
}
