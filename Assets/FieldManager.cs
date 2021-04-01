using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PrefabParticule;
    public FieldZone Zone;
    public Parti ParticuleStats;
    public GameObject Parent;
    public int NbrEmmiter;
    public float DelayEmmit;
    public float DelayEmmitSet;
    public List<Parti> ListParticuleForLoop;
    public List<Vector3> ListTransform;
    public Loop LoopOption;
    public bool Test;
    
    private bool Activate = true;
    void Start()
    {
        DelayEmmitSet = DelayEmmit;
        DelayEmmit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Activate)
        {
            if (DelayEmmit <= 0)
            {
                if (LoopOption.IsLooping == false || LoopOption.Phase == LoopPhase.BEFORELOOP || LoopOption.Phase == LoopPhase.RECORD)
                {
                    for (int i = 0; i < NbrEmmiter; i++)
                    {
                        if (Parent)
                        {
                            Vector3 Position = new Vector3(Random.Range(Zone.Min.x, Zone.Max.x), Random.Range(Zone.Min.y, Zone.Max.y), 0);
                            GameObject Obj = Instantiate(PrefabParticule, Parent.transform.position + Position, Parent.transform.rotation, Parent.transform);
                            ParticuleStats = BuildParti(ParticuleStats);
                            Obj.GetComponent<Particule>().Stats = ParticuleStats;
                            if (LoopOption.Phase == LoopPhase.RECORD)
                            {
                                ListParticuleForLoop.Add(ParticuleStats);
                                ListTransform.Add(Position);
                            }

                        }
                        else
                        {
                            Vector3 Position = new Vector3(Random.Range(Zone.Min.x, Zone.Max.x), Random.Range(Zone.Min.y, Zone.Max.y), 0);
                            GameObject Obj = Instantiate(PrefabParticule, transform.position + Position, transform.rotation);
                            ParticuleStats = BuildParti(ParticuleStats);
                            Obj.GetComponent<Particule>().Stats = ParticuleStats;
                            if (LoopOption.Phase == LoopPhase.RECORD)
                            {
                                ListParticuleForLoop.Add(ParticuleStats);
                                ListTransform.Add(Position);
                            }

                        }
                    }
                    DelayEmmit = DelayEmmitSet;
                }
                else if (LoopOption.Phase == LoopPhase.DISPLAY)
                {
                    for (int i = 0; i < NbrEmmiter; i++)
                    {
                        if (Parent)
                        {
                            Vector3 Pos = ListTransform[LoopOption.index];
                            GameObject Obj = Instantiate(PrefabParticule, Parent.transform.position + Pos, Parent.transform.rotation, Parent.transform);
                            Obj.GetComponent<Particule>().Stats = ListParticuleForLoop[LoopOption.index];
                            LoopOption.index += 1;
                            if (LoopOption.index == ListParticuleForLoop.Count)
                            {
                                LoopOption.index = 0;
                            }
                            DelayEmmit = DelayEmmitSet;
                        }
                        else
                        {
                            Vector3 Pos = ListTransform[LoopOption.index];
                            GameObject Obj = Instantiate(PrefabParticule, transform.position + Pos, transform.rotation);
                            Obj.GetComponent<Particule>().Stats = ListParticuleForLoop[LoopOption.index];
                            LoopOption.index += 1;
                            if (LoopOption.index == ListParticuleForLoop.Count)
                            {
                                LoopOption.index = 0;
                            }
                            DelayEmmit = DelayEmmitSet;
                        }
                    }
                }

            }
            if (LoopOption.IsLooping)
            {
                if (LoopOption.Phase == LoopPhase.BEFORELOOP)
                {
                    if (LoopOption.TimeBeforeLoop > 0)
                    {
                        LoopOption.TimeBeforeLoop -= Time.deltaTime;
                    }
                    else
                    {
                        LoopOption.Phase = LoopPhase.RECORD;
                    }
                }
                else if (LoopOption.Phase == LoopPhase.RECORD)
                {
                    if (LoopOption.LoopTime > 0)
                    {
                        LoopOption.LoopTime -= Time.deltaTime;
                    }
                    else
                    {
                        LoopOption.Phase = LoopPhase.DISPLAY;
                    }
                }
                else if (LoopOption.Phase == LoopPhase.DISPLAY)
                {

                }
            }

            if (LoopOption.TimeBeforeLoop >= 0 && LoopOption.IsLooping)
            {
                LoopOption.TimeBeforeLoop -= Time.deltaTime;
            }

            if (DelayEmmit > 0)
            {
                DelayEmmit -= Time.deltaTime;
            }
        }
        else
        {
            if (!Test)
            {
                Destroy(gameObject);
            }
        }
    }


    public Parti BuildParti(Parti parti)
    {
        parti.degree = Mathf.Deg2Rad * Random.Range(ParticuleStats.Direction.x, ParticuleStats.Direction.y);
        parti.Speed.z = Random.Range(parti.Speed.x, parti.Speed.y);
        parti.Scale.z = Random.Range(parti.Scale.x, parti.Scale.y);
        parti.Lifeline.z = Random.Range(parti.Lifeline.x, parti.Lifeline.y);
        parti.Acceleration.z = Random.Range(parti.Acceleration.x, parti.Acceleration.y);
        if (parti.Burst)
        {
            Activate = false;
        }
        return parti;
    }

}

[System.Serializable]
public struct FieldZone
{
    public Vector3 Min;
    public Vector3 Max;
}