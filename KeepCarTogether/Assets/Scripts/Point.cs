using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    static GameObject PointParent;

    private void Start()
    {
        if(PointParent == null)
        {
            PointParent = new GameObject("Point Parent");
        }
        transform.parent = PointParent.transform;
    }

    public Vector3 pos
    {
        get { return transform.position; }
    }
}
