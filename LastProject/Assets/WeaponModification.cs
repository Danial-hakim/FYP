using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModification : Interactable
{
    [SerializeField] float shootForce;
    [SerializeField] float upwardForce;
    [SerializeField] float spread;
    [SerializeField] int bulletPerTap;
    [SerializeField] bool allowButtonHold;
    [SerializeField] float timeBetweenShot;
    [SerializeField] float timeBetweenShooting;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void Interact()
    {
        ProjectileGun gun = GameObject.FindGameObjectWithTag("Gun").GetComponent<ProjectileGun>();
        gun.UpdateGunModification(shootForce,upwardForce,spread,bulletPerTap,allowButtonHold,timeBetweenShot,timeBetweenShooting);
    }
}
