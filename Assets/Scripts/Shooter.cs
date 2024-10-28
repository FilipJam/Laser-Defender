using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // empty game object with Guns as children
    [SerializeField] GameObject _gunHolder;

    List<Gun> _guns;
    GameplayAudioPlayer _audioPlayer;

    // PowerManager equips/unequips guns using this property
    public GameObject GunHolder { get => _gunHolder; set => _gunHolder = value; }

    void Awake() {
        _audioPlayer = FindObjectOfType<GameplayAudioPlayer>();
        PrepareWeapons();
    }

    // prepares guns for shooting
    public void PrepareWeapons() {
        // gets Gun component from children into a list for easy access
        _guns = _gunHolder.GetComponentsInChildren<Gun>().ToList();
    }
    public void FireWeapons() {
        if(_guns == null) { return; }

        // shoot every gun
        _guns.ForEach(gun => gun.Shoot());
        _audioPlayer.PlayShootingClip();
    }
}
