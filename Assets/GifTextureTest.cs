using Jillzhang.GifUtility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class GifTextureTest : MonoBehaviour {

    public Renderer Renderer;

    // Use this for initialization
    void Start()
    {
        byte[] buffer = System.IO.File.ReadAllBytes("Assets/demo.gif");
        GifTexture gifTexture = new GifTexture(buffer);
        gifTexture.Loop = true;
        gifTexture.Play();
        Renderer.material.mainTexture = gifTexture;
    }


}
