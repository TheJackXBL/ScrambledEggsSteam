using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI btnText;

    [Header("Display Positioning")]
    [SerializeField] private Vector2 displayPositionOffset = new Vector2(0, 0);

    [Header("Level")]
    [SerializeField] public LevelInfoSO correspondingLevel;

    private LevelInfoDisplay levelInfoDisplay;

    public void SetUI()
    {
        btnText.text = correspondingLevel.levelBtnText;
    }

    public void HandleClick()
    {
        if (levelInfoDisplay == null)
            levelInfoDisplay = FindObjectOfType<LevelInfoDisplay>(true);
        levelInfoDisplay.transform.position = transform.position + (Vector3)displayPositionOffset;
        levelInfoDisplay.SetUI(correspondingLevel);
    }
}
