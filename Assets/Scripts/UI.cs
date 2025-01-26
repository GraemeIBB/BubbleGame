using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI Altimeter;
    public TextMeshProUGUI BubbleAmmo;
    public TextMeshProUGUI Health;
    public BubbleGunBehavior bubbleGun;
    public Player player;
    private Vector3 initialPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position;
        Altimeter = transform.Find("Canvas/Altimeter").GetComponent<TextMeshProUGUI>();
        if(Altimeter == null)
        {
            Debug.LogError("Altimeter not found");
        }
        BubbleAmmo = transform.Find("Canvas/BubbleAmmo").GetComponent<TextMeshProUGUI>();
        if(BubbleAmmo == null)
        {
            Debug.LogError("BubbleAmmo not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAltimeter();
        UpdateBubbleAmmo();
        UpdateHealth();
    }
    void UpdateAltimeter()
    {
        float deltaheight = transform.position.y - initialPosition.y;
        deltaheight = Mathf.Round(deltaheight * 10f) / 10f;
        Altimeter.text = "Altitude: " + deltaheight.ToString();
    }
    void UpdateBubbleAmmo()
    {
        int ammo = (int)bubbleGun.getBubbleAmmo();

        BubbleAmmo.text = "Bubble Ammo: " + ammo.ToString();
    }
    void UpdateHealth()
    {
        int health = (int)player.getHealth();

        Health.text = "Health: " + health.ToString();
    }

    
    
}
