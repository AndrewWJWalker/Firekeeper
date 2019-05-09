using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceHud : MonoBehaviour
{


    private Dictionary<ResourceType, int> _currentResources;

    private int _woodAmount;
    private int _goldAmount;
    private int _veryExpensiveAndRareMaterialAmount;

    [SerializeField] private Text _woodAmountText;
    [SerializeField] private Text _goldAmountText;
    [SerializeField] private Text _veryExpensiveAndRareMaterialAmountText;

    [SerializeField] private int _woodStartAmount = 100;
    [SerializeField] private int _goldStartAmount = 0;

    private void Start()
    {
        _woodAmount = _woodStartAmount;
        _goldAmount = _goldStartAmount;
        _veryExpensiveAndRareMaterialAmount = 0;

        _currentResources = new Dictionary<ResourceType, int>();

        _currentResources.Add(ResourceType.Wood, _woodAmount);
        _currentResources.Add(ResourceType.Gold, _goldAmount);

        UpdateDisplay(ResourceType.Wood);
        UpdateDisplay(ResourceType.Gold);
    }

    private void UpdateDisplay(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Wood:
                _woodAmountText.text = _currentResources[type].ToString();
                break;
            case ResourceType.Gold:
                _goldAmountText.text = _currentResources[type].ToString();
                break;
            case ResourceType.VeryExpensiveAndRareMaterial:
                _veryExpensiveAndRareMaterialAmountText.text = _currentResources[type].ToString();
                break;
            default:
                Debug.LogError("Resource Type is missing, it should be added through code!");
                break;
        }
        
    } 

    public int GetResourcesAmount(ResourceType type)
    {
        if (!_currentResources.ContainsKey(type))
        {
            Debug.LogError("Resource type " + type + " does not exist");
            return 0;
        }
        return _currentResources[type];
    }


    public void AddResources(ResourceType type, int amount)
    {
        if (!_currentResources.ContainsKey(type))
        {
            _currentResources.Add(type, amount);
        }
        else
        {
            _currentResources[type] += amount;
        }

        UpdateDisplay(type);
    }

    public void RemoveResources(ResourceType type, int amount)
    {
        if (!_currentResources.ContainsKey(type))
        {
            return;
        }

        if (_currentResources[type] - amount < 0 ) { return; }

        _currentResources[type] -= amount;
        UpdateDisplay(type);
    }

    public void ClearResources()
    {
        _woodAmountText.text = 0.ToString();
        _goldAmountText.text = 0.ToString();
        _veryExpensiveAndRareMaterialAmountText.text = 0.ToString();

        _currentResources.Clear();
    }
}
