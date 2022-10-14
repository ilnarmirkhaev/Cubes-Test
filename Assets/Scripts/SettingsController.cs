using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public CubeSpawner cubeSpawner;
    public Button settingsButton;
    public GameObject settingsPanel;

    public TMP_InputField timeInput;
    public TMP_InputField speedInput;
    public TMP_InputField distanceInput;

    private bool _settingsOpened;

    private void Awake()
    {
        settingsButton.onClick.AddListener(ToggleSettings);

        timeInput.onEndEdit.AddListener(OnTimeInputChanged);
        speedInput.onEndEdit.AddListener(OnSpeedInputChanged);
        distanceInput.onEndEdit.AddListener(OnDistanceInputChanged);

        
        settingsPanel.SetActive(false);
    }

    private void Start()
    {
        timeInput.text = cubeSpawner.TimeBetweenSpawn.ToString();
        speedInput.text = cubeSpawner.CubeSpeed.ToString();
        distanceInput.text = cubeSpawner.CubeDistance.ToString();
    }

    private void ToggleSettings()
    {
        var value = !_settingsOpened;
        settingsPanel.SetActive(value);
        _settingsOpened = value;
    }

    private void OnTimeInputChanged(string input)
    {
        if (IsValidInput(input, out var result))
            cubeSpawner.TimeBetweenSpawn = result;
        else
            timeInput.text = cubeSpawner.TimeBetweenSpawn.ToString();
    }

    private void OnSpeedInputChanged(string input)
    {
        if (IsValidInput(input, out var result))
            cubeSpawner.CubeSpeed = result;
        else
            speedInput.text = cubeSpawner.CubeSpeed.ToString();
    }

    private void OnDistanceInputChanged(string input)
    {
        if (IsValidInput(input, out var result))
            cubeSpawner.CubeDistance = result;
        else
            distanceInput.text = cubeSpawner.CubeDistance.ToString();
    }

    private bool IsValidInput(string input, out int result) =>
        int.TryParse(input, out result) && result > 0;
}