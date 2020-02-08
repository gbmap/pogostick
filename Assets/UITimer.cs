using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    GamemodeChallenge gamemode;
    Text txt;

    // Start is called before the first frame update
    void Start()
    {
         gamemode = FindObjectOfType<GamemodeChallenge>();
        txt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        System.TimeSpan time = System.TimeSpan.FromSeconds(gamemode.Timer);

        //here backslash is must to tell that colon is
        //not the part of format, it just a character that we want in output
        string str = time.ToString(@"mm\:ss\:fff");
        txt.text = str;
    }
}
