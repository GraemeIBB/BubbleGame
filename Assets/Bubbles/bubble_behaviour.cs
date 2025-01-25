using UnityEngine;
using System.Collections;

public class bubble_behaviour : MonoBehaviour
{

    [SerializeField]
    float base_bounce;

    [SerializeField]
    public float radius;

    float bounce_power;
    Collider coll;
    Transform trans;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trans = GetComponent<Transform>();
        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        trans.localScale = new Vector3(radius, radius, radius);
        bounce_power = Mathf.Sqrt(radius) * base_bounce; // bigger radius = stronger bounce
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 diff = other.transform.position - coll.transform.position;
        
        other.attachedRigidbody.AddRelativeForce(diff.normalized * bounce_power, ForceMode.Impulse); // difference between bubble and object
        // Debug.Log(diff.normalized * bounce_power);

        Destroy(gameObject);//pop the bubble
    }
}
