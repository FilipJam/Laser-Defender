using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System;

public class SaveManager : MonoBehaviour
{
    Save _currentSave;
    string _savePath;
    string _currentFilename;
    
    public Save CurrentSave => _currentSave;

    public string CurrentFile => _currentFilename;

    void Awake() {
        ManageSingleton();
        _savePath = Application.persistentDataPath + "/save/";
    }

    void Start() {
        if(PlayerPrefs.HasKey(StringResource.CurrentFile)) {
            LoadFile(PlayerPrefs.GetString(StringResource.CurrentFile));
        }
    }


    public string[] GetFilenames() {
        string[] filePaths = Directory.GetFiles(_savePath);
        string[] result = new string[filePaths.Length];
        for(int i = 0; i < filePaths.Length; i++) {
            string filename = filePaths[i].Split('/').Last();
            result[i] = filename[..filename.IndexOf('.')];
        }
        return result;
    }

    
    public void DeleteFile(string filename) {
        string path = GetFilePath(filename);
        File.Delete(path);

        ResetCurrentFile();
    }

    public void LoadFile(string filename) {
        FileStream file = null;
        try {
            file = File.OpenRead(GetFilePath(filename));
            var save = (Save)new BinaryFormatter().Deserialize(file);
            SetCurrentFile(filename, save);
        } catch(FileNotFoundException ex) {
            Debug.Log(ex.Message);
        } finally {
            file?.Close();
        }
    }

    public void SaveFile(Save save, string filename=null) {
        if(_currentFilename == "" && filename == null) {
            Debug.Log("Invalid filename");
            return;
        }

        string selectedFile = filename ?? _currentFilename;
        string path = GetFilePath(selectedFile);
        FileStream file = File.Exists(path) ? File.OpenWrite(path) : File.Create(path);
        new BinaryFormatter().Serialize(file, save);
        file.Close();

        SetCurrentFile(selectedFile, save);
    }

    void ResetCurrentFile() {
        _currentFilename = null;
        _currentSave = null;
    }
    void SetCurrentFile(string filename, Save save) {
        _currentFilename = filename;
        _currentSave = save;

        PlayerPrefs.SetString(StringResource.CurrentFile, _currentFilename);
        PlayerPrefs.Save();
    }


    void ManageSingleton() {
        int numSaveManagers = FindObjectsByType<SaveManager>(FindObjectsSortMode.None).Length;
        if(numSaveManagers == 1) {
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    

    


    string GetFilePath(string filename) {
        return _savePath + filename + ".save";
    }

    
}
