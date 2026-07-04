using StarterAssets;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RepairFlange : MonoBehaviour
{
    [Header("External References")]
    private StarterAssetsInputs playerInput;

    [Header("Config")]
    [SerializeField] private int totalScrews = 8;
    [SerializeField] private SequenceData repairSequenceData;
    [SerializeField] private GameObject screwButtons;
    [SerializeField] private UIVirtualButton[] nutButtons;
    [SerializeField] private NutSlotView[] nutSlots;

    private ISequenceProvider sequence;
    private int screwsPlaced = 0;
    private int nutStep = 0;
    private bool nutPhaseActive = false;

    public event Action OnRepairCompleted;

    private void Awake()
    {
        sequence = repairSequenceData;
        playerInput = FindAnyObjectByType<StarterAssetsInputs>();
    }

    // 1------ SCREWS
    public void OnScrewInserted()
    {
        if (nutPhaseActive) return;

        screwsPlaced++;

        if (screwsPlaced >= totalScrews)
            BeginNutPhase();
    }

    private void BeginNutPhase()
    {
        nutPhaseActive = true;

        foreach (var nutBtn in nutButtons)
            nutBtn.enabled = true;

        foreach (var nutSlot in nutSlots)
            nutSlot.ShowButton();
    }

    // 2------ NUTS
    public void OnNutTightened(int nutId)
    {
        if (!nutPhaseActive) return;

        if (nutId < 0 || nutId >= nutSlots.Length)
        {
            Debug.LogError($"[RepairFlange] nutId out of range: {nutId}");
            return;
        }

        if (nutId == sequence.GetIdAt(nutStep))
        {
            nutSlots[nutId].ShowCorrect();
            nutStep++;

            if (nutStep >= sequence.Count)
                CompleteRepair();
        }
        else
        {
            nutSlots[nutId].FlashError();
        }
    }

    private void CompleteRepair()
    {
        nutPhaseActive = false;
        enabled = false;
        playerInput.EnablePlayer();
        OnRepairCompleted?.Invoke();
    }

    public void EnterRepair()
    {
        screwsPlaced = 0;
        nutStep = 0;
        nutPhaseActive = false;

        screwButtons.SetActive(true);
        playerInput.DisablePlayer();
    }
}