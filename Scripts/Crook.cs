using UnityEngine;

[RequireComponent(typeof(Transform), typeof(Mover))]
public class Crook : MonoBehaviour
{
    [SerializeField] private Mover _mover;

    private void Start()
    {
        _mover.StartMoving();
    }
}