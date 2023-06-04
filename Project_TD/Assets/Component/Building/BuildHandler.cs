using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.PlayerSettings;

public class BuildHandler : MonoBehaviour
{
    //
    public static BuildHandler instance;

    BuildingData currentData;
    GameObject currentShow;
    Grid grid;
    [SerializeField] GameObject greenHover;
    [SerializeField] GameObject redHover;

    Dictionary<Vector3, GameObject> builtDictionary = new();


    private void Awake()
    {
        instance = this;
        grid = GetComponent<Grid>();
    }

    [SerializeField] BuildingData debuggData;
    private void Start()
    {
        //StartBuilding(debuggData);
    }

    private void Update()
    {
        HandleBuilding();
    }

    //so how we will do this
    //


    //i want to get information of the current grid i am hovering as long as it is builadable.

    #region BUILDING

   
    void HandleBuilding()
    {
        if (currentData == null) return;
        if (currentShow == null) return;
        HandleHover();
        if (!CanBuildInLayer(currentData.buildableLayer.ToString()))
        {
            //if you cannot then we show nothing.
            DisableHover();
            currentShow.SetActive(false);
            return;
        }

        if (!IsGridFree(GetPlacePos()))
        {
            //we show red.
            Debug.Log("grid not free");
            currentShow.SetActive(false);
            RedHover();
            return;
        }

        if (!currentShow.activeInHierarchy) currentShow.SetActive(true);

        currentShow.transform.position = GetPlacePos();
        GreenHover();
        

        if (Input.GetMouseButtonDown(1))
        {
            //then we build the stuff.
            //Debug.Log("we built this " + currentData.name + " in here " + GetGridPos());
            Build(GetPlacePos());
        }
    }

    void Build(Vector3 pos)
    {
        //we put the object in the palce.
        GameObject newObject = Instantiate(currentData.buildPrefab, pos, Quaternion.identity);
        builtDictionary.Add(pos, newObject);
    }

    public void StartBuilding(BuildingData data)
    {
        //we get a fella in teh mouse.
        currentData = data;
        currentShow = Instantiate(data.showPrefab, GetGridPos(), Quaternion.identity);
    }

    public void StopBuilding()
    {

    }
    #endregion

    #region HOVER

    void HandleHover()
    {
        greenHover.transform.position = GetPlacePos();
        redHover.transform.position = GetPlacePos();
    }
    public void DisableHover()
    {
        greenHover.SetActive(false);
        redHover.SetActive(false);
    }
    void GreenHover()
    {
        greenHover.SetActive(true);
        redHover.SetActive(false);
    }
    void RedHover()
    {
        greenHover.SetActive(false);
        redHover.SetActive(true);
    }




    #endregion



    #region UTILS

    bool IsGridFree(Vector3 pos)
    {
        //we check if there is something else built there.
        return !builtDictionary.ContainsKey(pos);
    }
    bool CanBuildInLayer(string layer)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask(layer)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    Vector3 GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
    Vector3Int GetGridPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(GetMousePos());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            return grid.WorldToCell(hit.point);
        }
        return Vector3Int.zero;

    }

    Vector3 GetPlacePos()
    {
        Vector3 mousePosition = GetMousePos();
        Vector3Int gridPos = grid.WorldToCell(mousePosition);
        Vector3 newPos = grid.CellToWorld(gridPos);
        newPos.y = 0.05f;
        newPos.x += 0.5f;
        newPos.z += 0.5f;

        return newPos;
    }

    #endregion
}
