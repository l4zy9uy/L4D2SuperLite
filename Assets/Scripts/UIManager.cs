using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    [SerializeField] private TextMeshProUGUI _ammo;
    [SerializeField] private TextMeshProUGUI _fpsText;

    void Start()
    {
        
    }

    void Update()
    {
        Gun _activeGun = WeaponController.Instance.activeGun;
        _ammo.SetText(_activeGun.bulletsLeft + " / " + _activeGun._currentBullets);
        FPSCounter.Instance.displayFPS(_fpsText);
    }

}
