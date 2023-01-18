using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    //https://www.youtube.com/watch?v=BYL6JtUdEY0
    public float delay = 3f;

    public GameObject explosionVFX;
    float countDown;

    public float blastRadius = 5f;
    public float blastForce = 700f;

    bool hasExploded = false;


    // Start is called before the first frame update
    void Start()
    {
        countDown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown<= 0f && !hasExploded)
        {
            Explode();
            hasExploded= true;
        }
        
        void Explode()
        {
            Instantiate(explosionVFX, transform.position, transform.rotation);

            Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

            Physics.OverlapSphere(transform.position, blastRadius);

            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(blastForce, transform.position, blastRadius);
                }
                //damage here to player/enemy
            }


            Destroy(gameObject);
        }
    }
}
