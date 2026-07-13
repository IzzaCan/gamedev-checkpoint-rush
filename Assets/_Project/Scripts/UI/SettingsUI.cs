using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        if (AudioManager.Instance == null)
        {
            Debug.LogError("AudioManager not found!");
            return;
        }

        musicSlider.SetValueWithoutNotify(AudioManager.Instance.MusicVolume);
        sfxSlider.SetValueWithoutNotify(AudioManager.Instance.SfxVolume);

        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSfxVolumeChanged);
    }

    private void OnDestroy()
    {
        if (musicSlider != null)
            musicSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);

        if (sfxSlider != null)
            sfxSlider.onValueChanged.RemoveListener(OnSfxVolumeChanged);
    }

    private void OnMusicVolumeChanged(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }

    private void OnSfxVolumeChanged(float value)
    {
        AudioManager.Instance.SetSfxVolume(value);
    }
}