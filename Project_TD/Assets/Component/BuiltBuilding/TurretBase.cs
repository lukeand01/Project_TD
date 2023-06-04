using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBase : BuildingBase
{
    //its effects and behavior are composite.

    GameObject currentTarget;
    [SerializeField] float range;
    [SerializeField] float attackCooldown;
    [SerializeField] BulletBase bulletTemplate;
    [SerializeField] Transform bulletPoint;
    float currentAttackCooldown;
    

    public void SetUp()
    {

    }

    private void Update()
    {
        if(currentTarget != null)
        {
            //rotate towards 
            RotateTower();
            HandleShooting();
        }
        else
        {
            currentTarget = GetTarget();
        }
    }

    GameObject GetTarget()
    {
        RaycastHit[] collided = Physics.SphereCastAll(transform.position, range, Vector3.up, 1, LayerMask.GetMask("Enemy"));

        if (collided.Length == 0)
        {
            return null;
        }


        float lowestDistance = range;
        GameObject currentTarget = null;
        foreach (var item in collided)
        {
            float distance = Vector3.Distance(item.transform.position, transform.position);
            if (distance <= lowestDistance)
            {
                lowestDistance = distance;
                currentTarget = item.collider.gameObject;
            }
        }

        return currentTarget;

    }
    [SerializeField] Vector3 correction;
    void RotateTower()
    {
        Vector3 lookPos = (currentTarget.transform.position - transform.position) + correction; 
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
    }

    void HandleShooting()
    {
        if(currentAttackCooldown > attackCooldown)
        {
            Shoot();
            currentAttackCooldown = 0;
        }
        else
        {
            currentAttackCooldown += Time.deltaTime;
        }
    }

    void Shoot()
    {
        //bullets always follow 
        if(currentTarget == null)
        {
            Debug.LogError("COULDNT SHOOT");
        }
        BulletBase newObject = Instantiate(bulletTemplate, bulletPoint.position, Quaternion.identity);
        newObject.SetUp(currentTarget);
    }
}
