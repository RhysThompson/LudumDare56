using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Menu for changing volume Levels. Call VolumeSettings.Instance.Open() to open the menu
/// </summary>
public class VolumeSettings : SingletonMenu<VolumeSettings>
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Image MuteImage;
    [SerializeField] private Sprite unmutedSprite;
    [SerializeField] private Sprite mutedSprite;
    [HideInInspector] public bool initialed = false;
    [HideInInspector] private RectTransform rectTransform;
    // * These values correspond to the exposed values in the audioMixer
    const string SFX_VALUE = "sfxVolume";
    const string MUSIC_VALUE = "musicVolume";
    const string MASTER_VALUE = "masterVolume";
    const string MUTED_VALUE = "muted";
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (PlayerPrefs.HasKey(MUSIC_VALUE))
            LoadSettings();
        else InitializeDefaultVolumeSettings();
    }
    public void SetMusicVolume()
    {
        PlayerPrefs.SetFloat(MUSIC_VALUE, musicSlider.value);
        float volume = Mathf.Log10(musicSlider.value) * 50;
        audioMixer.SetFloat(MUSIC_VALUE, volume);
    }

    public void SetSfxVolume()
    {
        PlayerPrefs.SetFloat(SFX_VALUE, sfxSlider.value);
        float volume = Mathf.Log10(sfxSlider.value) * 50;
        audioMixer.SetFloat(SFX_VALUE, volume);
    }
    public void SetMasterVolume()
    {

        PlayerPrefs.SetFloat(MASTER_VALUE, masterSlider.value);
        float volume = Mathf.Log10(masterSlider.value) * 50;
        audioMixer.SetFloat(MASTER_VALUE, volume);
        if (volume > -80)
        {
            PlayerPrefs.SetInt(MUTED_VALUE, 0);
            MuteImage.sprite = unmutedSprite;
            masterSlider.colors.Equals(masterSlider.colors.normalColor);
        }
    }
    public void MuteButton()
    {
        if (PlayerPrefs.GetInt(MUTED_VALUE) == 0)
        {
            MuteOn();
        }
        else
        {
            MuteOff();
        }
    }
    public void MuteOn()
    {
        PlayerPrefs.SetInt(MUTED_VALUE, 1);
        audioMixer.SetFloat(MASTER_VALUE, -80f);
        MuteImage.sprite = mutedSprite;
        masterSlider.colors.Equals(masterSlider.colors.disabledColor);
    }
    private void MuteOff()
    {
        PlayerPrefs.SetInt(MUTED_VALUE, 0);
        SetMasterVolume();
        MuteImage.sprite = unmutedSprite;
        masterSlider.colors.Equals(masterSlider.colors.normalColor);
    }
    /// <summary>
    /// This function will load playerprefs and apply them
    /// </summary>
    private void LoadSettings()
    {
        musicSlider.value = PlayerPrefs.GetFloat(MUSIC_VALUE);
        sfxSlider.value = PlayerPrefs.GetFloat(SFX_VALUE);
        masterSlider.value = PlayerPrefs.GetFloat(MASTER_VALUE);
        SetMusicVolume();
        SetSfxVolume();
        SetMasterVolume();

        bool muted = PlayerPrefs.GetInt(MUTED_VALUE) == 1;
        if (muted) MuteOn(); else MuteOff();
    }
    private void InitializeDefaultVolumeSettings()
    {
        SetMasterVolume();
        SetMusicVolume();
        SetSfxVolume();
    }
}
