using System.Collections.Generic;

[System.Serializable]
public class LocationData
{
    public string Name;
    public int ID;
    public string Type;
    public string Faction;
    public float locationX;
    public float locationY;
}

[System.Serializable]
public class LocationsWrapper
{
    public List<string> AvailableFactions;
    public List<LocationData> Entities;
}
