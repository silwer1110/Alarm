using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _rotateSpeed = 1f;

    public IEnumerator MoveTo(Vector3 target)
    {
        float distanceBetweenPoints = 0.1f;

        while ((transform.position - target).sqrMagnitude > distanceBetweenPoints * distanceBetweenPoints)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);

            yield return null;
        }

        transform.position = target;
    }

    public IEnumerator Rotate(Vector3 target)
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