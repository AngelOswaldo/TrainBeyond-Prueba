using TMPro;
using UnityEngine;

public class TriggerUI : MonoBehaviour
{
    [SerializeField] private GameObject textUI;

    private void Awake()
    {
        Hide();
    }

    public void Show()
    {
        textUI.SetActive(true);
    }

    public void Hide()
    {
        textUI.SetActive(false);
    }
}
