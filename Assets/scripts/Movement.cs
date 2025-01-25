using UnityEngine;

public class Movement : MonoBehaviour
{
    private Entity player;
    private Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed;
    void Start()
    {
        cam = Camera.main;
        player = GetComponent<Entity>();
        speed = player.speed;
    }

    // Update is called once per frame
    void Update()
    {


        speed = player.speed;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (Input.GetKeyDown(KeyCode.LeftControl)){
            Crouch();
        }
        if(Input.GetKeyUp(KeyCode.LeftControl)){
            Stand();
        }
        
        
        
        transform.Translate(movement * speed * Time.deltaTime);
    }
    void Crouch(){
        // Debug.Log("Crouch");
        //move camera to crouch position
        // cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - 1, cam.transform.position.z);
        //change speed if starting from zero
        //slide: if starting at speed, maintain speed with slight decrease
    }
    void Stand(){
        // Debug.Log("Stand");

    }

    
}
