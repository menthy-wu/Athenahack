using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    float x,
        y;

    [SerializeField]
    float speed;
    Vector3 mousePos;
    Vector3 aimDir;
    Rigidbody2D rb;
    Camera cam;
    Transform weapon;
    Animator animator;

    [SerializeField]
    int health;

    [SerializeField]
    GameObject spark;
    Animator cemera;

    float flip = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        weapon = gameObject.transform.Find("Weapon");
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() { }

    void FixedUpdate()
    {
        move();
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            shoot();
        else
            stopShoot();
    }

    void move()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("move", Mathf.Abs(x) + Mathf.Abs(y));
        rb.velocity = new Vector2(x, y).normalized * speed;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        aimDir = mousePos - transform.position;
        if (mousePos.x < transform.position.x)
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
        animator.SetBool("attact", true);
    }

    void stopShoot()
    {
        animator.SetBool("attact", false);
        weapon.GetChild(0).GetComponent<Weapon>().stopFire();
    }

    public void damage(float damage)
    {
        health--;
        if (health <= 0)
            die();
    }

    void die()
    {
        animator.SetBool("die", true);
        GameObject sparkInstance = Instantiate(spark, transform.position, transform.rotation);
        Object.Destroy(sparkInstance, 2.0f);
        cemera.Play("cemarashake");
    }
}
