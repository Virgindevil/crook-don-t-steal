using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SignalLight))]

public class Signaling : MonoBehaviour
{
    private AudioSource _audioSource;
    private SignalLight _signalLight;
    private Coroutine _soundCoroutine;

    private int _crooksInside = 0;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _signalLight = GetComponent<SignalLight>();

        _audioSource.volume = 0f;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CrookMovement>())
        {
            _crooksInside++;
            if (_crooksInside == 1)
            {
                if (_soundCoroutine == null)
                {
                    _soundCoroutine = StartCoroutine(VolumeUp());
                }
                _audioSource.Play();
                _signalLight.StartAlarm();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<CrookMovement>())
        {
            _crooksInside--;
            if (_crooksInside <= 0)
            {
                if (_soundCoroutine != null)
                {
                    StopCoroutine(_soundCoroutine);
                    _soundCoroutine = null;
                }
                StartCoroutine(VolumeDown());
                _signalLight.StopAlarm();
            }            
        }
    }

    private IEnumerator VolumeUp()
    {
        while (_audioSource.volume < 1.0f)
        {
            _audioSource.volume += Time.fixedDeltaTime;
            yield return null;
        }
    }

    private IEnumerator VolumeDown()
    {
        while (_audioSource.volume > 0f)
        {
            _audioSource.volume -= Time.fixedDeltaTime;
            yield return null;
        }
        if (_audioSource.volume <= 0f)
        {
            _audioSource.Stop();
        }
    }
}
