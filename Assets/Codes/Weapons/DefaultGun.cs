using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun : Weapon
{
    bulletParticle bullet;

    // Start is called before the first frame update
    void Start()
    {
        bullet = gameObject.transform.Find("Bullet").gameObject.GetComponent<bulletParticle>();
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
