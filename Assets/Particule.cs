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
    
    
    
    void Start()
    {
        NbrRound = Stats.Lifeline / Time.fixedDeltaTime;
        GetComponent<SpriteRenderer>().sprite = Stats.sprite;
        GetComponent<SpriteRenderer>().color = Stats.Col;      
        Acc = new Vector2(Mathf.Cos(Stats.degree), Mathf.Sin(Stats.degree));
        transform.localScale = new Vector3(Stats.Scale, Stats.Scale, Stats.Scale);
        if(Stats.Reverse)
        {
            float MagnitudeSpeed = (Stats.Speed + Stats.Acceleration*NbrRound/2) * NbrRound;
            transform.position += Acc * MagnitudeSpeed  *Time.fixedDeltaTime;

        }
        /*Debug.Log(0.5f + zou);*/
    }

    // Update is called once per frame
    void Update()
    {
        if(Stats.Lifeline<=0)
        {
            Destroy(gameObject);
        }
        GetComponent<SpriteRenderer>().color = Color.Lerp(Stats.Col, Stats.ColLerp, Life/Stats.SpeedColor);
    }
    public void FixedUpdate()
    {
        Life += Time.fixedDeltaTime;
        Stats.Lifeline -= Time.fixedDeltaTime;
        Stats.Speed += Stats.Acceleration;
        V = Acc * Stats.Speed + Stats.Gravity;
        V *= Time.fixedDeltaTime;
        Round += 1;
        Counter += V.magnitude;
        if (Stats.Reverse)
        {
            V = -V;
        }
        transform.position = transform.position + V;
    }

}

[System.Serializable]
public struct Parti
{
    public Color Col;
    public Color ColLerp;
    public float SpeedColor;
    public Vector2 Direction;
    public float Speed;
    public float Lifeline;
    public float Acceleration;
    public Vector3 Gravity;
    public Sprite sprite;
    public float Scale;
    public float degree;
    public bool Reverse;
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

public enum LoopPhase
{
    NOTHING,
    BEFORELOOP,
    RECORD,
    DISPLAY,
}