using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    public string promtMessage;
    
    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {

    }
}
