using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseRaycaster : MonoBehaviour
{
    public Camera cam;
    public LayerMask interactionLayer;
    public float interactionRange;

    public GameObject currentObj;
    public Vector3 currentHitPos;

    public UnityAction<PartType> PickedPart;
    public UnityAction<Point> PlacePart;
 
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionLayer))
        {
            //Debug.Log(hit.collider.gameObject.name);
            currentObj = hit.collider.gameObject;
            currentHitPos = hit.point;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            interact();
        }
    }

    void interact()
    {
        if(currentObj != null)
        {
            if(currentObj.TryGetComponent<CarPart>(out CarPart part))
            {
                if (PickedPart != null)
                {
                    PickedPart.Invoke(part.partType);
                }
            }

            if (currentObj.TryGetComponent<Point>(out Point point))
            {
                if (PlacePart != null)
                {
                    PlacePart.Invoke(point);
                }
            }
        }
    }

}
