using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particule : MonoBehaviour
{
    // Start is called before the first frame update

    public Parti Stats;
    public float Life;
    public Vector3 Acc;
    
    
    void Start()
    {
        GetComponent<SpriteRenderer>().color = Stats.Col;
        float degree = Mathf.Deg2Rad * Random.Range(Stats.Direction.x, Stats.Direction.y);
        Acc = new Vector2(Mathf.Cos(degree), Mathf.Sin(degree));
    }

    // Update is called once per frame
    void Update()
    {
        Life += Time.deltaTime;
        Stats.Speed += Stats.Acceleration;
        Vector3 V = Acc * Stats.Speed+ Stats.Gravity;
        V *= Time.deltaTime;
        transform.position = transform.position + V;
        if(Stats.Lifeline<=0)
        {
            Destroy(gameObject);
        }else
        {
            Stats.Lifeline -= Time.deltaTime;
            
        }
        GetComponent<SpriteRenderer>().color = Color.Lerp(Stats.Col, Stats.ColLerp, Life/Stats.SpeedColor);
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
}
