using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouetteObject : MonoBehaviour
{
    private Vector3[] Vertices;
    public Vector3 StartPosition => transform.TransformPoint(Vertices[0]);
    private void Start()
    {

    }

    void Update()
    {
        
    }
}
