using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _increaseSpeed = 0.1f;

    private Coroutine _coroutine;
    private int _crooksCount = 0;
    private float MaxVolume = 1f;
    private float MinVolume = 0f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();       
    }

    private void OnTriggerEnter()
    {
        _crooksCount++;

        HandleAlarm();
    }

    private void OnTriggerExit()
    {
        _crooksCount--;

        HandleAlarm();
    }

    private void HandleAlarm()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (_crooksCount > 0)
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