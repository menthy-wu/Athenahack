using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : Weapon
{
    bulletParticle bullet;

    // Start is called before the first frame update
    void Start()
    {
        bullet = gameObject.transform.Find("Bullet").gameObject.GetComponent<bulletParticle>();
        Invoke("Fire", 2f);
        Object.Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update() { }

    public override void Fire()
    {
        bullet.startParticle();
    }

    public override void stopFire()
    {
        bullet.stopParticle();
    }
}
