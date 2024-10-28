using System.Diagnostics;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    [SerializeField] int _power = 0;
    
    Shooter _shooter;
    GunManager _gunManager;
    
    void Awake() {
        _gunManager = FindObjectOfType<GunManager>();
        _shooter = GetComponent<Shooter>();
    }

    void Start() {
        // equips guns at initial power level
        SwitchGuns();
    }

    public void AddPower(int amount) {
        // changes power level of guns
        _power = Mathf.Clamp(_power + amount, 0, _gunManager.MaxPower);
        // equips new guns with new power level
        SwitchGuns();
    }

    void SwitchGuns() {
        // Destroy current guns
        DestroyGuns();
        // Equip new guns
        EquipGuns();
    }

    void DestroyGuns() {
        Destroy(_shooter.GunHolder);
    }

    void EquipGuns() {
        // spawns specified guns as children of this gameObject
        _shooter.GunHolder = Instantiate(_gunManager.GetGunsAt(_power), transform);
        _shooter.PrepareWeapons();
    }
}
