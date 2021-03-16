using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateParent : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotation;
    public float SpeedRotation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotation += SpeedRotation*Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
