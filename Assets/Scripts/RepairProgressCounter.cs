using UnityEngine;
using TMPro;

public class RepairProgressCounter : MonoBehaviour
{
    [SerializeField] private RepairFlange[] flanges;

    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject finalScreen;

    private int totalFlanges;
    private int repairedCount = 0;

    private void Awake()
    {
        totalFlanges = flanges.Length;
        finalScreen.SetActive(false);
        UpdateText();
    }

    private void OnEnable()
    {
        foreach (var flange in flanges)
            flange.OnRepairCompleted += HandleFlangeRepaired;
    }

    private void OnDisable()
    {
        foreach (var flange in flanges)
            flange.OnRepairCompleted -= HandleFlangeRepaired;
    }

    private void HandleFlangeRepaired()
    {
        repairedCount++;
        UpdateText();

        if (repairedCount >= totalFlanges)
            ShowFinalScreen();
    }

    private void UpdateText()
    {
        scoreText.text = $"{repairedCount}/{totalFlanges}";
    }

    private void ShowFinalScreen()
    {
        finalScreen.SetActive(true);
    }
}