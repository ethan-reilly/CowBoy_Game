using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    // bullet 
    public GameObject revolverBullet, rifleBullet;

    // bullet speed
    private float shootForce = 40;

    // gun stats
    private float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    private int magazineSize, bulletsPerTap;
    private bool allowButtonHold = false;

    int bulletsLeft, bulletsShot;

    // bools
    bool shooting, readyToShoot, reloading;

    // reference
    public Camera maincamera;
    public Transform attackPointRevolver, attackPointRifle;

    // UI
    private Canvas reloadRadialCanvas;
    private Image reloadRadialImage;
    private float reloadTimeLeft;

    // bug fixing
    public bool allowInvoke = true;

    public int weaponType = 0;

    // graphics
    //public GameObject muzzleFlash;    //@TODO Muzzle Flash
    private TextMeshProUGUI ammoDisplay;


    private void Awake()
    {
        // make sure magazine is full
        readyToShoot = true;

        reloadTimeLeft = reloadTime;

        weaponType = GameManager.Instance.GetWeaponType();
        SetWeaponProperties();
        bulletsLeft = magazineSize;

        GameObject tempObj = GameObject.Find("PlayerReload - Canvas");

        if (tempObj != null)
        {
            reloadRadialCanvas = tempObj.GetComponent<Canvas>();
            reloadRadialCanvas.gameObject.SetActive(false);

            if (reloadRadialCanvas != null)
            {
                reloadRadialImage = reloadRadialCanvas.GetComponentInChildren<Image>();
               
                /// Debug
                //  Debug.Log("Image found");
            }
            else
            {
                Debug.Log("Image not found");
            }
        }
        else
        {
            Debug.Log("Canvas not found");
        }


    }

    private void SetWeaponProperties()
    {
        if(weaponType == 0)
        {
            timeBetweenShooting = 0.3f;
            spread = 0;
            reloadTime = 1.1f;
            timeBetweenShots = 0;
            magazineSize = 6;
            bulletsPerTap = 1;
        }
        else
        {
            //@DEBUG 
            timeBetweenShooting = 0.95f;
            spread = 0;
            reloadTime = 2f;
            timeBetweenShots = 0;
            magazineSize = 3;
            bulletsPerTap = 1;
        }
        
    }

    private void Update()
    {
        MyInput();

        if (ammoDisplay != null)
        {
            ammoDisplay.SetText("AMMO: " + bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
        }
        else
        {
            ammoDisplay = UI.Instance.getAmmoDisplay();
        }

        if (reloading)
        {
            reloadRadialCanvas.gameObject.SetActive(true);
            // Reload Radial decreasing
            reloadTimeLeft -= Time.deltaTime;
            reloadRadialImage.fillAmount = reloadTimeLeft / reloadTime;
        }
        else
        {
           reloadRadialCanvas.gameObject.SetActive(false);
            reloadTimeLeft = reloadTime;
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
            if (readyToShoot && !reloading && bulletsLeft <= 0) 
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