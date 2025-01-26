using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public Image blackOverlay; // Assign your black UI Image in the inspector
    public float fadeSpeed = 1f; // Adjust the speed of the fade
    public AudioSource failSound;
    public AudioSource mainSound;

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
        if(transform.position.y < -30.0f && isFading == false){
            isFading = true;
            failSound.Play();
            mainSound.Stop();
        }

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
                mainSound.Play();
            }
        }
    }
}
