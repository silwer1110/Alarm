using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class AlarmArea : MonoBehaviour
{
    private int _crooksCount = 0;

    public event UnityAction<int> Detected;

    private void OnTriggerEnter()
    {
        _crooksCount++;

        Detected?.Invoke(_crooksCount);
    }

    private void OnTriggerExit()
    {
        _crooksCount--;

        Detected?.Invoke(_crooksCount);
    }
}
