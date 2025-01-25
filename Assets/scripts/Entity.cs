using UnityEngine;

public class Entity : MonoBehaviour
{
    public int health = 100;
    public float speed = 5.0f;
    public Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 lastPosition;
    private float unitPerSecond;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        Debug.Log("Entity Start");
        rb = GetComponent<Rigidbody>();
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
        Debug.Log("Speed: " + unitPerSecond);
    }
    
    void jumpCheck(){
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
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
    
}
