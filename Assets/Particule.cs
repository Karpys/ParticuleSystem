using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particule : MonoBehaviour
{
    // Start is called before the first frame update
    public Parti Stats;
    public float Life;
    public Vector3 Acc;
    public float Counter;
    public float CounterAcc;
    public float CounterSpeed;
    public float Round;
    public Vector3 V;
    public float NbrRound;
    public float LifeBeforeDecay;
    
    
    void Start()
    {
        LifeBeforeDecay = Stats.Lifeline.z;
        NbrRound = Stats.Lifeline.z / Time.fixedDeltaTime;
        GetComponent<SpriteRenderer>().sprite = Stats.sprite;
        GetComponent<SpriteRenderer>().color = Stats.Col;      
        Acc = new Vector2(Mathf.Cos(Stats.degree), Mathf.Sin(Stats.degree));
        if(Stats.ScaleEqualsBaseOnScaleXVector)
        {
            transform.localScale = new Vector3(Stats.ScaleX.z, Stats.ScaleX.z, Stats.ScaleX.z);
        }
        else
        {
            transform.localScale = new Vector3(Stats.ScaleX.z, Stats.ScaleY.z, Stats.ScaleX.z);
        }
        if(Stats.Reverse)
        {
            float MagnitudeSpeed = (Stats.Speed.z + Stats.Acceleration.z*NbrRound/2) * NbrRound;
            transform.position += Acc * MagnitudeSpeed  *Time.fixedDeltaTime;

        }
        /*Debug.Log(0.5f + zou);*/
    }

    // Update is called once per frame
    void Update()
    {
        if(Stats.Screen.Screen==PerfectScreen2.StateScreen.GO)
        {
            NextStep();
        }
        if(Stats.Lifeline.z<=0)
        {
            if(Stats.DecayOption.IsDecay)
            {
                GetComponent<SpriteRenderer>().color = Color.Lerp(Stats.ColLerp, Stats.DecayOption.DecayCol, (Life- LifeBeforeDecay) / Stats.DecayOption.TimeDecay);
                if((Life-LifeBeforeDecay)/Stats.DecayOption.TimeDecay>=1)
                {
                    if (Stats.EmmitOption.Emmiting)
                    {
                        EmmitFunc();
                    }
                    Destroy(gameObject);
                }
            }
            else
            {
                if (Stats.EmmitOption.Emmiting)
                {
                    EmmitFunc();
                }
                Destroy(gameObject);
            }
        }else
        {

        GetComponent<SpriteRenderer>().color = Color.Lerp(Stats.Col, Stats.ColLerp, Life/Stats.SpeedColor);
        }
    }

    public void NextStep()
    {
        Life += Time.fixedDeltaTime;
        Stats.Lifeline.z -= Time.fixedDeltaTime;
        Stats.Speed.z += Stats.Acceleration.z;
        if (Stats.degreeAcc != 0)
        {
            Stats.degree += Stats.degreeAcc * Mathf.Deg2Rad * Time.fixedDeltaTime;
            Acc = new Vector2(Mathf.Cos(Stats.degree), Mathf.Sin(Stats.degree));
        }
        V = Acc * Stats.Speed.z + Stats.Gravity;
        V *= Time.fixedDeltaTime;
        Round += 1;
        Counter += V.magnitude;
        if (Stats.Reverse)
        {
            V = -V;
        }
        if (Stats.ScalingX != 0 || Stats.ScalingY!=0)
        {
            Stats.ScaleX.z += Stats.ScalingX * Time.fixedDeltaTime;
            Stats.ScaleY.z += Stats.ScalingY * Time.fixedDeltaTime;
            transform.localScale = new Vector3(Stats.ScaleX.z, Stats.ScaleY.z, Stats.ScaleX.z);
        }
        transform.position = transform.position + V;
    }
    public void FixedUpdate()
    {
        
    }


    public void EmmitFunc()
    {
        GameObject Em = Instantiate(Stats.EmmitOption.Emmiter, transform.position, transform.rotation);
        Em.GetComponent<ParticuleManager>().Test = false;
        Stats.Screen.ListParti.Add(Em.GetComponent<ParticuleManager>());
    }
}

[System.Serializable]
public struct Parti
{
    public PerfectScreen2 Screen;
    public bool Burst;
    public Color Col;
    public Color ColLerp;
    public float SpeedColor;
    public Vector2 Direction;
    public Vector3 Speed;
    public Vector3 Lifeline;
    public Vector3 Acceleration;
    public Vector3 Gravity;
    public Sprite sprite;
    public Vector3 ScaleX;
    public Vector3 ScaleY;
    public bool ScaleEqualsBaseOnScaleXVector;
    public float ScalingX;
    public float ScalingY;
    public float degree;
    public float degreeAcc;
    public bool Reverse;
    public Decay DecayOption;
    public Emmit EmmitOption;
}

[System.Serializable]
public struct Emmit
{
    public bool Emmiting;
    public GameObject Emmiter;
}

[System.Serializable]
public struct Loop
{
    public bool IsLooping;
    public float TimeBeforeLoop;
    public float LoopTime;
    public bool Stack;
    public bool Release;
    public LoopPhase Phase;
    public int index;
}



[System.Serializable]
public struct Decay
{
    public bool IsDecay;
    public Color DecayCol;
    public float TimeDecay;
}

public enum LoopPhase
{
    NOTHING,
    BEFORELOOP,
    RECORD,
    DISPLAY,
}