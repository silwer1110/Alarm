using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class Crook : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Transform[] _waypoints;

    private void Start()
    {
        StartCoroutine(PatrolRoutine());
    }

    private IEnumerator PatrolRoutine()
    {
        Vector3 target;

        for (int i = 0; i < _waypoints.Length; i++)
        {
            target = _waypoints[i].position;

            yield return _mover.MoveTo(target);
            yield return _mover.Rotate(target);
        }
    }
}