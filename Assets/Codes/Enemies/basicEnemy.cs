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

    Rigidbody2D rb;

    [SerializeField]
    float distanceToShoot = 5f;

    [SerializeField]
    float distanceToStop = 3f;
    Transform weapon;
    Vector3 playerPos;
    Vector3 aimDir;
    float flip = 1;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        weapon = gameObject.transform.Find("Weapon");
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
        rotateGun();
        if (Vector2.Distance(target.position, transform.position) <= distanceToShoot)
            {anim.SetBool("isShooting", true);
            shoot();
            }
        else
            {stopShoot();
            anim.SetBool("isShooting", false);
            }
    }

    void move()
    {
        Vector2 targetDir = target.position - transform.position;
        if (Vector2.Distance(target.position, transform.position) >= distanceToStop)
        {
            rb.velocity = targetDir.normalized * speed;
        }
        else
        {
            rb.velocity = Vector2.Perpendicular(targetDir.normalized) * speed;
        }
    }

    public void hurt(float damage)
    {
        health -= damage;
        if (health <= 0)
            die();
    }

    void die() { }

    void rotateGun()
    {
        playerPos = target.transform.position;

        aimDir = playerPos - transform.position;
        if (playerPos.x < transform.position.x)
        {
            flip = -1;
        }
        else
            flip = 1;
        float angle = Mathf.Atan2(aimDir.y, flip * aimDir.x) * Mathf.Rad2Deg;
        transform.localScale = new Vector3(flip, 1, 1);
        weapon.eulerAngles = new Vector3(0, 0, flip * angle);
    }

    void shoot()
    {
        weapon.GetChild(0).GetComponent<Weapon>().Fire();
    }

    void stopShoot()
    {
        weapon.GetChild(0).GetComponent<Weapon>().stopFire();
    }

    void damage(float damage)
    {
        Debug.Log("noooo");
    }
}
