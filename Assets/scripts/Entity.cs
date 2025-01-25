using UnityEngine;

public class Entity : MonoBehaviour
{
    public int health = 100;
    public float speed = 5.0f;
    public float jumpStrength = 15.0f;
    public Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 lastPosition;
    private float unitPerSecond;

    public bool canJump = false;
    public bool jumpCommand = false;
    bool isSlowed = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        Debug.Log("Entity Start");
        //rb = GetComponent<Rigidbody>();
        if(rb == null)
        {
            Debug.LogError("Rigidbody not found");
        }
        lastPosition = transform.position;
    }

    // Update is called once per frame
    public void Update()
    {
        jumpCheck();
        pollSpeed();
    }
    void pollSpeed()
    {
        Vector3 currentPosition = transform.position;
        unitPerSecond = Vector3.Distance(currentPosition, lastPosition) / Time.deltaTime;
        lastPosition = currentPosition;
        //Debug.Log("Speed: " + unitPerSecond);
    }
    
    void jumpCheck(){
        if (jumpCommand && canJump)
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            jumpCommand = false;
        }
    }
    public float getCurrentSpeed()
    {
        return unitPerSecond;
    }
    public Vector3 getCurrentPosition()
    {
        return transform.position;
    }
    public Vector3 getCurrentVelocity()
    {
        return rb.linearVelocity;
    }
    public Vector3 getCurrentDirection()
    {
        return transform.forward;
    }

    private void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "platform"){
            canJump = true;
            jumpCommand = false;
            // Debug.Log("can jump");
        }
    }

    private void OnCollisionStay(Collision other){
        if(other.gameObject.tag == "platform"){
            canJump = true;
            // Debug.Log("can jump");
        }
    }


    private void OnCollisionExit(Collision other){
        if(other.gameObject.tag == "platform"){
            canJump = false;
        }
    }
    
}
