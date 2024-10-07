using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    public float FadeSpeed = 1f;

    private float TargetAlpha;
    private float StartDelay = 0f;
    private Material Mat;
    void Start()
    {
        Mat = GetComponent<Renderer>().material;
        DoFade(1f, 0f, 0f);
    }

    void Update()
    {
        if (StartDelay > 0f)
            StartDelay -= Time.deltaTime;

        if (!IsFadeComplete() && StartDelay <= 0)
        {
            Color col = Mat.color;
            col.a = Mathf.MoveTowards(col.a, TargetAlpha, FadeSpeed*Time.deltaTime);
            Mat.color = col;
        }
    }

    public void DoFade(float startAlpha, float targetAlpha, float startDelay)
    {
        Color col = Mat.color;
        col.a = startAlpha;
        Mat.color = col;

        TargetAlpha = targetAlpha;
        StartDelay = startDelay;
    }

    public bool IsFadeComplete()
    {
        return Mat.color.a == TargetAlpha;
    }
}
