using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class MenuInfoManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _fileInfoTMP;
    [SerializeField] TMP_Dropdown _filesDropDown;

    [SerializeField] Button _playButton;

    SaveManager _saveManager;

    readonly string _saveInfoTemplate = "File: \t\t\t{0}\nHighscore:\t\t{1}\nLongest Run:\t{2}";

    public Button PlayButton => _playButton;

    void Awake() {
        _saveManager = FindObjectOfType<SaveManager>();
    }

    void Start()
    {
        PopulateDropDown();
        UpdateSaveInfoUI();
        UpdatePlayButton();
    }

    public void OnLoad() {
        _saveManager.LoadFile(GetSelectedOption().text);
        UpdateSaveInfoUI();
        UpdatePlayButton();
    }

    public void OnDelete() {
        _saveManager.DeleteFile(GetSelectedOption().text);
        PopulateDropDown();
        UpdateSaveInfoUI();
        UpdatePlayButton();
    }

    public void SetOptionToCurrent() {
        string filename = _saveManager.CurrentFile;
        int value = _filesDropDown.options.Select(option => option.text).ToList().IndexOf(filename);

        _filesDropDown.value = (filename != null && value >= 0) ? value : 0; 
    }

    public void UpdateSaveInfoUI() {
        if(_saveManager.CurrentFile == null) {
            _fileInfoTMP.text = GetDefaultSaveInfo();
            return;
        }

        string currentFile = _saveManager.CurrentFile;
        int highscore = _saveManager.CurrentSave.Highscore;
        string longestRun = FormattedLongestTime(_saveManager.CurrentSave.LongestRun);

        _fileInfoTMP.text = string.Format(_saveInfoTemplate, currentFile, highscore, longestRun);
    }

    public TMP_Dropdown.OptionData GetSelectedOption() => _filesDropDown.options[_filesDropDown.value];

    string GetDefaultSaveInfo() => string.Format(_saveInfoTemplate, "N/A", "N/A", "N/A");
    string FormattedLongestTime(int[] time) => string.Format("{2}:{1}.{0}", time[0], FormatTimeValue(time[1]), FormatTimeValue(time[2]));
    string FormatTimeValue(int num) => (num < 10 ? "0" : "") + num;

    void PopulateDropDown() {
        string[] files = _saveManager.GetFilenames();
        _filesDropDown.ClearOptions();

        if(files.Length == 0) {
            _filesDropDown.AddOptions(new List<string> {"Select File"});
        } else {
            _filesDropDown.AddOptions(files.ToList());
        }
    }

    void UpdatePlayButton() {
        _playButton.interactable = _saveManager.CurrentFile != null;
    }
    
}
