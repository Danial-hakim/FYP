using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    int maxHealth = 100;
    int currentHealth;

    public HealthBar healthbar;

    UIController uiController;

    Minimap minimap;

    bool isActive = false;
    [SerializeField]GameObject magnifer;

    [SerializeField]GameObject gameoverScreen;
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

        minimap = GameObject.Find("MinimapCamera").GetComponent<Minimap>();

        minimap.SetupMiniMap(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyUp(KeyCode.L))
       {
            Damage();
       }

       if(Input.GetKeyUp(KeyCode.P))
       {
            HandleMagnifer();
       }

        if (currentHealth <= 0 && Input.GetKeyUp(KeyCode.M))
        {
            AllowRestartScene();
        }
    }

    void Damage()
    {
        currentHealth -= 10;
        healthbar.SetHealth(currentHealth);

        if(currentHealth < 0)
        {
            gameoverScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void HandleMagnifer()
    {
        isActive = !isActive;
        if (isActive)
        {
            magnifer.SetActive(false);
        }
        else
        {
            magnifer.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && currentHealth > 0)
        {
            Damage();
        }
    }

    void AllowRestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
