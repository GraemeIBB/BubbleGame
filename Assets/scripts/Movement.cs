using UnityEngine;

public class Movement : MonoBehaviour
{
    private Entity player;
    private Transform cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed;
    private float speedOrig;
    public float sprintSpeed = 7f;
    public bool isSprinting = false;
    
    public bool isCrouching = false;
    public float crouchHeight = 1.0f;
    public float crouchSpeed = 2f;
    public float crouchSpeedThreshold = 2.0f;
    public bool isSliding = false;
    public float slideStartTime;
    void Start()
    {
        cam = transform.GetChild(0);
        player = GetComponent<Entity>();
        speed = player.speed;
        speedOrig = speed;
    }

    // Update is called once per frame
    void Update()
    {


        // speed = player.speed; this will override crouch speed
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (Input.GetKeyDown(KeyCode.LeftControl)){
            Crouch();
        }
        if(Input.GetKeyUp(KeyCode.LeftControl)){
            Stand();
        }
        // if(isSliding & player.canJump){
        //     //get horizontal speed components and move in that direction
        //     Vector3 slideDirection = new Vector3(player.getCurrentDirection().x, 0, player.getCurrentDirection().z); //im sure theres a better way to do this
        //     transform.Translate((slideDirection.x * player.getCurrentVelocity().x * Time.deltaTime)/Mathf.Max(Time.time - slideStartTime, 1),0,slideDirection.z * player.getCurrentVelocity().z * Time.deltaTime/Mathf.Max(Time.time - slideStartTime, 1));
        // }

        if(isCrouching && Input.GetKeyDown(KeyCode.LeftShift)){
            // Stand();
            // Sprint();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            // Sprint(); Buggy as hell
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            // StopSprint();
        }
        
        transform.Translate(movement * speed * Time.deltaTime);
    }

   

    void Crouch(){
        if(isSprinting) StopSprint();
        Debug.Log("Crouch");
        isCrouching = true;
        //move camera to crouch position
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - crouchHeight, cam.transform.position.z);
        // if(player.getCurrentSpeed() > crouchSpeedThreshold & player.canJump){
        //     isSliding = true;
        //     slideStartTime = Time.time;

        // } else {
        //     isSliding = false;
        // }

        //change speed if starting from zero
        speed = crouchSpeed; //slide: if starting at speed, maintain speed with slight decrease
        
    }
    void Stand(){
        if(isSliding)isSliding = false;
        Debug.Log("Stand");
        isCrouching = false;
        //move camera to standing position
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + crouchHeight, cam.transform.position.z);
        speed = speedOrig; //stand: increase speed

    }
    void Sprint(){
        if(isCrouching) Stand();
        Debug.Log("Sprint");
        speed = sprintSpeed;
        isSprinting = true;
    }
    void StopSprint(){
        Debug.Log("Stop Sprint");
        speed = speedOrig;
        isSprinting = false;
    }


    
}
