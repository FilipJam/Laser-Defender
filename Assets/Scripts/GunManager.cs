using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _gunLevels;

    public int MaxPower => _gunLevels.Count - 1;

    public GameObject GetGunsAt(int index) {
        return _gunLevels[index];
    }
}
