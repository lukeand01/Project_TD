using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHQ : MonoBehaviour
{
    [SerializeField] float initialHealth;
    float maxHealth;
    float currentHealth;

    private void Awake()
    {
        maxHealth = initialHealth;
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Enemy") return;

        //but if an enemy ever touches this then it takes damge.
        TakeDamage(collision.gameObject.GetComponent<EnemyBase>().debugDamage);
    }
}
