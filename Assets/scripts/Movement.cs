using UnityEngine;

public class Movement : MonoBehaviour
{
    private Entity player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed;
    void Start()
    {
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
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
