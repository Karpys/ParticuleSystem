using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticuleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PrefabParticule;
    public Parti ParticuleStats;
    public GameObject Parent;
    public int NbrEmmiter;
    public float DelayEmmit;
    public float DelayEmmitSet;
    void Start()
    {
        DelayEmmitSet = DelayEmmit;
        DelayEmmit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(DelayEmmit<=0)
        {
            for(int i=0;i< NbrEmmiter; i++)
            {
                if(Parent)
                {
                    GameObject Obj = Instantiate(PrefabParticule, transform.position, transform.rotation,Parent.transform);
                    Obj.GetComponent<Particule>().Stats = ParticuleStats;
                }
                else
                {
                    GameObject Obj = Instantiate(PrefabParticule, transform.position, transform.rotation);
                    Obj.GetComponent<Particule>().Stats = ParticuleStats;
                }
            
            }
            DelayEmmit = DelayEmmitSet;
        }
        if(DelayEmmit>0)
        {
        DelayEmmit -= Time.deltaTime;
        }
    }
}
