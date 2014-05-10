using System;
using UnityEngine;
using System.Collections;

public class ScoreLabel : MonoBehaviour
{

    public GameController controller;
    public TextMesh textLabel;

    void Update()
    {
        textLabel.text = string.Format("Score: {0:D4}", controller.gameScore);
    }

}
