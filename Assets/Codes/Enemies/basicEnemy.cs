using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicEnemy : MonoBehaviour
{
    [SerializeField]
    float health;
    Transform target;

    [SerializeField]
    float speed = 3f;

    [SerializeField]
    float rotateSpeed = 0.0025f;
    Rigidbody2D rb;

    [SerializeField]
    float distanceToShoot = 5f;

    [SerializeField]
    float distanceToStop = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(target.position, transform.position) >= distanceToStop)
            rb.velocity = transform.up * speed;
        RotatesTowardsTarget();
    }

    void RotatesTowardsTarget()
    {
        Vector2 targetDir = target.position - transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

    public void hurt(float damage)
    {
        health -= damage;
        if (health <= 0)
            die();
    }

    void die() { }
}
