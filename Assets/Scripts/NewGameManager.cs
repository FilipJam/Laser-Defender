using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NewGameManager : MonoBehaviour
{
    [SerializeField] TMP_InputField _inputTMP;

    LevelManager _levelManager;

    bool _saveSuccessful;

    void Awake() {
        _levelManager = FindObjectOfType<LevelManager>();
    }

    public void ClearInput() {
        Debug.Log("Clearing text");
        _inputTMP.text = "";
    }

    public void ParseSave() {
        string filename = _inputTMP.text.Trim().ToLower();
        if(filename == "") { return; }

        FindObjectOfType<SaveManager>().SaveFile(new Save(), filename);
        _saveSuccessful = true;
    }

    public void StartNewGame() {
        if(!_saveSuccessful) { return; }
        _levelManager.LoadGame();
    }
}
