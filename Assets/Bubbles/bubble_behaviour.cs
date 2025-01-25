using UnityEngine;

public class bubble_behaviour : MonoBehaviour
{
    public float damage;

    [SerializeField]
    float bounce_power;

    [SerializeField]
    float radius;

    Collider coll;
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
         //set radius to scale thing 
    }

    private void OnTriggerEnter(Collider other)
    {
        // Vector3 diff = other.transform.position - coll.transform.position;
        
        // other.attachedRigidbody.AddRelativeForce(diff.normalized * bounce_power, ForceMode.Impulse); // difference between bubble and object
        // // Debug.Log(diff.normalized * bounce_power);

        // Destroy(gameObject);//pop the bubble
    }
}
