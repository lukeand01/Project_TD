using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuildingData : ScriptableObject
{

    public GameObject showPrefab;
    public GameObject buildPrefab;
    public Vector3Int size;
    public string buildableLayer;

}
