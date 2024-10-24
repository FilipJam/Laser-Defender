using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject _gunHolder;
    List<Gun> _guns;

    GameplayAudioPlayer _audioPlayer;

    public GameObject GunHolder { get => _gunHolder; set => _gunHolder = value; }

    void Awake() {
        _audioPlayer = FindObjectOfType<GameplayAudioPlayer>();
        UpdateWeapons();
    }

    public void UpdateWeapons() {
        _guns = _gunHolder.GetComponentsInChildren<Gun>().ToList();
    }
    public void FireWeapons() {
        if(_guns == null) { return; }
        _guns.ForEach(gun => gun.Shoot());
        _audioPlayer.PlayShootingClip();
    }
}
