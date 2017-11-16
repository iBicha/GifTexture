using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jillzhang.GifUtility;
using System.IO;

public class GifTexture : MonoBehaviour
{

    public Renderer Renderer;

    GifImage gif;
    int index = 0;
    // Use this for initialization
    void Start()
    {
        gif = GifDecoder.Decode("Assets/demo.gif");
        StartCoroutine(CycleGif());

    }


    IEnumerator CycleGif()
    {
        while (true)
        {
            GifFrame frame = gif.Frames[index];
            Renderer.material.mainTexture = frame.Image;
            index = (index + 1) % gif.Frames.Count;
            yield return new WaitForSeconds(frame.Delay / 100f);
        }
    }
    

}
