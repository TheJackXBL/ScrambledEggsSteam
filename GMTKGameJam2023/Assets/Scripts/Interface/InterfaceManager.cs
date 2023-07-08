using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfaceManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI killsText;
    [SerializeField] private TextMeshProUGUI carWalletCountText;
    [SerializeField] private TextMeshProUGUI tokensText;
    [SerializeField] private TextMeshProUGUI timeText;

    private GameManager gameManager;
    private CarWallet carWallet;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        carWallet = FindObjectOfType<CarWallet>();
    }

    private void Update()
    {
        pointsText.text = gameManager.playerScore.ToString("0000");
        killsText.text = gameManager.killCount.ToString("000");
        tokensText.text = gameManager.tokens.ToString("000");
        carWalletCountText.text = carWallet.carCount.ToString("00");
        timeText.text = gameManager.time.ToString("0");
;
    }
}
