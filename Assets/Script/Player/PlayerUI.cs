using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Scrap elements")]
    public TextMeshProUGUI scrapText;
    public Image scrapImage;
    public Texture2D newScrapImage;
    
    [Header("Water elements")]
    public TextMeshProUGUI waterText;
    public Image waterImage;
    public Texture2D newWaterImage;
    
    
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
        
        waterText.text = playerStats.water.ToString();
    }
}
