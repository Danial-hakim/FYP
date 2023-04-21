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
        //Working on putting in fuzzy logic n makin sure the elements working , need to do boss battle after player kill certain number of enemies , open a portal , go new scene n fight boss
        GameObject.FindGameObjectWithTag("Gun").GetComponent<ProjectileGun>().UpdateElement(renderer);
    }
}
