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
        SetFirstFrame();
    }

    public GifTexture(byte[] buffer)
    {
        gif = GifDecoder.Decode(buffer);
        if (gif == null)
        {
            throw new System.Exception("Could not decode gif");
        }
        SetFirstFrame();
    }

    public bool UseRealtime { get; set; }

    private IEnumerator coroutine;
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
        if (isPlaying)
        {
            Host.StopCoroutine(coroutine);
            isPlaying = false;
        }
    }

    public void Play()
    {
        if (!isPlaying)
        {
            if (index == gif.Frames.Count -1)
            {
                index = 0;
            }
            Host.StartCoroutine(coroutine = CycleGif());
            isPlaying = true;
        }
    }

    public void Stop()
    {
        Pause();
        SetFirstFrame();
    }

    IEnumerator CycleGif()
    {
        while (true)
        {
            gif.UpdateTexture(index);
            if (UseRealtime)
            {
                yield return new WaitForSecondsRealtime(gif.Frames[index].Delay / 100f);
            }
            else
            {
                yield return new WaitForSeconds(gif.Frames[index].Delay / 100f);
            }

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


    void SetFirstFrame()
    {
        if(gif.Frames.Count > 0)
        {
            index = 0;
            gif.UpdateTexture(index);
        }
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
