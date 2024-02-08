using System;
using UnityEngine;

public enum EBoundsSide
{
    Top = 0, Bot, Left, Right, Front, Back
}

public class Gamemode : MonoBehaviour
{
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        Instantiate(Resources.Load("UIGameplay"));
        Instantiate(Resources.Load("Managers"));
    }

}
