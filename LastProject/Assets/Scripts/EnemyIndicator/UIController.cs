using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Canvas canvas;

    public List<TargetIndicator> targetIndicators = new List<TargetIndicator>();

    public Camera MainCamera;

    public GameObject TargetIndicatorPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MainCamera != null) 
        {
            if (targetIndicators.Count > 0)
            {
                for (int i = 0; i < targetIndicators.Count; i++)
                {
                    targetIndicators[i].UpdateTargetIndicator();
                }
            }
        }
    }

    public void AddTargetIndicator(GameObject target)
    {
        TargetIndicator indicator = GameObject.Instantiate(TargetIndicatorPrefab, canvas.transform).GetComponent<TargetIndicator>();
        indicator.InitialiseTargetIndicator(target, MainCamera, canvas);
        targetIndicators.Add(indicator);
    }

    public void RemoveTargetIndicator(GameObject target)
    {
        TargetIndicator indicatorToRemove = null;

        // Find the target indicator to remove from the list
        foreach (TargetIndicator indicator in targetIndicators)
        {
            if (indicator.gameObject == target)
            {
                indicatorToRemove = indicator;
                break;
            }
        }

        // Remove the target indicator from the list and destroy its game object
        if (indicatorToRemove != null)
        {
            targetIndicators.Remove(indicatorToRemove);
            Destroy(indicatorToRemove.gameObject);
        }
    }

    public void SetupIndicatorMaster()
    {
        MainCamera = Camera.main;

        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();

        if(canvas == null)
        {
            Debug.Log("Cant find canvas");
        }
    }

}
