using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject rebindCanvas;
    bool allowRebind;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                handleRebindCanvas();
                Time.timeScale = 0;
            }
            else
            {
                handleRebindCanvas();
                Time.timeScale = 1;
            }
        }
    }

    void handleRebindCanvas()
    {
        allowRebind = !allowRebind;

        if(allowRebind) 
        {
            rebindCanvas.SetActive(true);
        }
        else
        {
            rebindCanvas.SetActive(false);
        }
    }
}
