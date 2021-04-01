using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateParent : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotation;
    public float SpeedRotation;
    public float Timeloop;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Timeloop = 360/SpeedRotation;
        rotation += SpeedRotation * Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(0, 0, rotation);

    }
}
