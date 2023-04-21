using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthbar;

    UIController uiController;

    Minimap minimap;

    bool isActive = false;
    [SerializeField]GameObject magnifer;

    [SerializeField]GameObject gameoverScreen;

    [SerializeField]TextMeshProUGUI resultText;

    bool gameIsOver;
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
        //Testing purpose , delete this 
       if(Input.GetKeyUp(KeyCode.L))
       {
            Damage();
       }

       if(Input.GetKeyUp(KeyCode.P))
       {
            HandleMagnifer();
       }

        if (gameIsOver && Input.GetKeyUp(KeyCode.M))
        {
            AllowRestartScene();
        }
    }

    void Damage()
    {
        currentHealth -= 10;
        healthbar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {
            UpdateResultText("You Lose\nPress M to Restart");
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

        if(other.tag == "Portal")
        {
            UpdateResultText("You Win\nPress M to Restart");
        }
    }

    void AllowRestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    void UpdateResultText(string result)
    {
        gameoverScreen.SetActive(true);
        Time.timeScale = 0;
        // Split the result string into two lines using "\n" as the line break
        string[] lines = result.Split('\n');
        if (lines.Length >= 2)
        {
            resultText.text = lines[0] + "\n" + lines[1];
        }
        else
        {
            resultText.text = result;
        }
        gameIsOver = true;
    }
}
