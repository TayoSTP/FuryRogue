using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    PlayerStats playerStats;
    TestPlayerMovement playerMovement;
    private AttackSystem _attackSystem;

    
    
    [Header ("Upgrade 1")]
    public string upgrade1Name;
    public int upgrade1Price;
    public TextMeshProUGUI upgrade1TextObject;
    public int upgrade1PriceInflation;
    
    [Header ("Upgrade 2")]
    public string upgrade2Name;
    public int upgrade2Price;
    public TextMeshProUGUI upgrade2TextObject;
    public int upgrade2PriceInflation;
    
    [Header ("Upgrade 3")]
    public string upgrade3Name;
    public int upgrade3Price;
    public TextMeshProUGUI upgrade3TextObject;
    public int upgrade3PriceInflation;
    
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
        
        //Upgrade 2 Text Setting
        upgrade2TextObject.text = upgrade2Name + " : " +(upgrade2Price.ToString());
        if (playerStats.scraps >= upgrade2Price)
        {
            upgrade2TextObject.color = Color.green;
        }
        else
        {
            upgrade2TextObject.color = Color.red;
        }
        
        //Upgrade 3 Text Setting
        upgrade3TextObject.text = upgrade3Name + " : " +(upgrade3Price.ToString());
        if (playerStats.scraps >= upgrade3Price)
        {
            upgrade3TextObject.color = Color.green;
        }
        else
        {
            upgrade3TextObject.color = Color.red;
        }
        
    }

    public void  Upgrade1()
    {
        if (playerStats.scraps >= upgrade1Price)
        {
            playerStats.scraps -= upgrade1Price;
            upgrade1Price = upgrade1Price + (upgrade1Price * upgrade1PriceInflation)/100;
            playerMovement._dashForce += 5;
        }
    }
    
    public void  Upgrade2()
    {
        if (playerStats.scraps >= upgrade2Price)
        {
            playerStats.scraps -= upgrade2Price;
            upgrade2Price = upgrade2Price + (upgrade2Price * upgrade2PriceInflation)/100;
            _attackSystem._attackDamage += 5;
        }
    }
    
    public void  Upgrade3()
    {
        if (playerStats.scraps >= upgrade3Price)
        {
            playerStats.scraps -= upgrade3Price;
            upgrade3Price = upgrade3Price + (upgrade3Price * upgrade3PriceInflation)/100;
            _attackSystem.bigAttackChancePourcentage += 10;
        }
    }

    public void CloseMenu(GameObject menu)
    {
        menu.SetActive(false);
    }
}
