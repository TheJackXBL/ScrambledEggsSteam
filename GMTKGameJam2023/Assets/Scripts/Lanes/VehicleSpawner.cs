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

    [Header("Select Indicators")]
    [SerializeField] private Transform selectedCarIndicator;
    [SerializeField] private Vector2 selectedCarIndicatorOffset = new(0, -1);

    [Header("Input")]
    [SerializeField] private int placeMouseBtn = 0;

    [Header("Tags")]
    [SerializeField] private string roadTag = "Road";

    [Header("Spawn Positioning")]
    [SerializeField] private Vector2 spawnOffset = new(0, -5);

    private Camera mainCamera;
    private SoundManager soundManager;
    private GameManager gameManager;
    private CarWallet carWallet;

    private Vector3 mousePos;

    private void Awake()
    {
        mainCamera = Camera.main;
        soundManager = FindObjectOfType<SoundManager>();
        gameManager = FindObjectOfType<GameManager>();
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
        // Check Money
        if (currentActiveCar.carPrice > gameManager.tokens)
            return;

        // Check Car Wallet Budget
        if (carWallet.carCount <= 0)
            return;

        // Raycast toward Click
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        // Return if Clicked Nothing
        if (hit.collider == null)
            return;

        // Return if Did Not Click Road
        if (!hit.collider.gameObject.CompareTag(roadTag))
            return;

        Vector3 spawnPos = hit.collider.transform.position + (Vector3)spawnOffset;

        // Spawn Car at Road at Position
        Instantiate(
            currentActiveCar.gameObject,
            spawnPos,
            Quaternion.identity,
            spawnedVehiclesContainer
        );

        // Reduce Car Wallet Count
        carWallet.carCount--;

        // Reduce Player Money
        gameManager.tokens -= currentActiveCar.carPrice;

        // Play Car Spawn SFX
        soundManager.PlaySound(SoundManager.SoundType.NewCar);
    }

    public void SelectCar(CarButton carBtn)
    {
        currentActiveCar = carBtn.correspondingCar;
    }

    private void UpdateCarIndicator()
    {
        carCursorFollower.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        carCursorFollower.sprite = currentActiveCar.carSprite;
    }
}
