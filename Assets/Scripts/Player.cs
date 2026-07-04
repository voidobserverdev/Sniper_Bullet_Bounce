using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int maxAmmo;
    [SerializeField] private InputAction fireAction;

    [Header("Aiming")]
    [SerializeField] private float rotationSpeed = 150f;
    [SerializeField] private InputAction aimAction;
    private int currentAmmo;

    public static Action<int> OnAmmoChanged;


    void OnEnable()
    {
        fireAction.Enable();
        aimAction.Enable();
    }

    void OnDisable()
    {
        aimAction.Disable();
        fireAction.Disable();
    }

    //Declaring the pool
    private Queue<GameObject> bulletPool = new Queue<GameObject>();

    void Start()
    {
        currentAmmo = maxAmmo;
        OnAmmoChanged?.Invoke(currentAmmo);
        //Initializing the pool
        for (int i = 0; i < maxAmmo; i++)
        {
            GameObject bulletObject = Instantiate(bulletPrefab);
            bulletObject.SetActive(false);
            bulletPool.Enqueue(bulletObject);
        }
    }

    void Update()
    {
        HandleAim();
        if (GameManager.Instance.CurrentState == GameState.Aiming)
        {
            if (fireAction.WasPressedThisFrame())
            {
                if (currentAmmo > 0)
                {
                    GameObject currentBullet = GetBullet();
                    GameManager.Instance.ChangeState(GameState.BulletInAction);
                    currentAmmo--;
                    OnAmmoChanged?.Invoke(currentAmmo);
                }
            }
        }
    }

    GameObject GetBullet()
    {
        GameObject currentBullet = bulletPool.Dequeue();

        currentBullet.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);

        currentBullet.SetActive(true);
        return currentBullet;
    }

    void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

    void HandleAim()
    {
        float aimInput = aimAction.ReadValue<float>();
        float zRotation = -aimInput * rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.forward, zRotation);
    }
}