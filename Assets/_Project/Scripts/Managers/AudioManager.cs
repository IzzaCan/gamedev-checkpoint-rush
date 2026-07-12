using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private const string MusicVolumeKey = "MusicVolume";
    private const string SfxVolumeKey = "SfxVolume";

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Background Music")]
    [SerializeField] private AudioClip backgroundMusic;

    public float MusicVolume => musicSource.volume;
    public float SfxVolume => sfxSource.volume;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadVolume();
        PlayBackgroundMusic();
    }

    #region Music

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic == null)
            return;

        if (musicSource.clip == backgroundMusic && musicSource.isPlaying)
            return;

        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    #endregion

    #region SFX

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null)
            return;

        sfxSource.PlayOneShot(clip);
    }

    #endregion

    #region Volume

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);

        PlayerPrefs.SetFloat(MusicVolumeKey, musicSource.volume);
        PlayerPrefs.Save();
    }

    public void SetSfxVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume);

        PlayerPrefs.SetFloat(SfxVolumeKey, sfxSource.volume);
        PlayerPrefs.Save();
    }

    public float GetMusicVolume()
    {
        return musicSource.volume;
    }

    public float GetSfxVolume()
    {
        return sfxSource.volume;
    }

    private void LoadVolume()
    {
        SetMusicVolume(PlayerPrefs.GetFloat(MusicVolumeKey, 1f));
        SetSfxVolume(PlayerPrefs.GetFloat(SfxVolumeKey, 1f));
    }

    #endregion
}