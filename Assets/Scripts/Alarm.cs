using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private RayCaster _rayCaster;
    [SerializeField] private float _increaseSpeed = 0.1f;

    private Coroutine _coroutine;
    private const float MaxVolume = 1f;
    private const float MinVolume = 0f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _rayCaster.OnRay += HandleAlarm;
    }

    private void OnDisable()
    {
        _rayCaster.OnRay -= HandleAlarm;
    }

    private void HandleAlarm(bool objectIn)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (objectIn)
        {
            _audioSource.Play();
            _coroutine = StartCoroutine(FadeVolumeTo(MaxVolume));
        }
        else
        {
            _coroutine = StartCoroutine(FadeVolumeTo(MinVolume));
        }
    }

    private IEnumerator FadeVolumeTo(float targetVolume)
    {
        while (Mathf.Approximately(_audioSource.volume, targetVolume) == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _increaseSpeed * Time.deltaTime);

            yield return null;
        }

        if (Mathf.Approximately(targetVolume, MinVolume))
            _audioSource.Stop();
    }
}