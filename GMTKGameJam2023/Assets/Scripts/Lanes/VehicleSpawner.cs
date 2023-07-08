using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Car standardCar;
    [SerializeField] public Car currentActiveCar;
    [SerializeField] private Transform spawnedVehiclesContainer;
    [SerializeField] private SpriteRenderer carCursorFollower;

    [Header("Input")]
    [SerializeField] private int placeMouseBtn = 0;

    [Header("Tags")]
    [SerializeField] private string roadTag = "Road";

    [Header("Spawn Positioning")]
    [SerializeField] private Vector2 spawnOffset = new(0, -5);

    private Camera mainCamera;
    private SoundManager soundManager;
    private CarWallet carWallet;

    private Vector3 mousePos;

    private void Awake()
    {
        mainCamera = Camera.main;
        soundManager = FindObjectOfType<SoundManager>();
        carWallet = GetComponent<CarWallet>();
    }

    private void Start()
    {
        currentActiveCar = standardCar;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(placeMouseBtn))
            PlaceSelectedCar();

        UpdateMousePos();
        UpdateCarIndicator();
    }

    private void UpdateMousePos()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void PlaceSelectedCar()
    {
        // Raycast toward Click
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        // Return if Clicked Nothing
        if (hit.collider == null)
            return;

        // Return if Did Not Click Road
        if (!hit.collider.gameObject.CompareTag(roadTag))
            return;

        // Check Car Wallet Budget
        if (carWallet.carCount <= 0)
            return;

        Vector3 spawnPos = hit.collider.transform.position + (Vector3)spawnOffset;

        // Spawn Car at Road at Position
        Instantiate(
            currentActiveCar.gameObject,
            spawnPos,
            Quaternion.identity,
            spawnedVehiclesContainer
        );

        carWallet.carCount--;
        
        soundManager.PlaySound(SoundManager.SoundType.NewCar);
    }

    public void SelectCar(Car car)
    {
        currentActiveCar = car;
    }

    private void UpdateCarIndicator()
    {
        carCursorFollower.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        carCursorFollower.sprite = currentActiveCar.carSprite;
    }
}
