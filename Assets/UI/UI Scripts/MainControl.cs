using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainControl : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextVaule = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaltVolume = 1.0f;

    [Header("GamePlay Settings")]
    [SerializeField] private TMP_Text ControllerSenTextValue = null;
    [SerializeField] private Slider ControllerSenSlider = null;
    [SerializeField] private int defaultSen = 4;
    public int MainControllerSen = 4;

    [Header("Toggler Settings")]
    [SerializeField] private Toggle InvertYToggle = null;

    [Header("Graphics Settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brigthnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1;

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropDown;
    [SerializeField] private Toggle FullScreenToggle;

    private int _qualityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;

    [Header ("Comfirmation")]
    [SerializeField] private GameObject comfirmationPrompt = null;

    [Header("Resolution Dropdowns")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    [Header("level to load")]
    public string __EasyLevel;
    public string _Tutorial;
    public string _Mediumlevel;
    public string _HardLevel;
    public string _EndlessLevel;

    public void EasyLevelDialogYes()
    {
        SceneManager.LoadScene(__EasyLevel);
    }
    public void MediumlevelDialogYes()
    {
        SceneManager.LoadScene(_Mediumlevel);
    }
    public void HardLevelDialogYes()
    {
        SceneManager.LoadScene(_HardLevel);
    }
    public void EndlessLevelDialogYes()
    {
        SceneManager.LoadScene(_EndlessLevel);
    }

    public void TutorialDialogYes()
    {
        SceneManager.LoadScene(_Tutorial);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
    
    public void SetVolume(float Volume)
    {
        AudioListener.volume = Volume;
        volumeTextVaule.text = Volume.ToString("0.0");
    }
         

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ComfirmationBox());
    }

    public void SetControllerSen(float sensitivity)
    {
        MainControllerSen = Mathf.RoundToInt(sensitivity);
        ControllerSenTextValue.text = sensitivity.ToString("0");
    }

    public void GameplayApply()
    {
        if (InvertYToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInvertY", 1);
            //invert Y
        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
            //Not Invert Y
        }

        PlayerPrefs.SetFloat("masterInvertY", MainControllerSen);
        StartCoroutine(ComfirmationBox());
    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        brigthnessTextValue.text = brightness.ToString("0");
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }
    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterbrightness", _brightnessLevel);

        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);

        PlayerPrefs.SetInt("masterFullScreen", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;

        StartCoroutine(ComfirmationBox());
    }
    public void RestButton(string Menutype)
    {
        if (Menutype == "Graphics")
        {
            brightnessSlider.value = defaultBrightness;
            brigthnessTextValue.text = defaultBrightness.ToString("0.0");

            qualityDropDown.value = 1;
            QualitySettings.SetQualityLevel(1);

            FullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
            GraphicsApply();
        }

        if (Menutype == "Audio")
        {
            AudioListener.volume = defaltVolume;
            volumeSlider.value = defaltVolume;
            volumeTextVaule.text = defaltVolume.ToString("0.0");
            VolumeApply();
        }
        if (Menutype == "Gameplay")
        {
            ControllerSenTextValue.text = defaultSen.ToString("0");
            ControllerSenSlider.value = defaultSen;
            MainControllerSen = defaultSen;
            InvertYToggle.isOn = false;
            GameplayApply();
        }
    }

    public IEnumerator ComfirmationBox()
    {
        comfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        comfirmationPrompt.SetActive(false);
    }



}
