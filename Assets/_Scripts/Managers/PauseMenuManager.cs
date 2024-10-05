using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
/// <summary>
/// provides implementation for pausing. When testing this class make sure to disable the canvas instead of the gameObject
/// </summary>
public class PauseMenuManager : SingletonMenu<PauseMenuManager>
{
    // Start is called before the first frame update
    [SerializeField] private AudioMixerSnapshot paused;
    [SerializeField] private AudioMixerSnapshot unpaused;
    public static event Action OnPause;
    public static event Action OnUnpause;
    public static bool isPaused { get; private set; } = false;
    public void Resume()
    {
        base.Close();
        Time.timeScale = 1f;
        unpaused.TransitionTo(.1f);
        isPaused = false;
        OnUnpause?.Invoke();
    }
    public void Pause()
    {
        base.Open();
        PauseWithoutMenu();
    }
    public void PauseWithoutMenu()
    {
        Time.timeScale = 0f;
        paused.TransitionTo(.1f);
        isPaused = true;
        OnPause?.Invoke();
    }

    private new void Awake()
    {
        base.Awake();
        GameManager.OnBeforeStateChanged += AutoUnPause;
    }
    private void OnDestroy()
    {
        GameManager.OnBeforeStateChanged -= AutoUnPause;
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        OnEscape();
    }
    public void OnEscape()
    {
        if (VolumeSettings.Instance.isOpen)
        {
            VolumeSettings.Instance.Close();
            return;
        }
        Toggle();
    }
    public new void Toggle()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }
    public void AutoUnPause(GameState newState)
    {
        if (isPaused)
            Resume();
    }
    public void Restart()
    {
        AudioSystem.Instance.PlaySFX("button");
        SceneSwitcher.Instance.RestartScene();
        Resume();
    }
    public void Options()
    {
        AudioSystem.Instance.PlaySFX("button");
        VolumeSettings.Instance.Open();
    }
    public void Main_Menu()
    {
        AudioSystem.Instance.PlaySFX("button");
        Resume();
        SceneSwitcher.Instance.ReturnToMenu();
    }
    public override void Open()
    {
        Resume();
    }
    public override void Close()
    {
        Pause();
    }
}
