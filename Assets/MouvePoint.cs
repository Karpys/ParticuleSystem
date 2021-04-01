using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvePoint : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> ListTransform;
    public AnimationCurve Curve;
    public bool loop;
    public float life;
    public int index;
    void Start()
    {
        transform.position = ListTransform[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        life += Time.deltaTime;
        if(Curve.Evaluate(life)!=1)
        {
            transform.position = Vector3.Lerp(ListTransform[index].position, ListTransform[index + 1].position, Curve.Evaluate(life));
        }else if(index<ListTransform.Count-2)
        {
            index += 1;
            life = 0;
        }else if(loop)
        {
            index = 0;
            life = 0;
        }
    }
}
