using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PerfectScreen2 : MonoBehaviour
{
    // Start is called before the first frame update
    public StateScreen Screen;
    public List<ParticuleManager> ListParti;
    public List<FieldManager> ListField;
    public int CounterGo;
    public bool smooth;
    public new Camera camera;
    public int resWidth = 400;
    public int resHeight = 400;
    public int ScreenPerSec;
    public int NombreFrame;
    public List<byte[]> ListByte = new List<byte[]>();
    public int Count;
    public string AnimName;
    public KeyCode Key;
    void Start()
    {
        
    }

    void Update()
    {
        

    }


    public void ScreenShot()
    {
        if (NombreFrame > 0)
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
            Count += 1;
            NombreFrame -= 1;
        }else if(NombreFrame==0)
        {
            var folder = Directory.CreateDirectory(Application.dataPath + "/../Test/" + AnimName + "/");
            for (int i = 0; i < ListByte.Count; i++)
            {
                /*string filename = ScreenShotName(resWidth, resHeight, i);*/
                System.IO.File.WriteAllBytes(Application.dataPath + "/../Test/" + AnimName + "/" + i.ToString() + ".png", ListByte[i]);
            }
            NombreFrame -= 1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!smooth)
        {

        if(Screen ==StateScreen.GO)
        {
           /* CounterGo += 1;
            if(CounterGo==2)
            {
                
                CounterGo = 0;
            }*/
                Screen = StateScreen.SCREEN;
                for(int i =0;i<ListParti.Count;i++)
                {
                    ListParti[i].Acti();
                }
                for (int i = 0; i < ListField.Count; i++)
                {
                    ListField[i].Acti();
                }
            }
        else if(Screen == StateScreen.SCREEN)
        {
                //Screen
                ScreenShot();
                Screen = StateScreen.Wait;
        }else if(Screen==StateScreen.Wait)
        {
                CounterGo += 1;
                if(CounterGo==5)
                {
                    CounterGo = 0;
                    Screen = StateScreen.GO;
                }

        }
        }else
        {
            Screen = StateScreen.GO;
            for (int i = 0; i < ListParti.Count; i++)
            {
                ListParti[i].Acti();
            }
            for (int i = 0; i < ListField.Count; i++)
            {
                ListField[i].Acti();
            }
        }


    }



    public enum StateScreen
    {
        SCREEN,
        GO,
        Wait,
    }
}
