using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Resource : MonoBehaviour
{
    [SerializeField] private GameObject _resourceHudPrefab;

    public int resourceCost { get; set; }

    private ResourceHud _resourceHud;

    private void Start()
    {
        _resourceHud = _resourceHudPrefab.GetComponent<ResourceHud>();

        if (_resourceHud == null)
        {
            Debug.LogError("attatch ResourceHud prefab");
        }
    }

    public bool PayResourcesForFix(ResourceType type)
    {
        var resourceAmount = _resourceHud.GetResourcesAmount(type);

        if (resourceAmount >= resourceCost)
        {
            _resourceHud.RemoveResources(type, resourceCost);

            //TODO Fixing Animation
            return true;
        }
        return false;
    }
    

    public bool PayResourcesForBuild(ResourceType type)
    {
        var resourceAmount = _resourceHud.GetResourcesAmount(type);

        if (resourceAmount >= resourceCost)
        {
            _resourceHud.RemoveResources(type, resourceCost);

            return true;
        }

        return false;
    }

    public void GainResourcesFromHarvest(ResourceType type, int gainingAmount, GameObject tree)
    {
        _resourceHud.AddResources(type, gainingAmount);

        //TODO Harvest Animation

        tree.SetActive(false);
    }
}
