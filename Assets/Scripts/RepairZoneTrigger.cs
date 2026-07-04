using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class RepairZoneTrigger : MonoBehaviour
{
    [SerializeField] private RepairFlange repairFlange;
    [SerializeField] private InputActionReference repairAction;
    [SerializeField] private TriggerUI playerUI;

    private bool playerInRange = false;
    private bool isRepaired = false;

    private void OnEnable()
    {
        repairAction.action.performed += OnRepairInput;
    }

    private void OnDisable()
    {
        repairAction.action.performed -= OnRepairInput;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isRepaired) return;

        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerUI.Show();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerUI.Hide();
        }
    }

    private void OnRepairInput(InputAction.CallbackContext context)
    {
        if (!playerInRange || isRepaired) return;

        repairFlange.EnterRepair();
        playerUI.Hide();
        isRepaired = true; // en vez de SetActive(false)
    }
}