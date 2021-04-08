using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstKeyCode : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticuleManager Particule;
    public KeyCode Key;
    public bool reverse;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(Key))
        {
            Particule.CanSpawn = reverse;
        }else
        {
            Particule.CanSpawn = !reverse;
        }
    }
}
