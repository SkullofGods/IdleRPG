using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public Health Player;
    public GameObject restartScreen;
    private void Start()
    {
        Player.onDeath.AddListener(ShowScreen);
    }

    private void ShowScreen()
    {
        LeanTween.moveLocalY(restartScreen, 0, .3f);
    }
}
