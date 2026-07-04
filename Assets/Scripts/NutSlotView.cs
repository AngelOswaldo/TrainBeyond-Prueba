using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NutSlotView : MonoBehaviour
{
    [SerializeField] private GameObject visual;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Image buttonImage;

    [SerializeField] private Color correctColor = Color.green;
    [SerializeField] private Color errorColor = Color.red;
    [SerializeField] private float errorFeedbackDuration = 1f;

    public void ShowButton()
    {
        canvas.SetActive(true);
    }

    public void ShowCorrect()
    {
        visual.SetActive(true);
        canvas.SetActive(false);
    }

    public void FlashError()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        buttonImage.color = errorColor;
        yield return new WaitForSeconds(errorFeedbackDuration);
        buttonImage.color = correctColor;
    }

    public void ResetVisual()
    {
        visual.SetActive(false);
        canvas.SetActive(true);
        buttonImage.color = correctColor;
    }
}