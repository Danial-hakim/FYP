using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIndicatorRegister : MonoBehaviour
{
    [SerializeField] float destroyTimer = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Register", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Register()
    {
        Debug.Log("called");
        if(!DI_System.CheckIfObjectInSight(this.transform))
        {
            Debug.Log("created");
            DI_System.CreateIndicator(this.transform);
        }
        Destroy(this.gameObject, destroyTimer);
    }
}
