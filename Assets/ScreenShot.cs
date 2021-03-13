using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenShot : MonoBehaviour
{
    public Camera camera;
    public int resWidth = 400;
    public int resHeight = 400;
    public int ScreenPerSec;
    public float RecordTime;
    public float ScreenDelay;
    public List<byte[]> ListByte=new List<byte[]>();
    public bool Go;
    public bool Save;
    public float DiviSpeed;
    public int Count;
    private float ScreenDelaySet;
    public string AnimName;
    void Start()
    {
        GlobalVariable.SpeedDivi = DiviSpeed;
        Debug.Log("cc");
        ScreenDelay = 1 / (float)ScreenPerSec;
        ScreenDelaySet = ScreenDelay;
    }

    public string ScreenShotName(int width, int height, int name)
    {
        return string.Format(name.ToString() + ".png", Application.dataPath, width, height, "Screen");
    }

    void Update()
    {
        if(Go)
        {

            if(RecordTime>0)
            {
                if(ScreenDelay<0)
                {
                    RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
                    camera.targetTexture = rt;
                    Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.ARGB32, false);
                    camera.Render();
                    RenderTexture.active = rt;
                    screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
                    camera.targetTexture = null;
                    RenderTexture.active = null;
                    Destroy(rt);
                    byte[] bytes = screenShot.EncodeToPNG();
                    ListByte.Add(bytes);
                    ScreenDelay += ScreenDelaySet;
                    Count += 1;
                }
                ScreenDelay -= Time.deltaTime;
                RecordTime -= Time.deltaTime;
            }
        }

        if(Save)
        {
            var folder = Directory.CreateDirectory(Application.dataPath +"/../Test/"+AnimName+"/");
            for (int i = 0; i < ListByte.Count; i++)
            {
                /*string filename = ScreenShotName(resWidth, resHeight, i);*/
                System.IO.File.WriteAllBytes(Application.dataPath + "/../Test/" + AnimName + "/" + i.ToString() + ".png", ListByte[i]);
            }
            Save = false;
        }
    }
}

public class GlobalVariable
{
    public static float SpeedDivi;
}
