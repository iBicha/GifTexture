using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jillzhang.GifUtility;
using System.IO;

public class GifTexture : MonoBehaviour
{

    public Renderer Renderer;

     // Use this for initialization
    void Start()
    {
        GifImage gif = GifDecoder.Decode("Assets/demo.gif");
        Renderer.material.mainTexture = gif.Texture;
        StartCoroutine(CycleGif(gif));

    }


    IEnumerator CycleGif(GifImage gif)
    {
        int index = 0;
        while (true)
        {
            GifFrame frame = gif.Frames[index];
            gif.GetTexture(index);
            index = (index + 1) % gif.Frames.Count;
            yield return new WaitForSeconds(frame.Delay / 100f);
        }
    }
    

}
