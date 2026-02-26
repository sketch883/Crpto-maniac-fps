using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public List<Weapon> weapons;
    private int currentWeaponIndex = 0;
    private float lastShootTime;
    public float weaponCooldown = 1.0f; // seconds
    private int currentAmmo;
    private int maxAmmo = 30;

    void Start()
    {
        EquipWeapon(currentWeaponIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchWeapon();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void SwitchWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
        EquipWeapon(currentWeaponIndex);
    }

    void EquipWeapon(int index)
    {
        currentAmmo = maxAmmo; // Reload ammo when switched
        Debug.Log("Equipped: " + weapons[index].name);
    }

    void Shoot()
    {
        if (Time.time >= lastShootTime + weaponCooldown && currentAmmo > 0)
        {
            Debug.Log("Shooting with: " + weapons[currentWeaponIndex].name);
            currentAmmo--;
            lastShootTime = Time.time;
            // Add shooting logic here (raycasting, particle effects, etc.)
        }
        else if (currentAmmo <= 0)
        {
            Debug.Log("Out of ammo!");
        }
        else
        {
            Debug.Log("Weapon is cooling down.");
        }
    }
}

[Serializable]
public class Weapon
{
    public string name;
    public float damage;
    public float fireRate;
}