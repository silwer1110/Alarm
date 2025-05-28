using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _rotateSpeed = 1f;

    public void StartMoving()
    {
        StartCoroutine(PatrolRoutine());
    }

    private IEnumerator PatrolRoutine()
    {
        Vector3 target;

        for (int i = 0; i < _waypoints.Length; i++)
        {
            target = _waypoints[i].position;

            yield return MoveTo(target);
            yield return Rotate(target);
        }
    }

    private IEnumerator MoveTo(Vector3 target)
    {
        float distanceBetweenPoints = 0.1f;

        while ((transform.position - target).sqrMagnitude > distanceBetweenPoints * distanceBetweenPoints)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);

            yield return null;
        }

        transform.position = target;
    }

    private IEnumerator Rotate(Vector3 target)
    {
        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);

        while (targetRotation != transform.rotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);

            yield return null ;
        }

        transform.rotation = targetRotation; 
    }
}