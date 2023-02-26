using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nobleEnemy : Enemy
{
    [SerializeField]
    float health = 6;

    Rigidbody2D rb;

    [SerializeField]
    GameObject spark;
    Animator cemera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cemera = GameObject.Find("Main Camera").GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate() { }

    public void hurt(float damage)
    {
        GameObject sparkInstance = Instantiate(spark, transform.position, transform.rotation);
        Object.Destroy(sparkInstance, 2.0f);
        cemera.Play("cemarashake");
        health -= damage;
        if (health <= 0)
            die();
    }

    void die()
    {
        GameObject sparkInstance = Instantiate(spark, transform.position, transform.rotation);
        Object.Destroy(sparkInstance, 2.0f);
        cemera.Play("cemarashake");
        Destroy(gameObject);
    }

    public override void damage(float damage)
    {
        health -= damage;
        if (health <= 0)
            die();
    }
}
