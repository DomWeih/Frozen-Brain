using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BuildingSys : MonoBehaviour
{
    [SerializeField] public static BuildingSys current;
    [SerializeField] public GridLayout gridLayout;
    [SerializeField] private Tilemap MainTilemap;
    [SerializeField] private Tilemap TempTilemap;
    [SerializeField] private TileBase whiteTile;
    [SerializeField] private TileBase redTile;
    [SerializeField] private TileBase greenTile;
    [SerializeField] private TileBase unbuildableTile;

    #region Buildings
    [SerializeField] public GameObject prefab1;
    [SerializeField] public GameObject prefab2;
    private GameObject buildingSilhouette;
    [SerializeField] public Material BuildableSil;
    [SerializeField] public Material UnbuildableSil;

    #endregion

    private PlaceableObject objectToPlace;
    private SilhouetteObject silhoutteToPlace;
    private Grid grid;
    private Vector3 prevPos;
    private Renderer visible;

    private bool selling = false;
    [SerializeField] private string selectableTag = "Building";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] public GameObject SellBackground;
    private Material defaultMaterial;

   
     


    private Transform _selection;

    #region Unity methods

    public void Awake()
    {
        current = this;
        visible = MainTilemap.GetComponent<TilemapRenderer>();
        visible.enabled = false;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void Update()
    {
        if (objectToPlace != null && !UIScript.mouse_over)
        {
            //Change color of Building Silhouette, if building is possible
            objectToPlace.transform.position = silhoutteToPlace.transform.position;
            if (!CanBePlaced(objectToPlace, objectToPlace.price))
            {
                //Building not possible = red silhouette
                silhoutteToPlace.GetComponent<Renderer>().material = UnbuildableSil;
            }
            else
            {
                //building possible = green silhouette
                silhoutteToPlace.GetComponent<Renderer>().material = BuildableSil;
            }

            //Place object
            if(Input.GetMouseButtonDown(0))
            {                
                if (CanBePlaced(objectToPlace, objectToPlace.price))
                {
                    objectToPlace.Place();
                    Vector3Int start = gridLayout.WorldToCell(objectToPlace.StartPosition);

                    objectToPlace.Position = start;

                    TakeArea(start, objectToPlace.Size);
                    
                    //Pay for Building
                    PlayerManager.s_Money -= objectToPlace.price;
                    //Make Object Visible
                    objectToPlace.visible.enabled = true;
                    objectToPlace = null;

                    Destroy(silhoutteToPlace.gameObject);

                    //Tilemap invisible
                    visible.enabled = false;

                    TempTilemap.ClearAllTiles();

                    
                }
                else
                {
                    return;
                }
            }
            
            //cancel building
            else if(Input.GetKeyDown(KeyCode.Escape))
            {
                TempTilemap.ClearAllTiles();

                Destroy(objectToPlace.gameObject);
                Destroy(silhoutteToPlace.gameObject);
                visible.enabled = false;
            }
        }
        

        //Sell Objects
        if (selling && !UIScript.mouse_over)
        {             
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                SellBuilding();
            }
            
            if (_selection != null)
            {
                var selectionRenderer = _selection.GetComponent<Renderer>();
                selectionRenderer.material = defaultMaterial;
                _selection = null;
                defaultMaterial = null;
            }
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                if (selection.CompareTag(selectableTag))
                {
                    var selectionRenderer = selection.GetComponent<Renderer>();
                    
                    if (selectionRenderer != null)
                    {
                        if(defaultMaterial == null)
                        {
                            defaultMaterial = selectionRenderer.material;
                        }
                        selectionRenderer.material = highlightMaterial;
                    }

                    _selection = selection;
                }
                if(Input.GetMouseButtonDown(0))
                {
                    PlaceableObject selectionObject = selection.GetComponent<PlaceableObject>();
                    BoxCollider b = selection.GetComponent<BoxCollider>();

                    ClearArea(selectionObject.Position, selectionObject.Size);

                    selectionObject.Sold();

                    Destroy(selectionObject.gameObject);
                }
            }

        }
    }




    #endregion
    #region Utils

    public static Vector3 getMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }
        return array;
    }

    #endregion

    #region Building Placement

    public void InitializeWithObject(GameObject prefab, GameObject siluette)
    {
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);

        Vector3 mousePos= getMouseWorldPosition();



        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        GameObject sil = Instantiate(siluette, mousePos, Quaternion.identity);
        objectToPlace = obj.GetComponent<PlaceableObject>();
        silhoutteToPlace = sil.GetComponent<SilhouetteObject>();
        obj.AddComponent<ObjectDrag>();
        sil.AddComponent<SiluetteDrag>();
    }

    //Old Building selection
    // public void PlaceBuilding(int buildingNr)
    // {
    //     Renderer visible = MainTilemap.GetComponent<TilemapRenderer>();
    //     visible.enabled = true;
    //     if(buildingNr == 1)
    //     {
    //         if(objectToPlace != null)
    //         {
    //             Destroy(silhoutteToPlace.gameObject);
    //             objectToPlace = null;
    //         }
    //         InitializeWithObject(prefab1, buildungSilhouette);
    //     }
    //     else if(buildingNr == 2)
    //     {
    //         if(objectToPlace != null)
    //         {
    //             Destroy(silhoutteToPlace.gameObject);
    //             objectToPlace = null;
    //         }
    //         InitializeWithObject(prefab2, buildungSilhouette);
    //     }
    // }
    public void PlaceBuilding(GameObject building)
    {
        selling = false;
        Renderer visible = MainTilemap.GetComponent<TilemapRenderer>();

        PlaceableObject getSilhoutte = building.GetComponent<PlaceableObject>();

        visible.enabled = true;
        if(objectToPlace != null)
            {
                Destroy(silhoutteToPlace.gameObject);
                objectToPlace = null;
            }
        InitializeWithObject(building, getSilhoutte.Silhoutte);
    }

    public bool CanBePlaced(PlaceableObject placeableObject, int price)
    {
        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(objectToPlace.StartPosition);
        area.size = placeableObject.Size;
        area.size = new Vector3Int(area.size.x + 1, area.size.y + 1, area.size.y);

        TileBase[] baseArray = GetTilesBlock(area, MainTilemap);

        if(prevPos != area.position)
                {
                    prevPos = area.position;
                    TempTilemap.ClearAllTiles();
                }

        Vector3Int start = gridLayout.WorldToCell(objectToPlace.StartPosition);
        Vector3Int size = objectToPlace.Size;

        if(PlayerManager.s_Money >= price)
        {
            foreach (var b in baseArray)
            {
                if (b == whiteTile | b == unbuildableTile)
                {
                    //shows red if not placable
                    TempTilemap.BoxFill(start, redTile, start.x, start.y,
                                start.x + size.x, start.y + size.y);

                    return false;
                }
            }
        }   
        else return false;
        //shows green if placable
        TempTilemap.BoxFill(start, greenTile, start.x, start.y,
                             start.x + size.x, start.y + size.y);
        return true;

    }

    public void TakeArea(Vector3Int start, Vector3Int size)
    {
        MainTilemap.BoxFill(start, whiteTile, start.x, start.y,
                             start.x + size.x, start.y + size.y);

                             Debug.Log(start);
    }
    




    public void SellBuilding()
    {
        if (selling)
        { 
            selling = false;
            SellBackground.SetActive(false);
        }
        else if (!selling)
        {
            selling = true;
            SellBackground.SetActive(true); 
        }
    }

    public void ClearArea(Vector3Int start, Vector3Int size)
    {
        Debug.Log("Sold");
        MainTilemap.BoxFill(start, null, start.x, start.y,
                             start.x + size.x, start.y + size.y);
                             Debug.Log(start);
    }


    
    #endregion
}
