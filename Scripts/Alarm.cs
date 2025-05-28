using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _increaseSpeed = 0.1f;

    private Coroutine _coroutine;
    private float _maxVolume = 1f;
    private float _minVolume = 0f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void DownVolume()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(FadeVolumeTo(_minVolume));
    }

    public void RiseVolum()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

            _audioSource.Play();
            _coroutine = StartCoroutine(FadeVolumeTo(_maxVolume));
    }

    private IEnumerator FadeVolumeTo(float targetVolume)
    {
        while (Mathf.Approximately(_audioSource.volume, targetVolume) == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _increaseSpeed * Time.deltaTime);

            yield return null;
        }

        if (Mathf.Approximately(targetVolume, _minVolume))
            _audioSource.Stop();
    }
}