using UnityEngine;

public class GiveAmmo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This method is called when this collider/rigidbody has begun touching another rigidbody/collider
    void OnCollisionEnter(Collision collision)
    {
        // Retrieve the object this collides with
        GameObject collidedObject = collision.gameObject;

        // Find the BubbleGunBehavior component on the collided object
        BubbleGunBehavior bubbleGunBehavior = collidedObject.transform.Find("Main Camera/BubbleGun").GetComponent<BubbleGunBehavior>();

        if (bubbleGunBehavior != null)
        {
            bubbleGunBehavior.bubbleAmmo = 100;
        
        }
        else
        {
            Debug.Log("BubbleGunBehavior component not found.");
        }
    }
}
