using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    GameObject currentTarget;
    public void SetUp(GameObject target)
    {
        currentTarget = target;
    }

    private void Update()
    {
        if (currentTarget == null) return;
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, Time.deltaTime * 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //
        if(collision.gameObject != currentTarget)
        {
            Debug.Log("not the target");
            return;
        }

        Debug.Log("got the target");
        collision.gameObject.GetComponent<IDamageable>().TakeDamage(10);
        Destroy(gameObject);

    }
}
