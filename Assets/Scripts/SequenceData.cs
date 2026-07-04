using System.Collections.Generic;
using UnityEngine;

public interface ISequenceProvider
{
    int Count { get; }
    int GetIdAt(int step);
    bool IsValidStep(int step);
}

[CreateAssetMenu(fileName = "SequenceData", menuName = "Scriptable Objects/RepairSequenceData")]
public class SequenceData : ScriptableObject, ISequenceProvider
{
    [SerializeField] private List<int> correctOrder = new List<int>();

    public int Count => correctOrder.Count;

    public int GetIdAt(int step)
    {
        if (!IsValidStep(step))
        {
            Debug.LogError($"[SequenceData] Índice fuera de rango: {step}");
            return -1;
        }
        return correctOrder[step];
    }

    public bool IsValidStep(int step) => step >= 0 && step < correctOrder.Count;
}