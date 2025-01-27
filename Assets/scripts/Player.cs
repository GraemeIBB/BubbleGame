using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public Image blackOverlay; // Assign your black UI Image in the inspector
    public float fadeSpeed = 0.3f; // Adjust the speed of the fade
    public AudioSource failSound;
    public AudioSource mainSound;

    private double timeSinceLastDeath = 5.0;

    private bool isFading = false;
    Vector3 initialPos;

    void Start(){
        base.Start();
        initialPos = transform.position;
    }

    void Update(){
        base.Update();
        if(Input.GetKeyDown(KeyCode.Space)){
            base.jumpCommand = true;
        }
        
        timeSinceLastDeath += Time.deltaTime;

        if (isFading){
            Color currentColor = blackOverlay.color;
            currentColor.a = Mathf.MoveTowards(currentColor.a, 1f, fadeSpeed * Time.deltaTime);
            blackOverlay.color = currentColor;

            if (currentColor.a >= 1f){
                rb.position = initialPos;
                currentColor.a = 0;
                blackOverlay.color = currentColor;
                rb.AddForce(rb.GetAccumulatedForce() * -1f, ForceMode.Force);
                isFading = false; 
                failSound.Stop();
                mainSound.Play();
                health = 100;
            }
        }

        if(transform.position.y < -30.0f && isFading == false && timeSinceLastDeath > 1.0 / fadeSpeed * 2 || health < 1 && !isFading){
            isFading = true;
            failSound.Play();
            mainSound.Stop();
            timeSinceLastDeath = 0.0;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bubble"))
        {
            if(!other.gameObject.GetComponent<bubble_behaviour>().fromPlayer && health > 1)
                health -= other.gameObject.GetComponent<bubble_behaviour>().damage;
            Destroy(other.gameObject);
            Debug.Log("New Player Health: " + health);
        }
    }

    public float getHealth(){
        return health;
    }

}
