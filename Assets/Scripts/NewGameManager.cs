using UnityEngine;
using TMPro;
using System.Linq;
using UnityEditor;
using System;

public class NewGameManager : MonoBehaviour
{
    [SerializeField] TMP_InputField _inputTMP;

    LevelManager _levelManager;
    SaveManager _saveManager;

    bool _saveSuccessful;

    void Awake() {
        _levelManager = FindObjectOfType<LevelManager>();
        _saveManager = FindObjectOfType<SaveManager>();
    }

    public void Clear() {
        _inputTMP.text = "";
    }

    public void ParseSave() {
        string filename = _inputTMP.text.Trim().ToLower();
        // "File Exists" is a warning message, not a valid filename
        if(filename == "" || filename == StringResource.FileExists) { return; }
        // try creating new save
        _saveSuccessful = CreateNewSave(filename);
    }

    public void StartNewGame() {
        if(!_saveSuccessful) { return; }
        // Only load game if save is successful
        _levelManager.LoadGame();
    }

    bool CreateNewSave(string filename) {
        string[] files = _saveManager.GetFilenames();
        // don't allow duplicate files
        if(files.Contains(filename)) {
            // give a warning to player that a file with that filename already exists
            _inputTMP.text = StringResource.FileExists;
            return false;
        }

        // create new save file if filename is unique
        _saveManager.SaveFile(new Save(), filename);
        return true;
    }
}
