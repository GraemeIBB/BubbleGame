using UnityEngine;
using System.Collections;

public class bubble_behaviour : MonoBehaviour
{
    public float damage;

    [SerializeField]
    float base_bounce;

    [SerializeField]
    public float radius;

    [SerializeField]
    public double expiryTime = 15.0;

    float bounce_power;
    Collider coll;
    Transform trans;

    public bool fromPlayer = false;

    public bool popable = false;

    private double timeSinceCreation = 0.0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trans = GetComponent<Transform>();
        coll = GetComponent<Collider>();
        trans.localScale = new Vector3(radius, radius, radius);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceCreation += Time.deltaTime;
        if(timeSinceCreation > expiryTime)
            Destroy(gameObject);
        if(!popable)
            trans.localScale = new Vector3(radius, radius, radius);
        bounce_power = radius * base_bounce; // bigger radius = stronger bounce
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     Vector3 diff = other.transform.position - coll.transform.position;
    //     if(popable && radius > 0.8){
    //         if(other.gameObject.tag == "platform") {
    //             Destroy(gameObject);//pop the bubble
    //         }
    //         else if(other.gameObject.tag == "Bubble") {
    //             trans.SetParent(other.transform, true);
    //         }
    //         else{
    //             other.attachedRigidbody.AddRelativeForce(diff.normalized * bounce_power, ForceMode.Impulse); // difference between bubble and object
    //             //Debug.Log(radius);
    //             Destroy(gameObject);//pop the bubble
    //         }
    //     }
    // }
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 diff = collision.transform.position - coll.transform.position;
        if(popable){
            if (collision.gameObject.tag == "platform")
            {
                Destroy(gameObject); // pop the bubble
            }
        }
        if (popable && radius > 0.8f)
        {
            if (collision.gameObject.tag == "platform")
            {
                Destroy(gameObject); // pop the bubble
            }
            else if (collision.gameObject.tag == "Bubble")
            {
                trans.SetParent(collision.transform, true);
            }
            else if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
            {
                ContactPoint contact = collision.contacts[0];
                Vector3 contactNormal = contact.normal;

                // Apply force based on the contact normal
                Rigidbody otherRigidbody = collision.rigidbody;
                if (otherRigidbody != null)
                {
                    // Vector3 reflectedVelocity = Vector3.Reflect(otherRigidbody.linearVelocity, contactNormal).normalized; // shoots off in direction dependent on contact with bubble. good for enemies
                    Vector3 reflectedVelocity = Vector3.Reflect(otherRigidbody.linearVelocity, new Vector3(0,1,0)).normalized; //works well for player
                    reflectedVelocity.y = 0.5f;

                    otherRigidbody.linearVelocity = reflectedVelocity * bounce_power;
                }

                Destroy(gameObject); // pop the bubble
            }
            else
            {
                ContactPoint contact = collision.contacts[0];
                Vector3 contactNormal = contact.normal;

                // Apply force based on the contact normal
                Rigidbody otherRigidbody = collision.rigidbody;
                if (otherRigidbody != null)
                {
                    Vector3 reflectedVelocity = Vector3.Reflect(otherRigidbody.linearVelocity, contactNormal).normalized; // shoots off in direction dependent on contact with bubble. good for enemies
                    // Vector3 reflectedVelocity = Vector3.Reflect(otherRigidbody.linearVelocity, new Vector3(0,1,0)).normalized; //works well for player
                    reflectedVelocity.y = 0.5f;

                    otherRigidbody.linearVelocity = reflectedVelocity * bounce_power;
                }

                Destroy(gameObject); // pop the bubble
            }
        }
    }
}
