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

    void Awake() {
        _saveManager = FindObjectOfType<SaveManager>();
    }
    void Start()
    {
        PopulateDropDown();
        UpdateSaveInfoUI();
        UpdatePlayButton();
    }

    // when load button is pressed
    public void OnLoad() {
        _saveManager.LoadFile(GetSelectedOption().text);
        UpdateSaveInfoUI();
        UpdatePlayButton();
    }

    // when delete button is pressed
    public void OnDelete() {
        _saveManager.DeleteFile(GetSelectedOption().text);
        PopulateDropDown();
        UpdateSaveInfoUI();
        UpdatePlayButton();
    }

    public void UpdateSaveInfoUI() {
        // OnDelete
        if(_saveManager.CurrentFile == null) {
            _fileInfoTMP.text = GetDefaultSaveInfo();
            return;
        }

        // OnLoad
        // gets data from loaded file
        string currentFile = _saveManager.CurrentFile;
        int highscore = _saveManager.CurrentSave.Highscore;
        string longestRun = _saveManager.CurrentSave.LongestRun.ToString();

        // displays file info on UI
        _fileInfoTMP.text = string.Format(_saveInfoTemplate, currentFile, highscore, longestRun);
    }

    TMP_Dropdown.OptionData GetSelectedOption() => _filesDropDown.options[_filesDropDown.value];
    string GetDefaultSaveInfo() => string.Format(_saveInfoTemplate, "N/A", "N/A", "N/A");

    void PopulateDropDown() {
        string[] files = _saveManager.GetFilenames();
        // clear list to prepare for new list
        _filesDropDown.ClearOptions();

        if(files.Length == 0) {
            // default filename as a prompt when no save files exist
            _filesDropDown.AddOptions(new List<string> {StringResource.SelectFile});
        } else {
            _filesDropDown.AddOptions(files.ToList());
        }
    }

    void UpdatePlayButton() {
        // Only allowed to play when a save file is selected
        _playButton.interactable = _saveManager.CurrentFile != null;
    }
}
