using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    private void Start()
    {
        UIController ui = GetComponentInParent<UIController>();
        if(ui == null)
        {
            ui = GameObject.Find("IndicatorMaster").GetComponent<UIController>();
        }

        if (ui == null) Debug.Log("No UIController component found");

        ui.AddTargetIndicator(this.gameObject);
    }
}
