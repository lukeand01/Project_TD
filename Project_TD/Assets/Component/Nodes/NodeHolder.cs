using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHolder : MonoBehaviour
{
    public List<Transform> nodeList = new();

    public void SetUp()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            nodeList.Add(transform.GetChild(i));
        }
    }

    public Vector3 GetFirstPos() => nodeList[0].position;

}
