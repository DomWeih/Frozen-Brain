using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;

    private void OnMouseDown()
    {
        offset = transform.position - BuildingSys.getMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        Vector3 pos = BuildingSys.getMouseWorldPosition() + offset;
        transform.position = BuildingSys.current.SnapCoordinateToGrid(pos);
    }
    
}
