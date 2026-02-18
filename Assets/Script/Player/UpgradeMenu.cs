using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    PlayerStats playerStats;
    TestPlayerMovement playerMovement;
    private AttackSystem _attackSystem;

    
    
    [Header ("Upgrade ")]
    public string upgrade1Name;
    public int upgrade1Price;
    public TextMeshProUGUI upgrade1TextObject;
    public int upgrade1PriceInflation;
    
    
    
    [Header ("PlugIn")]
    public string plugInName;
    public int plugInPrice;
    public TextMeshProUGUI plugInTextObject;
    public int plugInPriceInflation;
    public Image plugInBackGroundImage;
    public Button plugInButton;
    public Image plugInImage;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<TestPlayerMovement>();
        _attackSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //Upgrade 1 Text Setting
        upgrade1TextObject.text = upgrade1Name + " : " +(upgrade1Price.ToString());
        if (playerStats.scraps >= upgrade1Price)
        {
            upgrade1TextObject.color = Color.green;
            
        }
        else
        {
            
            upgrade1TextObject.color = Color.red;
        }

        if (playerStats.scraps >= plugInPrice)
        {
            plugInButton.GetComponent<Button>().interactable = true;
            plugInBackGroundImage.color = Color.white;
        }
        else
        {
            plugInButton.GetComponent<Button>().interactable = false;
            plugInBackGroundImage.color = Color.grey;
        }
        
        //Upgrade 2 Text Setting
        plugInTextObject.text = plugInName + " : " +(plugInPrice.ToString());
        if (playerStats.scraps >= plugInPrice)
        {
            plugInTextObject.color = Color.green;
        }
        else
        {
            plugInTextObject.color = Color.red;
        }
        
        
    }

    public void  Upgrade1()
    {
        if (playerStats.scraps >= upgrade1Price)
        {
            playerStats.scraps -= upgrade1Price;
            upgrade1Price = upgrade1Price + (upgrade1Price * upgrade1PriceInflation)/100;
            playerMovement._dashForce += 5;
            _attackSystem._attackDamage += ((_attackSystem._attackDamage* 20)/100);
            playerStats._maxHealth += ((playerStats._maxHealth* 20)/100);
        }
    }
    
    

    public void OpenMenu(GameObject menu)
    {
        print("bitch");
        menu.SetActive(true);
    }

    public void BuyPlugin(GameObject plugin,CanvasGroup canvas)
    {
        if (playerStats.scraps >= plugInPrice)
        {
            playerStats.scraps -= plugInPrice;
            _attackSystem.plugIn1 = true;
            //image = plugInImage;
            //, Image image
            //canvas.alpha = 1;
            //, 
        }
    }
    
    public void CloseMenu(GameObject menu)
    {
        menu.SetActive(false);
    }
}
