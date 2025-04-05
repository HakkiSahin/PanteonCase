using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    public float damage = 2f;
    private Vector3 targetPosition;
    private bool isStop;
    private Transform bulletOwner;
    
    public void Initialize(Vector3 target, float damageAmount, Transform currentSoldier)
    {
        targetPosition = target;
        damage = damageAmount;
        bulletOwner = currentSoldier;
    }

    private void Update()
    {
        if (!isStop)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Soldier") || other.CompareTag("Build"))
        {
            Debug.Log("in here");
            if (other.transform != bulletOwner)
            {
                 isStop = true;
                 other.GetComponent<HealthController>().TakeDamage(damage);
                 Destroy(gameObject);
            }
        }
    }
}
