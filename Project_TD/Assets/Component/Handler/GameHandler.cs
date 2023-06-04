using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    //this is can be unique per phase.
    //types of behavior should be here.
    //like the ai of hte "enemy commander". 
    //
    [SerializeField] EnemyBase debugEnemy;

    public List<NodeHolder> nodeHolderList = new();


    private void Awake()
    {
        foreach (var item in nodeHolderList)
        {
            item.SetUp();
        }

        StartCoroutine(DebugSpawnLoop());
    }

    IEnumerator DebugSpawnLoop()
    {
        yield return new WaitForSeconds(2);
        SpawnEnemy();
        StartCoroutine(DebugSpawnLoop());
    }


    [ContextMenu("SPAWN ENEMY")]
    public void SpawnEnemy()
    {
        NodeHolder node = nodeHolderList[Random.Range(0, nodeHolderList.Count)];
        EnemyBase newObject = Instantiate(debugEnemy, node.GetFirstPos(), Quaternion.identity);
        newObject.SetUp(node);
    }

}
