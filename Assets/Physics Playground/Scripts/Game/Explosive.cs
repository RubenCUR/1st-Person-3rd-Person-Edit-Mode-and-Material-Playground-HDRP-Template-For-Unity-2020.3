using System.Collections;
using UnityEngine;
using Cinemachine;
public class Explosive : MonoBehaviour
{

    [Tooltip("The radius of the explosion.")]
    public float radius = 2;
    [Tooltip("The power of the explosion.")]
    public float power = 500;
    [Tooltip("The collisision magnitude that trigger the explosion.")]
    public float collisionThreshold = 1;
    [Tooltip("The time before the explosion (in seconds).")]
    public float explosionCountDown = 5;
    [Tooltip("The frequency with which this object blinks before the explosion.")]
    [Range(1, 10)] 
    public int blinkFrequency = 1;
    [Tooltip("The material used for the blink effect.")]
    public Material explosiveMaterial;
    [Tooltip("The particles spawned after the explosion.")]
    public GameObject explosionParticle;

    //Used to shake the camera providing a screenshake effect
    CinemachineImpulseSource impulseSource;

    bool alreadyExploded = false;
    Material defaultMaterial;
    Renderer rend;
    Rigidbody rb;

    void Start()
    {
        rend = GetComponent<Renderer>();
        defaultMaterial = rend.material;
        impulseSource = GetComponent<CinemachineImpulseSource>();
        rb = GetComponent<Rigidbody>();
    }

    //When this collides with another object
    void OnCollisionEnter(Collision collision) 
    {
        //Check if it's already exploded AND if the collision was hard enough to exceed the collision threshold
        if(!alreadyExploded && collision.relativeVelocity.magnitude >= collisionThreshold)
        {
            WaitAndExplode();
            alreadyExploded = !alreadyExploded;
        }
    }

    /// <summary>
    /// Apply an explosive force to nearby rigidbodies
    /// </summary>
    void Detonate()
    {
        Vector3 explosionPos = transform.position;

        //Cast a sphere around this to find nearby colliders
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        
        //Check if each one found has a Rigidbody
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //If it does, add explosion force and send it flying
                var realPower = power /Time.timeScale;
                rb.AddExplosionForce(realPower, explosionPos, radius, 1);
            }
        }

        //If the explosion particle is assigned in the Inspector...
        if (explosionParticle != null)
        {
            //Instantiate a particle system
            var particle = Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            //And then destroy it after 3 seconds
            Destroy(particle, 3);
        }

        if(impulseSource != null)
        {
            //If there is a Cinemachine impulse source assigned, generate an impulse to create a screenshake effect
            impulseSource.GenerateImpulse();
        }

        //Destroy this gameObject
        Destroy(gameObject);
    }


    /// <summary>
    /// Toggle the material of the explosive object, if the explosive material is not null
    /// </summary>
    void ToggleMaterial()
    {
        if(explosiveMaterial != null)
        {
            //Switch from the default material to the explosive material
            rend.material = rend.material == defaultMaterial ? explosiveMaterial : defaultMaterial;
        }
    } 

    /// <summary>
    /// Wait "explosionCountDown" seconds before call the Detonate method.
    /// Toggle the material while waiting using "blinkFrequency".
    /// </summary>
    IEnumerator DetonateCoroutine()
    {
        float elapsedTime = 0;
        float frameCount = 0;

        while (elapsedTime <= explosionCountDown)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            frameCount ++;

            var blink = Mathf.PingPong(frameCount * Time.timeScale, 10 / blinkFrequency);

            if(blink == 10 / blinkFrequency)
                ToggleMaterial();

            if(elapsedTime >= explosionCountDown)
                Detonate();
        }
    }

    /// <summary>
    /// Wrapper method for "DetonateCoroutine"
    /// </summary>
    void WaitAndExplode()
    {
        StartCoroutine(DetonateCoroutine());
    }
}
