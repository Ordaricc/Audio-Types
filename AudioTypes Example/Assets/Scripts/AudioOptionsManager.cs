using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AudioOptionsManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectsSlider;

    [SerializeField] private TextMeshProUGUI musicVolumeNumberText;
    [SerializeField] private TextMeshProUGUI effectsVolumeNumberText;

    public static float musicVolume { get; private set; } = 100;
    public static float effectsVolume { get; private set; } = 100;

    private const string musicVolumePref = "musicVolumePref";
    private const string soundsVolumePref = "soundsVolumePref";
    private const string firstTimePlayingPref = "firstTimePlayingPref";

    private void Awake()
    {
        LoadSavedData();
    }

    public void LoadSavedData()
    {
        if (PlayerPrefs.GetInt(firstTimePlayingPref) == 0)
        {
            PlayerPrefs.SetInt(firstTimePlayingPref, 1);

            PlayerPrefs.SetFloat(musicVolumePref, musicSlider.value);
            OnMusicSliderValueChange(musicSlider.value);

            PlayerPrefs.SetFloat(soundsVolumePref, effectsSlider.value);
            OnEffectsSliderValueChange(effectsSlider.value);
        }

        musicSlider.value = PlayerPrefs.GetFloat(musicVolumePref);
        effectsSlider.value = PlayerPrefs.GetFloat(soundsVolumePref);

        OnMusicSliderValueChange(musicSlider.value);
        OnEffectsSliderValueChange(effectsSlider.value);
    }
    
    public void OnMusicSliderValueChange(float value)
    {
        musicVolume = value;
        musicVolumeNumberText.text = value.ToString();

        AudioManager.Instance.UpdateVolumeOfPlayingClips();
        PlayerPrefs.SetFloat(musicVolumePref, musicVolume);
    }

    public void OnEffectsSliderValueChange(float value)
    {
        effectsVolume = value;
        effectsVolumeNumberText.text = value.ToString();

        AudioManager.Instance.UpdateVolumeOfPlayingClips();
        PlayerPrefs.SetFloat(soundsVolumePref, effectsVolume);
    }
}