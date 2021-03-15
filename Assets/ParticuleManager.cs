using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticuleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PrefabParticule;
    public Parti ParticuleStats;
    public GameObject Parent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<2; i++)
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
    }
}
