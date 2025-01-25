using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class BubbleGunBehavior : MonoBehaviour
{
    public GameObject Bubble;
    private GameObject currentBubble;
    private bubble_behaviour currentBubbleScript;
    public Transform barrelTransform;

    // spread is the variability of the accuracy of the gun - it is a real number between 0 and 1
    [SerializeField] private float bubbleVelocity = 0.03f;
    [SerializeField] private float inflationRate = 0.10f;
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
            sinceLastFire = 0.0f;
            startBubbleInflation();
            FireBubble();
        } 
        else if (!isFullAuto && Input.GetButtonDown("Fire1"))
        {
            startBubbleInflation();
            FireBubble();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            if(!inflating)
                startBubbleInflation();
        }
        else if (Input.GetButtonUp("Fire2")){
            if(inflating){
                FireBubble();
            }
        }
        else if (Input.GetButton("Fire2")){
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
        
        currentBubbleScript.popable = true;
        Rigidbody rb = currentBubble.GetComponent<Rigidbody>();

        rb.AddRelativeForce(barrelTransform.forward * bubbleVelocity, ForceMode.Impulse); // difference between bubble and object
    }
}
