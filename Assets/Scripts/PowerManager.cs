using UnityEngine;

public class PowerManager : MonoBehaviour
{
    [SerializeField] int _power = 2;
    
    Shooter _shooter;

    GunManager _gunManager;
    
    void Awake() {
        _gunManager = FindObjectOfType<GunManager>();
        _shooter = GetComponent<Shooter>();
    }

    void Start() {
        AddPower(0);
    }

    public void AddPower(int amount) {
        _power = Mathf.Clamp(_power + amount, 0, _gunManager.MaxPower);
        ChangeGuns();
    }

    void ChangeGuns() {
        DestroyGuns();
        EquipGuns();
    }

    void DestroyGuns() {
        if(_shooter.GunHolder != null) {
            _shooter.GunHolder.SetActive(false);
            Destroy(_shooter.GunHolder);
        }
    }

    void EquipGuns() {
        _shooter.GunHolder = Instantiate(_gunManager.GetGunsAt(_power), gameObject.transform);
        _shooter.UpdateWeapons();
    }
}
