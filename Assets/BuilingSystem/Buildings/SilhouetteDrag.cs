using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SiluetteDrag : MonoBehaviour
{
    private Vector3 pos;

    private void Update()
    {
        pos = BuildingSys.getMouseWorldPosition();
        transform.position = BuildingSys.current.SnapCoordinateToGrid(pos);
    }
}
