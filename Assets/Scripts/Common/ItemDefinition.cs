[System.Serializable]
public class ItemDefinition
{
    public string Type;
    public ItemType ItemType;
    public string Id;
    public string Name;
    public string Description;
    public int Chance;
    public float RageIncrease;
    public int? Level;
}

public enum ItemType
{
    ShelfUpgrade,
    ShovelUpgrade,
    Misc
}