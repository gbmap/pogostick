using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MsgFinishingLineReached
{
    public float t;
}

public class GamemodeChallenge : Gamemode
{
    public string NextLevel;

    bool runTimer;
    public float Timer { get; private set; }

    Canvas c;

    public System.Action OnFinishingLineReached;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        Time.timeScale = 1f;
        runTimer = true;

        c = FindObjectOfType<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (runTimer)
        {
            Timer += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadCurrentLevel();
        }
    }

    public void OnLevelFinished()
    {
        OnFinishingLineReached?.Invoke();
        runTimer = false;

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadSceneAsync(NextLevel);
    }

    public void ReloadCurrentLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
