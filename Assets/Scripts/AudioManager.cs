using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _uiSFXASource;
    [SerializeField] private AudioSource _gameSFXASource;

    [SerializeField] private AudioClip _levelStartClip;
    [SerializeField] private AudioClip _energyClickedClip;
    [SerializeField] private AudioClip _lineConnectedClip;
    [SerializeField] private AudioClip _levelCompletedClip;
    [SerializeField] private AudioClip _buttonClick;
    
    /// <summary>
    /// Audio plays when a level starts
    /// </summary>
    public void LevelStart()
    {
        _gameSFXASource.PlayOneShot(_levelStartClip);
    }

    /// <summary>
    /// Audio plays when an energy is clicked
    /// </summary>
    public void EnergyClicked()
    {
        _gameSFXASource.PlayOneShot(_energyClickedClip);
    }

    /// <summary>
    /// Audio plays when a line is connected
    /// </summary>
    public void LineConnected()
    {
        _gameSFXASource.PlayOneShot(_lineConnectedClip);
    }

    /// <summary>
    /// Audio plays when a level is completed
    /// </summary>
    public void LevelCompleted()
    {
        _gameSFXASource.PlayOneShot(_levelCompletedClip);
    }

    /// <summary>
    /// Plays sfx when UI button is clicked
    /// </summary>
    public void MenuButtonClick()
    {
        _uiSFXASource.PlayOneShot(_buttonClick);
    }
    
}
