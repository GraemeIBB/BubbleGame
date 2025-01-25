using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class BubbleGunBehavior : MonoBehaviour
{
    enum BubbleType {
        SmallBubble,
        LargeBubble
    }

    public GameObject smallBubble;
    public GameObject largeBubble;
    public Transform barrelTransform;


    // velocity is the speed that the bubble exits the barrel - it is a real number 
    // spread is the variability of the accuracy of the gun - it is a real number between 0 and 1
    [SerializeField] private double smallBubbleVelocity = 100.0;
    [SerializeField] private double smallBubbleSpread = 0.50;
    [SerializeField] private double largeBubbleVelocity = 10.0;
    [SerializeField] private double largeBubbleSpread = 0.10;
    [SerializeField] private bool isFullAuto = true; // o/w semi-auto
    [SerializeField] private double fireDelay = 0.5; // in ms for full-auto
    [SerializeField] private float spreadMultiplier = 10.0f;

    private double sinceLastFire = 0.0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // to ensure user can fire right away
        sinceLastFire = fireDelay + 1;
    }

    // Update is called once per frame
    void Update()
    {
        sinceLastFire += Time.deltaTime;

        if (isFullAuto && Input.GetButton("Fire1") && sinceLastFire >= fireDelay) 
        {
            sinceLastFire = 0.0;
            FireBubble(BubbleType.SmallBubble);
        } 
        else if (!isFullAuto && Input.GetButtonDown("Fire1"))
        {
            FireBubble(BubbleType.SmallBubble);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            FireBubble(BubbleType.LargeBubble);
        }
    }

    private void FireBubble(BubbleType bubbleType) {
        GameObject clone;
        double velocity, spread1, spread2;
        if (bubbleType == BubbleType.SmallBubble) {
            clone = Instantiate(smallBubble);
            velocity = smallBubbleVelocity;
            spread1 = RandomGaussian((float)smallBubbleSpread);
            spread2 = RandomGaussian((float)smallBubbleSpread);
        } else {
            clone = Instantiate(largeBubble);
            velocity = largeBubbleVelocity;
            spread1 = RandomGaussian((float)largeBubbleSpread);
            spread2 = RandomGaussian((float)largeBubbleSpread);
        }

        Rigidbody rb = clone.GetComponent<Rigidbody>();

        //bubbleDirection.x += (float)spread1 * spreadMultiplier;
        //bubbleDirection.y += (float)spread2 * spreadMultiplier;
        clone.transform.position = barrelTransform.position + barrelTransform.forward * 2;
        rb.AddRelativeForce(barrelTransform.forward * (float)velocity, ForceMode.Impulse); // difference between bubble and object
    }

    public static float RandomGaussian(float maxValue = 1.0f)
    {
        float minValue = 0.0f;
        float u, v, S;

        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }
}
