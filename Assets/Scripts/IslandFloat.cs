using UnityEngine;

public class IslandFloat : MonoBehaviour
{
    public float heightChange = 1f;
    public float offset = 0f;
    public Vector3 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(startPosition.x, startPosition.y + Mathf.Sin(Time.time/2 + offset) * heightChange, startPosition.z);
    }
}
