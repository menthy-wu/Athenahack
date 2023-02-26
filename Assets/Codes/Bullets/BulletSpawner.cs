using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public bool changeDirection = false;
    public int numCol;
    public float speed;
    public Sprite texture;
    public Color color;
    public float lifetime;
    public float firerate;
    public float size;
    public Material material;
    public float spinSpeed;
    public float timeChangeDirection = 2;
    private float time;
    private float angle;

    bulletParticle bulletParticle;

    [SerializeField]
    GameObject spark;

    ParticleSystem system;

    private void Awake()
    {
        Summon();
    }

    private void FixedUpdate()
    {
        if(changeDirection){
            if(time<timeChangeDirection && time>=0){
            time += Time.fixedDeltaTime;
            transform.rotation = Quaternion.Euler(0,0, time*spinSpeed);
            }
            else if (time >= timeChangeDirection){
                time = 0;
                time -= Time.fixedDeltaTime;
                transform.rotation = Quaternion.Euler(0,0, time*spinSpeed);
            }
            else if(time>-timeChangeDirection){
                time -= Time.fixedDeltaTime;
                transform.rotation = Quaternion.Euler(0,0, time*spinSpeed);
            }
            else{
                time = 0;
            }
        }
        time += Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(0, 0, time * spinSpeed);
    }

    void Summon()
    {
        angle = 360f / numCol;
        for (int i = 0; i < numCol; ++i)
        {
            // A simple particle material with no texture.
            Material particleMaterial = material;

            // Create a green Particle System.
            var go = new GameObject("Particle System");
            go.transform.Rotate(angle * i, 90, 0); // Rotate so the system emits upwards.
            go.transform.parent = this.transform;
            go.transform.position = this.transform.position;
            system = go.AddComponent<ParticleSystem>();
            go.AddComponent(System.Type.GetType("bulletParticle"));
            bulletParticle = go.GetComponent<bulletParticle>();
            bulletParticle.spark = spark;
            go.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
            var mainModule = system.main;
            mainModule.startColor = Color.green;
            mainModule.startSize = 0.5f;
            mainModule.startSpeed = speed;
            mainModule.maxParticles = 100000;
            mainModule.simulationSpace = ParticleSystemSimulationSpace.World;
            var emission = system.emission;
            emission.enabled = false;

            var forma = system.shape;
            forma.enabled = true;
            forma.shapeType = ParticleSystemShapeType.Sprite;
            forma.sprite = null;

            var text = system.textureSheetAnimation;
            text.enabled = true;
            text.mode = ParticleSystemAnimationMode.Sprites;
            text.AddSprite(texture);
            var collision = system.collision;
            collision.enabled = true;
            collision.sendCollisionMessages = true;
            collision.type = ParticleSystemCollisionType.World;
            collision.collidesWith = LayerMask.GetMask("Player", "Ground");
            collision.mode = ParticleSystemCollisionMode.Collision2D;
            collision.bounce = 0;
            collision.lifetimeLoss = 1;
        }

        // Every 2 secs we will emit.
        InvokeRepeating("DoEmit", firerate, firerate);
    }

    void DoEmit()
    {
        foreach (Transform child in transform)
        {
            system = child.GetComponent<ParticleSystem>();
            // Any parameters we assign in emitParams will override the current system's when we call Emit.
            // Here we will override the start color and size.
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.startColor = color;
            emitParams.startSize = size;
            emitParams.startLifetime = lifetime;
            system.Emit(emitParams, 10);
        }
    }
}
