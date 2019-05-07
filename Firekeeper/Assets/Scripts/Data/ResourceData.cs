using UnityEngine;

public enum ResourceType
{
    Wood = 1 << 0,
    Gold = 1 << 1,
    VeryExpensiveAndRareMaterial = 1 << 2
}

[CreateAssetMenu(menuName = "Resource Data")]
public class ResourceData : ScriptableObject
{
    public int resourcePoints;
    public ResourceType resourceType;
    public int requiredHarvestTime;
}
