using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
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
        InitSavePath();
    }
    void Start()
    {
        LoadMostRecentFile();
    }

    public string[] GetFilenames() {
        string[] filePaths = Directory.GetFiles(_savePath);
        string[] result = new string[filePaths.Length];
        // converts array of file paths into array of filenames
        for(int i = 0; i < filePaths.Length; i++) {
            // only filename, not full file path
            // last element of array is filename
            string filename = filePaths[i].Split(Path.AltDirectorySeparatorChar).Last();
            // filename without file extension
            result[i] = filename[..filename.IndexOf('.')];
        }
        return result;
    }
    public void DeleteFile(string filename) {
        string path = GetFilePath(filename);
        File.Delete(path);

        if(filename == _currentFilename) {
            // reset if current file was deleted
            ResetCurrentFile();
        }
    }
    public void LoadFile(string filename) {
        FileStream file = null;
        try {
            file = File.OpenRead(GetFilePath(filename));
            // deserializes bytes into an object
            // then cast deserialized object as Save so it can be used
            var save = (Save)new BinaryFormatter().Deserialize(file);
            // set current file to loaded file
            SetCurrentFile(filename, save);
        } catch(FileNotFoundException ex) {
            Debug.Log(ex.Message);
        } finally {
            // '?' because if failed at File.OpenRead then FileStream is still null
            file?.Close();
        }
    }
    public void SaveFile(Save save, string filename=null) {
        // do not accept blanks
        if(_currentFilename == "" && filename == null) {
            Debug.Log("Invalid filename");
            return;
        }

        // if no filename specified, use current file instead
        string selectedFile = filename ?? _currentFilename;
        string path = GetFilePath(selectedFile);

        FileStream file = null;
        try {
            // writes into file if it exists and creates new file if doesn't exist
            file = File.Exists(path) ? File.OpenWrite(path) : File.Create(path);
            // serialize Save object into bytes
            new BinaryFormatter().Serialize(file, save);
            // set current file to saved file
            SetCurrentFile(selectedFile, save);
        } catch(Exception ex) {
            Debug.Log(ex.Message);
        } finally {
            // '?' because if failed at File.OpenRead then FileStream is still null
            file?.Close();
        }
    }

    void LoadMostRecentFile()
    {
        // PlayerPrefs has no currentFile key when loaded for first time
        if (PlayerPrefs.HasKey(StringResource.CurrentFile))
        {
            LoadFile(PlayerPrefs.GetString(StringResource.CurrentFile));
        }
    }
    void ResetCurrentFile() {
        _currentFilename = null;
        _currentSave = null;
    }
    void SetCurrentFile(string filename, Save save) {
        _currentFilename = filename;
        _currentSave = save;

        // saves filename as most recent, so that game remembers next time its started
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
    void InitSavePath() {
        _savePath = Application.persistentDataPath + "/save/";
        if(!Directory.Exists(_savePath)) {
            Directory.CreateDirectory(_savePath);
        }
    }
    string GetFilePath(string filename) {
        return _savePath + filename + ".save";
    }
}
