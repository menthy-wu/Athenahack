using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior1 : MonoBehaviour
{
    static int lose = 0;
    float x,
        y;
    Vector2 r;

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
    public GameObject loseUI;

    float flip = 1;

    // Start is called before the first frame update
    void Awake()
    {
        loseUI = GameObject.Find("LostUI");
        rb = gameObject.GetComponent<Rigidbody2D>();
        weapon = gameObject.transform.Find("Weapon");
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        cemera = GameObject.Find("Main Camera").GetComponent<Animator>();
        animator = GetComponent<Animator>();
        healthObject = gameObject.transform.Find("Health").GetComponent<health>();
        lose++;
    }

    public void OnMove(InputValue value)
    {
        Vector2 m_Move = value.Get<Vector2>();
        x = m_Move.x;
        y = m_Move.y;
    }

    public void OnJump(InputValue value)
    {
        r = value.Get<Vector2>();
        Debug.Log(r);
    }

    public void OnLook(InputValue value)
    {
        if (value.isPressed)
            shoot();
        else
        {
            stopShoot();
        }
    }

    void Start() { }

    // Update is called once per frame
    void Update() { }

    void FixedUpdate()
    {
        move();
    }

    void move()
    {
        // x = Input.GetAxisRaw("Horizontal");
        // y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("move", Mathf.Abs(x) + Mathf.Abs(y));
        rb.velocity = new Vector2(x, y).normalized * speed;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // aimDir = mousePos - transform.position;
        aimDir = r;
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
        if (health <= 0)
            return;
        health--;
        healthObject.damage();
        if (health <= 0)
            die();
    }

    void die()
    {
        loseUI.transform.GetChild(0).gameObject.SetActive(true);
        return;
        animator.SetBool("die", true);
        GameObject sparkInstance = Instantiate(spark, transform.position, transform.rotation);
        Object.Destroy(sparkInstance, 2.0f);
        cemera.Play("cemarashake");
        lose--;
        if (lose <= 0)
            loseUI.transform.GetChild(0).gameObject.SetActive(true);
    }
}
