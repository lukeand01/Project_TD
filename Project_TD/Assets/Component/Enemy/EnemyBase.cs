using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.ScrollRect;

public class EnemyBase : MonoBehaviour, IDamageable
{
    //they not always walk towards the target. they might have other goals.

    //for now it will just start moving forward.
    public float debugDamage;
    public float debugSpeed;

    [SerializeField]float initialHealth;
    float maxHealth;
    float currentHealth;


    NodeHolder nodeHolder = new();

    private void Awake()
    {
        maxHealth = initialHealth;
        currentHealth = maxHealth;
        
    }
    private void Start()
    {
        
    }

    public void SetUp(NodeHolder nodeHolder)
    {
        this.nodeHolder = nodeHolder;
        //start walking to the next and keep track of where you are at.
        StartCoroutine(MoveProcess());
    }

    IEnumerator MoveProcess()
    {
        List<Transform> nodeList = nodeHolder.nodeList;
        for (int i = 1; i < nodeList.Count; i++)
        {

            while (Vector3.Distance(transform.position, nodeList[i].position) > 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, nodeList[i].position, Time.deltaTime * debugSpeed);
                yield return new WaitForSeconds(0.0008f);
            }

            if(i + 1 < nodeList.Count)
            {
                transform.Rotate(new Vector3(0, 90 * GetRotationDir(nodeList[i + 1].position), 0));
            }
            
        }

    }

    int GetRotationDir(Vector3 pos)
    {

        if (pos.x > transform.position.x) return 1;
        if (pos.x < transform.position.x) return -1;
        Debug.Log("its equal");
        return 0;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
