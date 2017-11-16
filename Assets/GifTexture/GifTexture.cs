using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jillzhang.GifUtility;
using System.IO;

public class GifTexture
{
    private static DummyBehaviour host;
    private static DummyBehaviour Host
    {
        get
        {
            if (host == null)
            {
                GameObject go = new GameObject("{Gif Host}");
                host = go.AddComponent<DummyBehaviour>();
                go.hideFlags = HideFlags.HideAndDontSave;
            }
            return host;
        }
    }


    private GifImage gif;
    private int index;

    public GifTexture(string filename)
    {
        gif = GifDecoder.Decode(filename);
        if (gif == null)
        {
            throw new System.Exception("Could not decode gif");
        }
    }

    public GifTexture(byte[] buffer)
    {
        gif = GifDecoder.Decode(buffer);
        if (gif == null)
        {
            throw new System.Exception("Could not decode gif");
        }
    }

    private bool isPlaying;
    public bool IsPlaying
    {
        get
        {
            return isPlaying;
        }
    }

    //TODO:  Get App Extension for looping, read the value and play exact number of times
    private bool loop;
    public bool Loop
    {
        get
        {
            return loop;
        }
        set
        {
            loop = value;
        }
    }

    public float Duration {
        get
        {
            float dur = 0f;
            for (int i = 0; i < gif.Frames.Count; i++)
            {
                dur += gif.Frames[i].Delay / 100f;
            }
            return dur;
        }
    }

    public void Pause()
    {
        Host.StopCoroutine(CycleGif());
        isPlaying = false;
    }

    public void Play()
    {
        Pause();
        Host.StartCoroutine(CycleGif());
        isPlaying = true;
    }

    public void Stop()
    {
        Pause();
        index = 0;
    }

    IEnumerator CycleGif()
    {
        while (true)
        {
            gif.UpdateTexture(index);
            
            yield return new WaitForSeconds(gif.Frames[index].Delay / 100f);

            if (loop || index < gif.Frames.Count - 1)
            {
                index = (index + 1) % gif.Frames.Count;
            }
            else
            {
                break;
            }
        }
        isPlaying = false;
    }

    static public implicit operator Texture(GifTexture gifTexture)
    {
        if(gifTexture == null || gifTexture.gif == null)
        {
            return null;
        }
        return gifTexture.gif.Texture;
    }

    private class DummyBehaviour : MonoBehaviour
    {

    }
}
