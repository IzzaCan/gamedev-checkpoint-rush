using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class EngineAudio : MonoBehaviour
{
    public static EngineAudio Instance { get; private set; }

    [Header("References")]
    [SerializeField] private AudioSource engineSource;

    [Header("Pitch")]
    [SerializeField] private float minPitch = 0.8f;
    [SerializeField] private float maxPitch = 1.8f;

    [Header("Speed")]
    [SerializeField] private float maxSpeed = 30f;

    [Header("Volume")]
    [SerializeField] private float idleVolume = 0.25f;
    [SerializeField] private float maxVolume = 1f;

    private Rigidbody rb;

    private float sfxVolume = 1f;
    private bool engineStarted = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        rb = GetComponent<Rigidbody>();

        if (engineSource == null)
            engineSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        AudioManager.OnSfxVolumeChanged += UpdateSfxVolume;
    }

    private void OnDisable()
    {
        AudioManager.OnSfxVolumeChanged -= UpdateSfxVolume;
    }

    private void Start()
    {
        if (AudioManager.Instance != null)
            sfxVolume = AudioManager.Instance.SfxVolume;

        engineSource.loop = true;
        engineSource.playOnAwake = false;
        engineSource.Stop();
    }

    private void Update()
    {
        if (!engineStarted)
            return;

        float speed = rb.linearVelocity.magnitude;
        float normalizedSpeed = Mathf.Clamp01(speed / maxSpeed);

        engineSource.pitch = Mathf.Lerp(minPitch, maxPitch, normalizedSpeed);

        float volume = Mathf.Lerp(idleVolume, maxVolume, normalizedSpeed);
        engineSource.volume = volume * sfxVolume;
    }

    private void UpdateSfxVolume(float volume)
    {
        sfxVolume = volume;
    }

    #region Engine Control

    public void StartEngine()
    {
        if (engineStarted)
            return;

        engineStarted = true;

        if (!engineSource.isPlaying)
            engineSource.Play();
    }

    public void StopEngine()
    {
        engineStarted = false;
        engineSource.Stop();
    }

    public void PauseEngine()
    {
        if (!engineStarted)
            return;

        if (engineSource.isPlaying)
            engineSource.Pause();
    }

    public void ResumeEngine()
    {
        if (!engineStarted)
            return;

        engineSource.UnPause();
    }

    #endregion
}