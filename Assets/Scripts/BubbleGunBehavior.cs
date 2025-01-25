using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class BubbleGunBehavior : MonoBehaviour
{
    enum BubbleType // the two types of bubbles, corresponding to L/R click
    {
        Attack,
        Movement,
    }
    public GameObject Bubble;
    private GameObject currentBubble;
    private BubbleType currentBubbleType;
    private bubble_behaviour currentBubbleScript;
    public Transform barrelTransform;
    public Transform barrelTip;

    // spread is the variability of the accuracy of the gun - it is a real number between 0 and 1
    [SerializeField] private float bubbleVelocity = 0.03f;
    [SerializeField] private float inflationRate = 0.10f;
    [SerializeField] private float smallBubbleDamage = 25.0f;
    [SerializeField] private float accuracySpread = 0.25f;
    [SerializeField] private float startingRadius = 0.50f;
    [SerializeField] private bool isFullAuto = true; // o/w semi-auto
    [SerializeField] private float fireDelay = 0.5f; // in ms for full-auto
    [SerializeField] private float spreadMultiplier = 10.0f;

    private float sinceLastFire = 0.0f;
    private float inflationStart = 0.0f;
    private bool inflating = false; 

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
            currentBubbleType = BubbleType.Attack;
            sinceLastFire = 0.0f;
            startBubbleInflation();
            FireBubble();
        } 
        else if (!isFullAuto && Input.GetButtonDown("Fire1"))
        {
            currentBubbleType = BubbleType.Attack;
            startBubbleInflation();
            FireBubble();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            currentBubbleType = BubbleType.Movement;
            if(!inflating)
                startBubbleInflation();
        }
        else if (Input.GetButtonUp("Fire2")){
            currentBubbleType = BubbleType.Movement;
            if(inflating){
                FireBubble();
            }
        }
        else if (Input.GetButton("Fire2")){
            currentBubbleType = BubbleType.Movement;
            if(inflating){
                currentBubbleScript.radius += inflationRate;
                currentBubble.transform.position = barrelTransform.position + barrelTransform.forward * currentBubbleScript.radius/2; 
            }
        }
    }

    private void startBubbleInflation(){
        inflating = true;
        GameObject clone = Instantiate(Bubble);
        currentBubbleScript = clone.GetComponent<bubble_behaviour>();
        currentBubbleScript.radius = startingRadius;
        currentBubble = clone;
        currentBubble.transform.position = barrelTransform.position + barrelTransform.forward * currentBubbleScript.radius; 

    }

    private void FireBubble() {
        inflating = false;

        if (currentBubbleType == BubbleType.Attack)
        {
            bubble_behaviour curBehaviour = currentBubble.GetComponent<bubble_behaviour>();
            curBehaviour.damage = smallBubbleDamage; 
        }
        
        Rigidbody rb = currentBubble.GetComponent<Rigidbody>();
        Vector3 spread = new Vector3(
            accuracySpread - RandomGaussian(accuracySpread),
            accuracySpread - RandomGaussian(accuracySpread)
        ) * spreadMultiplier;

        rb.AddForce((barrelTransform.forward + spread).normalized * bubbleVelocity, ForceMode.Impulse); // difference between bubble and object
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
