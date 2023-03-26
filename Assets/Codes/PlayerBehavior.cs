using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    float x,
        y,
        w;

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
    health healthObject;
    GameObject loseUI;
    bool died = false;

    float flip = 1;

    // Start is called before the first frame update
    void Awake()
    {
        loseUI = GameObject.Find("LostUI");
        loseUI.SetActive(false);
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        weapon = gameObject.transform.Find("Weapon");
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        cemera = GameObject.Find("Main Camera").GetComponent<Animator>();
        animator = GetComponent<Animator>();
        healthObject = GameObject.Find("Health").GetComponent<health>();
        w = transform.localScale.x;
    }

    // Update is called once per frame
    void Update() { }

    void FixedUpdate()
    {
        if (died)
            return;
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
        transform.localScale = new Vector3(flip * w, w, w);
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
        healthObject.damage();
        if (health <= 0)
            die();
    }

    void die()
    {
        died = true;
        animator.SetBool("die", true);
        GameObject sparkInstance = Instantiate(spark, transform.position, transform.rotation);
        Object.Destroy(sparkInstance, 2.0f);
        cemera.Play("cemarashake");
        loseUI.SetActive(true);
    }
}
