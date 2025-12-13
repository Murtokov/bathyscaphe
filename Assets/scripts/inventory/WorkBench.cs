using System;
using TMPro;
using UnityEngine;

public class WorkBench : MonoBehaviour
{
    public GameObject contextHint;
    public bool inTrigger = false, isForUpgrade = false;
    public string[] ItemsName1 = {"repair kit"};
    public string[] ItemsName2 = {"shield plate", "screw"};
    public TMP_Text txt;
    public string init_text;

    public void Start()
    {
        contextHint.SetActive(false);
        txt = contextHint.GetComponent<TMP_Text>();
        init_text = txt.text;
    }
    public void Update()
    {
        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!isForUpgrade)
                {
                    SubmarineConfig submarineConfig = SavesManager.LoadConfig<SubmarineConfig>("SubmarineConfig");
                    InventoryManager Inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
                    if (submarineConfig.health == submarineConfig.maxHealth)
                    {
                        txt.text = "The Submarine's Hull Is Completely Intact"; 
                    }
                    else
                    {   
                        if (Inventory.DeleteItem("repair kit"))
                        {
                            submarineConfig.health = Math.Max(submarineConfig.maxHealth, submarineConfig.health + 60);
                            SavesManager.SaveConfig<SubmarineConfig>(submarineConfig, "SubmarineConfig");
                        }
                        else
                        {
                            txt.text = "Repair Kit required";
                        }
                    }
                }
                if (isForUpgrade)
                {
                    SubmarineConfig submarineConfig = SavesManager.LoadConfig<SubmarineConfig>("SubmarineConfig");
                    InventoryManager Inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
                    foreach (string s in ItemsName2)
                    {
                        Debug.Log(s);
                        if (s == "shield plate")
                        {
                            float tmp = submarineConfig.maxHealth;
                            while (Inventory.DeleteItem(s))
                            {
                                tmp *= 1.1f;
                            }
                            Debug.Log(tmp);
                            submarineConfig.maxHealth = (int)tmp;
                            submarineConfig.health = (int)tmp;
                        }
                        if (s == "screw")
                        {
                            while (Inventory.DeleteItem(s))
                            {
                                submarineConfig.speed *= 1.1f;
                            }
                        }
                    }
                    SavesManager.SaveConfig<SubmarineConfig>(submarineConfig, "SubmarineConfig");
                    SubmarineLife submarineLife = GameObject.FindGameObjectWithTag("Submarine").GetComponent<SubmarineLife>();
                    submarineLife.UpdateHealth();
                    txt.text = "All Upgrades Applied";
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            txt.text = init_text;
            contextHint.SetActive(true);
            inTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            txt.text = init_text;
            inTrigger = false;
            contextHint.SetActive(false);
        }
    }
}
