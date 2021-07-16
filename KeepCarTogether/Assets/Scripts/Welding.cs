using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Welding : MonoBehaviour
{
    public GameObject centrePiece;
    public GameObject WeldSpot;
    public float minDisBtwWeld;
    public MouseRaycaster mousePointer;

    private GameObject weldParent;
    private Vector3 lastWeldSpot;

    public Car car;

    public int totalCount = 0;

    void Start()
    {
        weldParent = new GameObject("Weld Parent");
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && mousePointer.currentObj.layer == 8)
        {
            Vector3 pos = mousePointer.currentHitPos;
            if(Vector3.Distance(pos, lastWeldSpot) >= minDisBtwWeld)
            {
                GameObject g = Instantiate(WeldSpot, pos, Quaternion.identity, weldParent.transform);
                car.WeldSpots.Add(g);
                car.Sturdiness++;
                car.UpdateText();
                lastWeldSpot = pos;
                totalCount++;
            }
        }  
    }
}
