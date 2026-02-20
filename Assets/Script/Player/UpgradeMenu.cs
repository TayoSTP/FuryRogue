using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    PlayerStats playerStats;
    TestPlayerMovement playerMovement;
    private AttackSystem _attackSystem;

    public Upgrade upgrade;
    
    [Header ("Upgrade ")]
    public string upgrade1Name;
    public int upgrade1Price;
    public TextMeshProUGUI upgrade1TextObject;
    public Image upgrade1Image;
    public Image upgrade2Image;
    public int upgrade1PriceInflation;
    public TextMeshProUGUI actualHpText;
    public TextMeshProUGUI nextHpText;
    public TextMeshProUGUI actualAttackText;
    public TextMeshProUGUI nextAttackText;
    public TextMeshProUGUI actualAttackSpeedText;
    public TextMeshProUGUI nextAttackSpeedText;
    public Button upgrade1Button;
    
    
    
    
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
        actualHpText.text = playerStats._maxHealth.ToString();
        actualAttackText.text = _attackSystem._attackDamage.ToString();
        actualAttackSpeedText.text = _attackSystem.attackRate.ToString();
        
        nextHpText.text = ((playerStats._maxHealth * 20)/100).ToString();
        nextAttackText.text = ((_attackSystem._attackDamage* 20)/100).ToString();
        nextAttackSpeedText.text = ((_attackSystem.attackRate * 20)/100).ToString();
        //Upgrade 1 Text Setting
        upgrade1TextObject.text = upgrade1Name + " : " +(upgrade1Price.ToString());
        if (playerStats.scraps >= upgrade1Price)
        {
            upgrade1Image.enabled = true;
            upgrade2Image.enabled = false;
            upgrade1TextObject.color = Color.white;
            upgrade1Button.interactable = true;
        }
        else
        {
            
            upgrade1Image.enabled = false;
            upgrade2Image.enabled = true;
            upgrade1TextObject.color = Color.grey;
            upgrade1Button.interactable = false;
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

    public void CloseUpgradeMenu()
    {
        upgrade.cam2.SetActive(false);
        upgrade.UI.SetActive(true);
    }

    public void OpenMenu(GameObject menu)
    {
        AudioManager.Instance.Playsound(AudioType.Button, AudioSourceType.Game);
        print("bitch");
        menu.SetActive(true);
    }

    public void BuyPlugin()
    {
        if (playerStats.scraps >= plugInPrice)
        {
            AudioManager.Instance.Playsound(AudioType.Coin, AudioSourceType.Game);
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
        AudioManager.Instance.Playsound(AudioType.Button, AudioSourceType.Game);
        menu.SetActive(false);
    }
}
