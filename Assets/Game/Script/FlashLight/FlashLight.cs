using System;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField]
    private Light _light;
    [SerializeField]
    PlayerCharacter _owner;
    [SerializeField]
    private float _initialBatteryLevel = 100;

    [SerializeField]
    private float _batteryDrainRate = 1;

    private float _batterLevel;

    public bool HasFlashlight => _owner.Inventory.CheckItem("FlashLight_001");
    public bool HasBattery => _batterLevel > 0;

    private void Awake()
    {
        _batterLevel = _initialBatteryLevel;
    }
    public void UseFlashLight()
    {
        if (HasFlashlight == true && _light != null)
        {
            if (HasBattery == true)
            {
                _light.enabled = !_light.enabled;
            }
            else
            {
                _light.enabled = false;
            }
        }
    }

    public void RefillBatteryLevel()
    {
    _batterLevel = _initialBatteryLevel;
    }

    private void Update()
    {
        UpdateFlashlightRotation();
        UpdateBatteryLevel();
    }

    private void UpdateFlashlightRotation()
    {
        _light.transform.rotation = Camera.main.transform.rotation;
    }

    private void UpdateBatteryLevel()
    {
        if (_light != null && _light.enabled == true)
        {
            if (HasBattery == true)
            {
                _batterLevel = _batterLevel - _batteryDrainRate * Time.deltaTime;
            }
            else
            {
                _batterLevel = 0;
                _light.enabled = false;
            }
        }
    }
}
