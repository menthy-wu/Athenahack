using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun1 : Weapon
{
    bulletParticle1 bullet;

    // Start is called before the first frame update
    void Awake()
    {
        bullet = gameObject.transform.Find("Bullet").gameObject.GetComponent<bulletParticle1>();
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
