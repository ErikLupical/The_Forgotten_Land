using UnityEngine;

public class LocationSpawner : MonoBehaviour
{
    public string jsonFileName = "Locations";
    public GameObject locationPrefab;

    void Start()
    {
        LoadLocations();
    }

    void LoadLocations()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);

        if (jsonFile == null)
        {
            Debug.LogError("JSON file not found!");
            return;
        }

        LocationData data = JsonUtility.FromJson<LocationData>(jsonFile.text);

        foreach (LocationEntity entity in data.Entities)
        {
            SpawnLocation(entity);
        }
    }

    void SpawnLocation(LocationEntity entity)
    {
        Vector2 position = new Vector2(entity.locationX, entity.locationY);

        GameObject obj = Instantiate(locationPrefab, position, Quaternion.identity);

        EntityInteractable interactable = obj.GetComponent<EntityInteractable>();

        interactable.entityName = entity.Name;
        interactable.ID = entity.ID;
        interactable.type = entity.Type;
        interactable.faction = entity.Faction;
    }
}
