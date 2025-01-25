using UnityEngine;

public class BubbleGunBehavior : MonoBehaviour
{
    public GameObject smallBubble;
    public GameObject largeBubble;

    [SerializeField] private double smallBubbleVelocity;
    [SerializeField] private double largeBubbleVelocity;
    private Transform barrelLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        barrelLocation = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Debug.Log("Fire 1");
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Fire 2");
        }
    }
}
