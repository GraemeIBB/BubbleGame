using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    /*
    more stats here
    */


    private Vector3 lastPosition;
    private float unitPerSecond;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pollSpeed();
    }
    void pollSpeed()
    {
        Vector3 currentPosition = transform.position;
        unitPerSecond = Vector3.Distance(currentPosition, lastPosition) / Time.deltaTime;
        lastPosition = currentPosition;
        Debug.Log("Speed: " + unitPerSecond);
    }
    public float getCurrentSpeed()
    {
        return unitPerSecond;
    }
    public Vector3 getCurrentPosition()
    {
        return transform.position;
    }
    /*
    add methods for upgrading damage, maybe speed, etc
    */
}
