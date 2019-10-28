using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public LayerMask obstacleMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var position = GetMousePositionWithOrthographicCamera();
            if (position != null)
            {
                MoveToPosition(position.Value);
            }
        }
    }

    private void MoveToPosition(Vector3 position)
    {
        transform.position = position;
    }

    private Vector3? GetMousePositionInWorldCoordinates()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        RaycastHit2D[] hits = new RaycastHit2D[1];
        if(Physics2D.GetRayIntersectionNonAlloc(ray,hits)>0 && obstacleMask.value != (1 << hits[0].collider.gameObject.layer))
        {
            return hits[0].point;
        }
        return null;

    }

    private Vector3? GetMousePositionWithOrthographicCamera()
    {
        var mouseInput = Input.mousePosition; //Input.GetTouch(0)
        var mousePosition = Camera.main.ScreenToWorldPoint(mouseInput);
        mousePosition.z = 0;
        return mousePosition;
    }

}
