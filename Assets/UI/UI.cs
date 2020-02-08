using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public UIConfig Config;

    // Start is called before the first frame update
    void Start()
    {
        var gamemode = FindObjectOfType<Gamemode>();
        if (gamemode == null)
        {
            return;
        }

        if (gamemode is GamemodeChallenge)
        {
            var t = Instantiate(Config.ChallengeUIPrefab, transform, false).GetComponent<RectTransform>();
            t.anchorMin = Vector2.zero;
            t.anchorMax = Vector2.one;
            t.offsetMin = Vector2.zero;
            t.offsetMax = Vector2.zero;
            //t.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0f, 0f);
            //t.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0f, 0f);
            /*t.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, 0f);
            t.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, 0f);*/

        }
    }
}
