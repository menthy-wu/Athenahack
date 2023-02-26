using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletParticle : MonoBehaviour
{
    [SerializeField]
    GameObject spark;
    private ParticleSystem particalSystem;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private void OnParticleCollision(GameObject other)
    {
        int events = particalSystem.GetCollisionEvents(other, collisionEvents);
        for (int i = 0; i < events; i++)
        {
            Instantiate(
                spark,
                collisionEvents[i].intersection,
                Quaternion.LookRotation(collisionEvents[i].normal)
            );
        }
    }

    private void Start()
    {
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
