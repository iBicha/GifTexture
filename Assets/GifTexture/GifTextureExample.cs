using System;
using System.Linq;
using System.Text;
using UnityEngine;

public class GifTextureExample : MonoBehaviour {

    private static StringBuilder console = new StringBuilder();
	// Use this for initialization
	void Start () {
        IntPtr error = new IntPtr();
        IntPtr ret = GifTexture.OpenFile("C:\\Users\\bhadriche\\Downloads\\NJGJ4_s-200x150.gif", error);
        Log(string.Format("Ret:{0} Error:{1}", ret, error));
	}
		
	public static void Log(string obj)
    {
        Debug.Log(obj);
        console.Append(obj + "\n");
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), console.ToString());
    }
}
