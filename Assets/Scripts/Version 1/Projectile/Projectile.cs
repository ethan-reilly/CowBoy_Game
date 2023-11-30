using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // bullet 
    public GameObject revolverBullet, rifleBullet;

    // bullet speed
    private float shootForce = 40;

    // gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold = false;

    int bulletsLeft, bulletsShot;

    // bools
    bool shooting, readyToShoot, reloading;

    // reference
    public Camera maincamera;
    public Transform attackPointRevolver, attackPointRifle;

    // bug fixing
    public bool allowInvoke = true;

    public int weaponType = 0;

    // graphics
    //public GameObject muzzleFlash;    //@TODO Muzzle Flash
    public TextMeshProUGUI ammoDisplay;

    private void Awake()
    {
        // make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;

        weaponType = GameManager.instance.GetWeaponType();
        SetWeaponProperties();

        Debug.Log("Proj Weapon type = " + weaponType);
    }

    private void SetWeaponProperties()
    {
        
    }

    private void Update()
    {
        MyInput();

        if(ammoDisplay != null)
        {
            ammoDisplay.SetText("AMMO: " + bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
        }
    }


    private void MyInput()
    {
        {
            // check if allowed to hold down button and take corresponding input
            if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
            else shooting = Input.GetKeyDown(KeyCode.Mouse0);

            // Reloading
            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
            {
                Reload();
            }
            // Auto Reloading
            if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
            {
                Reload();
            }

            // Shooting
            if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
            {
                // Set bullets shot to 0
                bulletsShot = 0;

                Shoot();
            }
        }

    }

    private void Shoot()
    {
        readyToShoot = false;

        // Finding centre of 2nd camera
        Ray ray = maincamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        
        // checking if ray hits something
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }

        Vector3 directionWithoutSpread;
        if(weaponType == 0){
            // calculating the direction from attackpoint to targetpoint
            directionWithoutSpread = targetPoint - attackPointRevolver.position;

            // instansiate bullet
            GameObject currentBullet = Instantiate(revolverBullet, attackPointRevolver.position, Quaternion.identity);
            currentBullet.transform.forward = directionWithoutSpread.normalized;

            // adding forces to bullet
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);
            //currentBullet.GetComponent<Rigidbody>().AddForce(maincamera.transform.up * upwardForce, ForceMode.Impulse);
        }
        else
        {
            directionWithoutSpread = targetPoint - attackPointRifle.position;

            GameObject currentBullet = Instantiate(rifleBullet, attackPointRifle.position, Quaternion.identity);
            currentBullet.transform.forward = directionWithoutSpread.normalized;

            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);
        }


        bulletsLeft--;
        bulletsShot++;

        
        if(allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;        
        }


    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }



}



// Debug
/*
 *  // calculating the direction from attackpoint to targetpoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        // instansiate bullet
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithoutSpread.normalized;

        // adding forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);
        //currentBullet.GetComponent<Rigidbody>().AddForce(maincamera.transform.up * upwardForce, ForceMode.Impulse);
*/