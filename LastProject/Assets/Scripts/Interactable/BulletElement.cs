using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletElement : Interactable
{

    Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void Interact()
    {
        GameObject.FindGameObjectWithTag("Gun").GetComponent<ProjectileGun>().UpdateElement(renderer);
    }
}
