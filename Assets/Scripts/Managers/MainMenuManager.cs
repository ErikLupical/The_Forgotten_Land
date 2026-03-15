using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public CanvasGroup MenuCG;
    public CanvasGroup CharacterCG;

    [Header("Player")]
    public TMP_Dropdown factionDropdown;
    public TMP_Dropdown typeDropdown;

    public GameObject playerPrefab;

    public CharacterStats knightStats;
    public CharacterStats archerStats;
    public CharacterStats rogueStats;
    public CharacterStats mageStats;

    public TextMeshProUGUI Attack;
    public TextMeshProUGUI Speed;
    public TextMeshProUGUI HP;
    public TextMeshProUGUI DefaultAbility;
    public TextMeshProUGUI AbilityDescription;

    CharacterStats currentStats;

    void Start()
    {
        UpdateStats();
        typeDropdown.onValueChanged.AddListener(delegate { UpdateStats(); });
    }

    public void UpdateStats()
    {
        switch (typeDropdown.value)
        {
            case 0: currentStats = knightStats; break;
            case 1: currentStats = archerStats; break;
            case 2: currentStats = mageStats; break;
            case 3: currentStats = rogueStats; break;
        }

        Attack.text = "Attack: " + currentStats.attack;
        Speed.text = "Speed: " + currentStats.speed;
        HP.text = "HP: " + currentStats.hp;

        DefaultAbility.text = "Ability: " + currentStats.defaultAbility.abilityName;
        AbilityDescription.text = currentStats.defaultAbility.description;
    }

    public void StartGame()
    {
        GameData.selectedFaction = (EntityBehavior.Faction)factionDropdown.value;
        GameData.selectedType = (EntityBehavior.EntityType)typeDropdown.value;
        GameData.selectedStats = currentStats;

        SceneManager.LoadScene("World");
    }


    public void OpenCharacterMenu()
    {
        MenuCG.alpha = 0;
        MenuCG.interactable = false;
        MenuCG.blocksRaycasts = false;

        CharacterCG.alpha = 1;
        CharacterCG.interactable = true;
        CharacterCG.blocksRaycasts = true;
    }

    public void CloseCharacterMenu()
    {
        CharacterCG.alpha = 0;
        CharacterCG.interactable = false;
        CharacterCG.blocksRaycasts = false;

        MenuCG.alpha = 1;
        MenuCG.interactable = true;
        MenuCG.blocksRaycasts = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
