using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceData _resourceData;
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
    public int GetResourcePoints()
    {
        return _resourceData.resourcePoints;
    }

    public int GetHarvestTime()
    {
        return _resourceData.requiredHarvestTime;
    }

    public ResourceType GetResourceType()
    {
        return _resourceData.resourceType;
    }

    public void PayResourcesForFix(ResourceType type, Fence fence)
    {
        var resourceAmount = _resourceHud.GetResourcesAmount(type);

        if (resourceAmount >= resourceCost)
        {
            _resourceHud.RemoveResources(type, resourceCost);

            //TODO Fixing Animation

            fence.RestoreFenceHealth();

        }
    }

    public void PayResourcesForBuild(ResourceType type, Fence fence)
    {
        var resourceAmount = _resourceHud.GetResourcesAmount(type);

        if (resourceAmount >= resourceCost)
        {
            _resourceHud.RemoveResources(type, resourceCost);

            //TODO Build Animation

            fence.gameObject.SetActive(true);
        }
    }
}
