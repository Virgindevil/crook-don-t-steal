using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class SignalLight : MonoBehaviour
{   
    [SerializeField] private Color _alarmColor = Color.red;

    private SpriteRenderer[] bulbs;
    private Color _baseColor = Color.white;
    private Coroutine _blinkCoroutine;

    private void Start()
    {
        bulbs = GetComponentsInChildren<SpriteRenderer>()
            .Where(sr => sr.gameObject != gameObject)
            .ToArray();
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            foreach (var bulb in bulbs)
            {
                ColorChange(bulb, _alarmColor);
            }
            yield return new WaitForSeconds(1);

            foreach (var bulb in bulbs)
            {
                ColorChange(bulb, _baseColor);
            }
            yield return new WaitForSeconds(1);
        }
    }

    public void StartAlarm()
    {
        if (_blinkCoroutine == null)
        {
            _blinkCoroutine = StartCoroutine(Blink());
        }
    }

    public void StopAlarm()
    {
        if (_blinkCoroutine != null)
        {
            StopCoroutine(_blinkCoroutine); 
            _blinkCoroutine = null;
        }

        foreach (var bulb in bulbs)
        {
            ColorChange(bulb, _baseColor);
        }
    }

    private void ColorChange(SpriteRenderer spriteRenderer, Color color)
    {
        spriteRenderer.color = color;
    }
}
