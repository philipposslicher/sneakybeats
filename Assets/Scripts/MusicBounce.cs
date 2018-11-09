using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBounce : MonoBehaviour
{

    public AudioSource audioSource;
    private RectTransform rt;
    private float volumeScale;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        float[] data = new float[16];
        float a = 0;
        audioSource.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        volumeScale = a / 16;

        Vector2 volumeVector = new Vector2(volumeScale + 0.5f, volumeScale + 0.5f);
        rt.localScale = Vector2.Lerp(rt.localScale, volumeVector, 0.05f);
    }

}
