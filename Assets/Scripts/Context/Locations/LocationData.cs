using System;
using System.Collections.Generic;

[Serializable]
public class LocationData
{
    public List<string> AvailableFactions;
    public List<LocationEntity> Entities;
}

[Serializable]
public class LocationEntity
{
    public string Name;
    public int ID;
    public string Type;
    public string Faction;
    public float locationX;
    public float locationY;
}