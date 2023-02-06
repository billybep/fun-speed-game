using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private TMP_Text _energyText;
    [SerializeField] private Button playButton;
    [SerializeField] private AndroidNotificationHandler androidNotificationHandler;
    [SerializeField] private IOsNotificationHandler iOsNotificationHandler;
    [SerializeField] private int _maxEnergy;
    [SerializeField] private int _energyRechargeDuration;

    private int _energy;

    private const string _EnergyKey = "Energy";
    private const string _EnergyReadyKey = "EnergyReady";
    
    private void Start()
    {
        OnApplicationFocus(true);
    }

    void OnApplicationFocus(bool hasFocus)
    {

        if (!hasFocus) return;

        CancelInvoke();

        int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);

        _highScoreText.text = highScore.ToString();

        _energy = PlayerPrefs.GetInt(_EnergyKey, _maxEnergy);

        if (_energy == 0)
        {
            string energyReadyString = PlayerPrefs.GetString(_EnergyReadyKey,string.Empty);

            if (energyReadyString == string.Empty) return;

            DateTime energyReady = DateTime.Parse(energyReadyString);

            if (DateTime.Now > energyReady)
            {
                _energy = _maxEnergy;
                PlayerPrefs.SetInt(_EnergyKey, _energy);
            }
            else
            {
                playButton.interactable = false;
                Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
            }
        }

        _energyText.text = $"PLAY ({_energy})";
    }

    private void EnergyRecharged()
    {
        playButton.interactable = true;
        _energy = _maxEnergy;
        PlayerPrefs.SetInt(_EnergyKey, _energy);
        _energyText.text = $"PLAY ({_energy})";
    }

    public void Play()
    {
        if (_energy < 1) return;

        _energy--;

        PlayerPrefs.SetInt(_EnergyKey, _energy);

        if (_energy == 0)
        {
            DateTime energyReady = DateTime.Now.AddMinutes(_energyRechargeDuration);

            PlayerPrefs.SetString(_EnergyReadyKey, energyReady.ToString());

            #if UNITY_ANDROID
            androidNotificationHandler.ScheduleNotification(energyReady);
            #elif UNITY_IOS
            iOsNotificationHandler.ScheduleNotification(_energyRechargeDuration);
            #endif
        }

        SceneManager.LoadScene(1);
    }
}
