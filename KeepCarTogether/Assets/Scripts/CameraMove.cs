using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    public float mouseSens;
    Camera cam;

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        float x = Input.GetAxis("Mouse X");
        player.transform.Rotate(0,x * mouseSens, 0);
        float y = Input.GetAxis("Mouse Y");
        cam.transform.Rotate(y * mouseSens * -1,0, 0);
    }

}
