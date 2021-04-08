using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PrefabParticule;
    public FieldType Type;
    public FieldZone Zone;
    public CircleZone CircleOption;
    public Parti ParticuleStats;
    public GameObject Parent;
    public int NbrEmmiter;
    public float DelayEmmit;
    public float DelayEmmitSet;
    public List<Parti> ListParticuleForLoop;
    public List<Vector3> ListTransform;
    public Loop LoopOption;
    public bool Test;
    public bool CanSpawn;
    
    private bool Activate = true;
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
                            Vector3 Pos = BuildPos();
                            GameObject Obj = Instantiate(PrefabParticule, Parent.transform.position + Pos, Parent.transform.rotation, Parent.transform);
                            ParticuleStats = BuildParti(ParticuleStats);
                            Obj.GetComponent<Particule>().Stats = ParticuleStats;
                            if (LoopOption.Phase == LoopPhase.RECORD)
                            {
                                ListParticuleForLoop.Add(ParticuleStats);
                                ListTransform.Add(Pos);
                            }

                        }
                        else
                        {
                            Vector3 Pos = BuildPos();
                            GameObject Obj = Instantiate(PrefabParticule, transform.position + Pos, transform.rotation);
                            ParticuleStats = BuildParti(ParticuleStats);
                            Obj.GetComponent<Particule>().Stats = ParticuleStats;
                            if (LoopOption.Phase == LoopPhase.RECORD)
                            {
                                ListParticuleForLoop.Add(ParticuleStats);
                                ListTransform.Add(Pos);
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

    public Vector3 BuildPos()
    {
        Vector3 Position = new Vector3(0,0,0);
        if(Type==FieldType.CIRCLE)
        {
            CircleOption.Direction.z = Random.Range(CircleOption.Direction.x, CircleOption.Direction.y);
            CircleOption.Direction.z = CircleOption.Direction.z * Mathf.Deg2Rad;
            Vector2 Magni = new Vector2(Mathf.Cos(CircleOption.Direction.z), Mathf.Sin(CircleOption.Direction.z));
            CircleOption.Radius.z = Random.Range(CircleOption.Radius.x, CircleOption.Radius.y);
            Position = new Vector3(Magni.x * CircleOption.Radius.z, Magni.y * CircleOption.Radius.z, 0);
        }
        else if(Type==FieldType.SHAPE)
        {
            Position = new Vector3(Random.Range(Zone.Min.x, Zone.Max.x), Random.Range(Zone.Min.y, Zone.Max.y), 0);
        }
        return Position;
    }


    public Parti BuildParti(Parti parti)
    {
        if(CircleOption.FollowDirection)
        {
            parti.degree = CircleOption.Direction.z;
        }
        else
        {
            parti.degree = Mathf.Deg2Rad * Random.Range(ParticuleStats.Direction.x, ParticuleStats.Direction.y);
        }
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

[System.Serializable]
public struct CircleZone
{
    public Vector3 Radius;
    public Vector3 Direction;
    public bool FollowDirection;
}

public enum FieldType
{
    SHAPE,
    CIRCLE
}