using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [SerializeField] private float destroyTimer = 1f;
    [SerializeField] private float shotPower = 5000f;
    [SerializeField] private float ejectPower = 150f;

    public AudioSource source;
    public AudioClip fireSound;
    public AudioClip reload;
    public AudioClip noAmmo;
    public Magazine magazine;

    public XRBaseInteractor socketInteractor;
    private bool hasSlide = true;

    public void AddMagazine(XRBaseInteractable interactable){
        magazine = interactable.GetComponent<Magazine>();
        source.PlayOneShot(reload);
        //hasSlide = false;
    }
    public void RemoveMagazine(XRBaseInteractable interactable){
        magazine = null;
        source.PlayOneShot(reload);
    }

    public void Slide(){
        source.PlayOneShot(reload);
        if(magazine.numBullets > 0 && hasSlide){
            magazine.numBullets--;
            GameObject tempCasing;
            tempCasing = Instantiate(bulletPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
            //Add force on casing to push it out
            tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
            //Add torque to make casing spin in random direction
            tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

            //Destroy casing after X seconds
            Destroy(tempCasing, destroyTimer);
        }
        else if (magazine.numBullets > 0 && hasSlide == false){
            hasSlide = true; 
        }

    }

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        socketInteractor.onSelectEnter.AddListener(AddMagazine);
        socketInteractor.onSelectExit.AddListener(RemoveMagazine);
    }

    public void PullTheTrigger(){
        if(magazine && magazine.numBullets > 0 && hasSlide){
            gunAnimator.SetTrigger("Fire");
        } else{
            source.PlayOneShot(noAmmo);
            hasSlide = false;
        }
            
    }


    //This function creates the bullet behavior
    void Shoot()
    {
        magazine.numBullets--;
        source.PlayOneShot(fireSound);

        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

}
