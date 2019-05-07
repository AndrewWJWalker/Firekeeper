using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceData resourceData;

    public int GetResourcePoints()
    {
        return resourceData.resourcePoints;
    }

    public int GetHarvestTime()
    {
        return resourceData.requiredHarvestTime;
    }

    public ResourceType GetResourceType()
    {
        return resourceData.resourceType;
    }
}
