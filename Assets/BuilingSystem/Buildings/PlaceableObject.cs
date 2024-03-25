using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public Renderer visible;
    public int buildingType;
    /*
        1 = Cryoconversation
        2 = LiquidNitrogen
        3 = LN Generator
    */
    
    //Capacity for Buildings bodies or LiquidNitrogen
    public int capacity;
    //Amount of LiquidNitrogen used by the building
    public int LNUsage;
    public int price;
    public int operatingCost;

    public Vector3Int Position;



    public bool Placed { get; private set; }
    public Vector3Int Size { get; private set;}
    private Vector3[] Vertices;

    public GameObject Silhoutte;

    private void GetColliderVertexPositionslocal()
    {
        BoxCollider b = gameObject.GetComponent<BoxCollider>();
        Vertices = new Vector3[4];
        Vertices[0] = b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f;
        Vertices[1] = b.center + new Vector3(b.size.x,  -b.size.y, -b.size.z) * 0.5f;
        Vertices[2] = b.center + new Vector3(b.size.x,  -b.size.y, b.size.z)  * 0.5f;
        Vertices[3] = b.center + new Vector3(-b.size.x, -b.size.y, b.size.z)  * 0.5f;
    }

    private void CalculateSizeInCells()
    {
        Vector3Int[] vertices = new Vector3Int[Vertices.Length];

        for(int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(Vertices[i]);
            vertices[i] = BuildingSys.current.gridLayout.WorldToCell(worldPos);
        }

        Size = new Vector3Int(Mathf.Abs((vertices[0] - vertices[1]).x), 
                                Mathf.Abs((vertices[0] - vertices[3]).y), 
                                1);
    }

    public Vector3 StartPosition => transform.TransformPoint(Vertices[0]);
    // Start is called before the first frame update
    private void Start()
    {
        GetColliderVertexPositionslocal();
        CalculateSizeInCells();

        //Object visible on initializatin
        visible = GetComponent<MeshRenderer>();
        visible.enabled= false;
    }

    public virtual void Place()
    {
        ObjectDrag drag = gameObject.GetComponent<ObjectDrag>();
        Destroy(drag);

        Placed = true;


        
        //invoke events of placement

        //NitrogenTank
        if(buildingType != 0)
        IncreaseStat();
    }

    private void IncreaseStat()
    {
        //Cryotank
        if(buildingType == 1)
        {
            PlayerManager.s_BodyTank += capacity;
            PlayerManager.NitrogenUsage += LNUsage;
        }

        //NitrogenTank
        else if(buildingType == 2)
        {
            PlayerManager.s_MaxLNitrogen += capacity;
        }

        //LN Generator
        else if(buildingType == 3)
        {
            PlayerManager.NitrogenGeneration += capacity;
            PlayerManager.s_operatingCost += operatingCost;
        }
    }

    public void Sold()
    {
        // Body Tank
        if(buildingType == 1)
        {
            PlayerManager.s_BodyTank -= capacity;
            if(PlayerManager.s_CurrentBodies > PlayerManager.s_BodyTank) PlayerManager.s_CurrentBodies = PlayerManager.s_BodyTank;
        }

        // LN Storage
        else if(buildingType == 2)
        {
            PlayerManager.NitrogenUsage -= LNUsage;
            PlayerManager.s_MaxLNitrogen -= capacity;
            if(PlayerManager.s_LiquidNitrogen > PlayerManager.s_MaxLNitrogen) PlayerManager.s_LiquidNitrogen = PlayerManager.s_MaxLNitrogen;
        }

        //LN Generator
        else if(buildingType == 3)
        {
            PlayerManager.NitrogenGeneration -= capacity;
            PlayerManager.s_operatingCost -= operatingCost;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
