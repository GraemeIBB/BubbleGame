using UnityEngine;
using System.Collections;

public class bubble_behaviour : MonoBehaviour
{
    public float damage;

    [SerializeField]
    float base_bounce;

    [SerializeField]
    public float radius;

    float bounce_power;
    Collider coll;
    Transform trans;

    public bool popable = false;

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
        if(!popable)
            trans.localScale = new Vector3(radius, radius, radius);
        bounce_power = Mathf.Sqrt(radius) * base_bounce; // bigger radius = stronger bounce
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 diff = other.transform.position - coll.transform.position;
        
        if(popable && other.gameObject.tag != "Bubble" && other.gameObject.tag != "platform"){
            other.attachedRigidbody.AddRelativeForce(diff.normalized * bounce_power, ForceMode.Impulse); // difference between bubble and object
            // Debug.Log(diff.normalized * bounce_power);

            Destroy(gameObject);//pop the bubble
        }
        else if(other.gameObject.tag == "platform") {
            Destroy(gameObject);//pop the bubble
        }
        else if(other.gameObject.tag == "Bubble") {
            //connect
            //popable = false;
            trans.SetParent(other.transform, true);
        }
    }
}
