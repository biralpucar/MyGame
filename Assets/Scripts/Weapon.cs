using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponShootType
{
    Manual,
    Automatic
}

public class Weapon : MonoBehaviour
{
    public WeaponShootType ShootType { get { return shootType; } }
    [Header("References")]
    [SerializeField] Transform firePoint;
    [SerializeField] Muzzleflash muzzleFlash;
    [SerializeField] LayerMask raycastHittableLayers;

    [Header("Shooting Parameters")]
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] WeaponShootType shootType;
    [SerializeField] float shotDelay;
    [SerializeField] float reloadTime;
    [SerializeField] int currentMagazineCount;
    [SerializeField] int magazineCapacity;
    [SerializeField] bool isReloading;

    [Header("Recoil")]
    [SerializeField] float recoilForce;
    [SerializeField] float maxRecoilDistance;
    [SerializeField] float recoilSpeed;
    [SerializeField] float recoilResetSpeed;

    [Space(10)]

    [SerializeField] float recoilRotationAmount;
    [SerializeField] float recoilRotationSpeed;
    [SerializeField] float recoilRotationResetSpeed;

    [Header("Audio")]
    [SerializeField] AudioClip shootSfx;




    Camera mainCamera;
    Collider ownerCollider;
    public GameObject impactParent { get; set; }
    AudioSource audioSource;
    float lastShotTime = Mathf.NegativeInfinity;
    Vector3 accumulatedRecoil_Position;
    Vector3 recoil_Position;
    Vector3 initialRotation;
    Quaternion accumulatedRecoil_Rotation;
    bool isRecoilRotating = false;



    void Awake()
    {
        currentMagazineCount = magazineCapacity;
        mainCamera = GetComponentInParent<Camera>();
        ownerCollider = GetComponentInParent<Collider>();
        initialRotation = transform.localEulerAngles;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ChangeShootType();
        UpdateWeaponRecoil();
        LoadAmmo();
        if (recoil_Position != Vector3.zero) transform.localPosition = recoil_Position;

    }

    void ChangeShootType()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (shootType == WeaponShootType.Manual)
            {
                shootType = WeaponShootType.Automatic;
            }

            else { shootType = WeaponShootType.Manual; }
        }
    }

    void LoadAmmo()
    {
        if (currentMagazineCount == 0)
        {
            isReloading = true;
        }
        if (isReloading)
        {
            reloadTime -= Time.deltaTime;
            if (reloadTime <= 0f)
            {
                isReloading = false;
                currentMagazineCount = magazineCapacity;
                reloadTime = 2f;
            }
        }
    }

    void UpdateWeaponRecoil()
    {
        if (recoil_Position.z >= accumulatedRecoil_Position.z * 0.99f)
        {
            recoil_Position = Vector3.Lerp(recoil_Position, accumulatedRecoil_Position, recoilSpeed * Time.deltaTime);
        }
        else
        {
            recoil_Position = Vector3.Lerp(recoil_Position, Vector3.zero, recoilResetSpeed * Time.deltaTime);
            accumulatedRecoil_Position = recoil_Position;
        }
        if (isRecoilRotating)
        {

            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, accumulatedRecoil_Rotation, recoilRotationSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.localRotation, accumulatedRecoil_Rotation) <= 0.01f)
            {
                isRecoilRotating = false;
            }
        }
        else
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(initialRotation), recoilRotationResetSpeed * Time.deltaTime);
        }
    }

    public void TryShoot()
    {
        if (currentMagazineCount == 0)
        {
            isReloading = true;
            if (isReloading)
            {
                return;
            }
        }

        if (lastShotTime + shotDelay <= Time.time)
        {
            lastShotTime = Time.time;
            Vector3 targetPosition = ProcessRaycast();
            Vector3 shotDirection = targetPosition - firePoint.position;
            StartCoroutine(muzzleFlash.Activate());
            CreateProjectile(shotDirection);
            audioSource.PlayOneShot(shootSfx);
            ProcessRecoil();
        }
    }

    Vector3 ProcessRaycast()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastHittableLayers))
        {
            //Debug.Log(hit.collider);
            return hit.point;
        }
        else
        {
            return ray.GetPoint(75);
        }
    }

    void CreateProjectile(Vector3 shotDirection)
    {

        Projectile projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.ownerCollider = ownerCollider;
        projectile.impactVFX = impactParent;
        projectile.transform.up = shotDirection;
        projectile.Direction(shotDirection);
        currentMagazineCount--;
    }

    void ProcessRecoil()
    {
        accumulatedRecoil_Position += Vector3.back * recoilForce;
        accumulatedRecoil_Position = Vector3.ClampMagnitude(accumulatedRecoil_Position, maxRecoilDistance);
        accumulatedRecoil_Rotation = Quaternion.Euler((Vector3.right * recoilRotationAmount) + initialRotation);
        isRecoilRotating = true;
    }

    public int GetAmmoCount()
    {
        return currentMagazineCount;
    }

    public bool Reloading()
    {
        return isReloading;
    }

    public float ReloadTiming()
    {
        return reloadTime;
    }
}
