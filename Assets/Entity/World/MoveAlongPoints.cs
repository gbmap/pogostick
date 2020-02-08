using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMoveType
{
    PingPong,
    Bell
}

public class MoveAlongPoints : MonoBehaviour
{
    public float Speed = 1f;
    public float StartOffset = 0f;
    public EMoveType MoveType = EMoveType.Bell;

    Vector3 pointA;
    Vector3 pointB;

    private float T;

    // Start is called before the first frame update
    void Awake()
    {
        pointA = transform.GetChild(0).position;
        pointB = transform.GetChild(1).position;
        T = StartOffset;
    }

    // Update is called once per frame
    void Update()
    {
        T += Time.deltaTime * Speed;

        float _t = Evaluate(T % 1f, MoveType);
        Vector3 pos = Vector3.Lerp(pointA, pointB, _t);
        transform.position = pos;
    }

    float Evaluate(float x, EMoveType moveType)
    {
        switch (moveType)
        {
            case EMoveType.Bell: return Bell(x);
            case EMoveType.PingPong: return PingPong(x);
            default: return PingPong(x);
        }
    }

    float PingPong(float x)
    {
        // 2*1/PI * asin(sin(2*PI/2*x))
        return Mathf.Abs(2f * 1f / Mathf.PI * Mathf.Asin(Mathf.Sin(2f * Mathf.PI / 2f * x)));
    }

    float Bell(float x)
    {
        return x * x * (1f - x) * Mathf.Pow(1f - x, 2f) * 64f;
    }

}
