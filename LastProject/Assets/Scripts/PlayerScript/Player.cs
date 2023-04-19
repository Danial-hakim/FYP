using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int maxHealth = 100;
    int currentHealth;

    public HealthBar healthbar;

    UIController uiController;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

        uiController = GetComponentInParent<UIController>();
        if (uiController == null)
        {
            uiController = GameObject.Find("IndicatorMaster").GetComponent<UIController>();
        }

        if (uiController == null) Debug.LogError("No UIController component found");

        uiController.SetupIndicatorMaster();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyUp(KeyCode.L))
       {
            Damage();
       }
    }

    void Damage()
    {
        currentHealth -= 10;
        healthbar.SetHealth(currentHealth);
    }
}
