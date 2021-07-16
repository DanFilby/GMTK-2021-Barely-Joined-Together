using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPart : MonoBehaviour
{
    public bool buildable;
    public PartType partType;
    public GameObject pointObj;
    public LayerMask layer;

    private void Start()
    {
        if (buildable){
            spawnPoints();
        }
    }

    private void spawnPoints()
    {
        if(partType == PartType.wheel){
            return;
        }
        //i know :/
        if(Physics.CheckSphere(transform.position + new Vector3(1,0,0), 0.1f, layer) == false)
        {
            Instantiate(pointObj, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
        }
        if (Physics.CheckSphere(transform.position + new Vector3(-1, 0, 0), 0.1f, layer) == false)
        {
            Instantiate(pointObj, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);
        }
        if (Physics.CheckSphere(transform.position + new Vector3(0, 0, 1), 0.1f, layer) == false)
        {
            Instantiate(pointObj, transform.position + new Vector3(0, 0, 1), Quaternion.identity);
        }
        if (Physics.CheckSphere(transform.position + new Vector3(0, 0, -1), 0.1f, layer) == false)
        {
            Instantiate(pointObj, transform.position + new Vector3(0, 0, -1), Quaternion.identity);
        }
    }


}

public enum PartType { floor,engine,wheel,none}
