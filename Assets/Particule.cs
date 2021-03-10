using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particule : MonoBehaviour
{
    // Start is called before the first frame update

    public Parti Stats;
    
    void Start()
    {
        GetComponent<SpriteRenderer>().color = Stats.Col;
        float degree = Mathf.Deg2Rad * Random.Range(0,Stats.Direction);
        Vector2 Dir = new Vector2(Mathf.Cos(degree), Mathf.Sin(degree));
        GetComponent<Rigidbody2D>().AddForce(Dir * Stats.Speed / GlobalVariable.SpeedDivi);
    }

    // Update is called once per frame
    void Update()
    {
        if(Stats.Lifeline<=0)
        {
            Destroy(gameObject);
        }else
        {
            Stats.Lifeline -= Time.deltaTime;
            
        }
    }
}

[System.Serializable]
public struct Parti
{
    public Color Col;
    public float Direction;
    public float Speed;
    public float Lifeline;
}
