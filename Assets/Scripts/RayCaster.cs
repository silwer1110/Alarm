using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RayCaster : MonoBehaviour
{
    [SerializeField] private float _detectionDistance = 5f;
    [SerializeField] private float _detectionCooldown = 5f;

    private bool _objectDetected = false;

    public event UnityAction<bool> OnRay;

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * _detectionDistance, Color.red);
    }

    private void Start()
    {
        StartCoroutine(Detect());
    }

    private IEnumerator Detect()
    {
        WaitForSeconds wait = new(_detectionCooldown);

        Ray ray = new(transform.position, transform.forward);

        while (enabled)
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo, _detectionDistance))
            {
                if (hitInfo.collider.TryGetComponent(out Julic julic))
                {
                    _objectDetected = !_objectDetected;

                    OnRay?.Invoke(_objectDetected);

                    yield return wait;
                }
            }

            yield return null;
        }
    }
}