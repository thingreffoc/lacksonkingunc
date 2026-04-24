using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapZoneDisplay : MonoBehaviour
{
    public static MapZoneDisplay Instance { get; private set; }

    [Header("b")]
    public Text displayText;

    [Header("fah")]
    public AudioSource audioSource;
    public AudioClip audioClip;

    [Header("ok daddy")]
    public float fadeInDuration  = 0.3f;
    public float holdDuration    = 2.5f;
    public float fadeOutDuration = 0.6f;

    private readonly HashSet<MapZone> activeZones = new HashSet<MapZone>();
    private Coroutine flashRoutine;

    private void Awake()
    {
        Instance = this;
        SetAlpha(0f);
    }

    public void EnterZone(MapZone zone)
    {
        activeZones.Add(zone);
        StopFlash();
        displayText.text = BuildText(zone.mapName);
        if (audioSource != null)
        {
            if (audioClip != null)
                audioSource.PlayOneShot(audioClip);
            else
                audioSource.Play();
        }
        flashRoutine = StartCoroutine(FlashOnce());
    }

    public void ExitZone(MapZone zone)
    {
        activeZones.Remove(zone);
    }

    private string BuildText(string mapName)
    {
        return "map: " + mapName.ToLower();
    }

    private void StopFlash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
            flashRoutine = null;
        }
        SetAlpha(0f);
    }

    private IEnumerator FlashOnce()
    {
        float elapsed = 0f;
        while (elapsed < fadeInDuration)
        {
            elapsed += Time.deltaTime;
            SetAlpha(elapsed / fadeInDuration);
            yield return null;
        }
        SetAlpha(1f);

        yield return new WaitForSeconds(holdDuration);

        elapsed = 0f;
        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            SetAlpha(1f - elapsed / fadeOutDuration);
            yield return null;
        }
        SetAlpha(0f);

        flashRoutine = null;
    }

    private void SetAlpha(float a)
    {
        Color c = displayText.color;
        c.a = Mathf.Clamp01(a);
        displayText.color = c;
    }
}