using System;
using UnityEngine;

public class UIGamemodeChallenge : MonoBehaviour
{
    GamemodeChallenge gamemode;

    public GameObject VictoryScreen;

    private void Awake()
    {
        gamemode = FindObjectOfType<GamemodeChallenge>();

        VictoryScreen.SetActive(false);
    }

    private void OnEnable()
    {
        gamemode.OnFinishingLineReached += OnFinishingLineReachedCallback;
    }

    private void OnDisable()
    {
        gamemode.OnFinishingLineReached += OnFinishingLineReachedCallback;
    }

    private void OnFinishingLineReachedCallback()
    {
        VictoryScreen.SetActive(true);
    }

    public void NextLevelClick()
    {
        //throw new System.NotImplementedException();
        gamemode.LoadNextLevel();
    }

    public void ReplayClick()
    {
        gamemode.ReloadCurrentLevel();
    }

    public void ExitToMenuClick()
    {
        throw new System.NotImplementedException();
    }
}
