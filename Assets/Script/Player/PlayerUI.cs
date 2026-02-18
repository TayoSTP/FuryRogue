using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Scrap elements")]
    public TextMeshProUGUI scrapText;
    public TextMeshProUGUI arrowsText;
    
    [Header("Images elements")]
    public Image lifebarImage;
    public Image staminaImage;
    public Image waterBarImage;
    
    
    PlayerStats playerStats;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        scrapText.text = playerStats.scraps.ToString();
        arrowsText.text = playerStats._ammo.ToString();
        
        lifebarImage.fillAmount = playerStats._currentHealth / playerStats._maxHealth;
        staminaImage.fillAmount = playerStats.currentStamina / playerStats.maxStamina;
        waterBarImage.fillAmount = playerStats.water / playerStats.maxWater;
    }
}
