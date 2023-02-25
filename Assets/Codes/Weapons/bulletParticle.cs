using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletParticle : MonoBehaviour
{
    private ParticleSystem particalSystem;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private void OnParticleCollision(GameObject other)
    {
        int events = particalSystem.GetCollisionEvents(other, collisionEvents);
        for (int i = 0; i < events; i++) { }
    }

    private void Start()
    {
        particalSystem = GetComponent<ParticleSystem>();
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
