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
    public List<Parti> ListParticuleForLoop;
    public Loop LoopOption;
    public bool Test;
    private bool Activate=true;
    public bool CanSpawn;
    void Start()
    {
        DelayEmmitSet = DelayEmmit;
        DelayEmmit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Acti()
    {
        if (Activate )
        {
            if (DelayEmmit <= 0 && CanSpawn)
            {
                if (LoopOption.IsLooping == false || LoopOption.Phase == LoopPhase.BEFORELOOP || LoopOption.Phase == LoopPhase.RECORD)
                {
                    for (int i = 0; i < NbrEmmiter; i++)
                    {
                        if (Parent)
                        {
                            GameObject Obj = Instantiate(PrefabParticule, Parent.transform.position, Parent.transform.rotation, Parent.transform);
                            ParticuleStats = BuildParti(ParticuleStats);
                            Obj.GetComponent<Particule>().Stats = ParticuleStats;
                            if (LoopOption.Phase == LoopPhase.RECORD)
                            {
                                ListParticuleForLoop.Add(ParticuleStats);
                            }

                        }
                        else
                        {
                            GameObject Obj = Instantiate(PrefabParticule, transform.position, transform.rotation);
                            ParticuleStats = BuildParti(ParticuleStats);
                            Obj.GetComponent<Particule>().Stats = ParticuleStats;
                            if (LoopOption.Phase == LoopPhase.RECORD)
                            {
                                ListParticuleForLoop.Add(ParticuleStats);
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
                            GameObject Obj = Instantiate(PrefabParticule, Parent.transform.position, Parent.transform.rotation, Parent.transform);
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
                            GameObject Obj = Instantiate(PrefabParticule, transform.position, transform.rotation);
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
                ParticuleStats.Screen.ListParti.Remove(this);
                Destroy(gameObject);
            }
        }
    }


    public Parti BuildParti(Parti parti)
    {
        parti.degree = Mathf.Deg2Rad * Random.Range(ParticuleStats.Direction.x, ParticuleStats.Direction.y);
        parti.Speed.z = Random.Range(parti.Speed.x, parti.Speed.y);
        parti.ScaleX.z = Random.Range(parti.ScaleX.x, parti.ScaleX.y);
        parti.ScaleY.z = Random.Range(parti.ScaleY.x, parti.ScaleY.y);
        parti.Lifeline.z = Random.Range(parti.Lifeline.x, parti.Lifeline.y);
        parti.Acceleration.z = Random.Range(parti.Acceleration.x, parti.Acceleration.y);
        if(parti.Burst)
        {
            Activate = false;
        }
        return parti;
    }
}
