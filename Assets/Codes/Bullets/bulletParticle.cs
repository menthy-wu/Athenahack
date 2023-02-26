using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletParticle : MonoBehaviour
{
    public GameObject spark;
    private ParticleSystem particalSystem;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    Animator cemera;

    private void OnParticleCollision(GameObject other)
    {
        int events = particalSystem.GetCollisionEvents(other, collisionEvents);
        if (gameObject.layer == 3 && other.layer == 6)
        {
            other.GetComponent<Enemy>().damage(2f);
        }
        else if (gameObject.layer == 6 && other.layer == 3)
        {
            other.GetComponent<PlayerBehavior>().damage(1);
        }
        for (int i = 0; i < events; i++)
        {
            GameObject sparkInstance = Instantiate(
                spark,
                collisionEvents[i].intersection,
                Quaternion.LookRotation(collisionEvents[i].normal)
            );
            Object.Destroy(sparkInstance, 2.0f);
            cemera.Play("cemarashake");
        }
    }

    private void Start()
    {
        cemera = GameObject.Find("Main Camera").GetComponent<Animator>();
        particalSystem = GetComponent<ParticleSystem>();
        var emission = particalSystem.emission;
        emission.enabled = false;
        var collision = particalSystem.collision;
        if (gameObject.layer == 3)
            collision.collidesWith = LayerMask.GetMask("Enemy", "Ground");
        else if (gameObject.layer == 6)
            collision.collidesWith = LayerMask.GetMask("Player", "Ground");
    }

    public void startParticle()
    {
        var emission = particalSystem.emission;
        emission.enabled = true;
    }

    public void stopParticle()
    {
        var emission = particalSystem.emission;
        emission.enabled = false;
    }
}
