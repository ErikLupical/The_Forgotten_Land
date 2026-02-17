using UnityEngine;
using AYellowpaper.SerializedCollections;
using UnityEngine.UI;
using TMPro;

public class StatsUI : MonoBehaviour
{
    [Header("Avatar")]
    [SerializeField]
    private SerializedDictionary<string, Sprite> avatars;
    public Image avatar;
    public Button avatarButton;

    private GameObject player;
    private float speed = 0f;
    private int attack = 0;
    private string abilityName = string.Empty;
    private string abilityDescription = string.Empty;
    private string passiveName = string.Empty;
    private string passiveDescription = string.Empty;

    [Header("Stats")]
    [SerializeField]
    public GameObject stats;
    private CanvasGroup statsCG;
    private bool statsOpen = false;
    [SerializeField]
    private TextMeshProUGUI speedValue;
    [SerializeField]
    private TextMeshProUGUI attackValue;
    [SerializeField]
    private TextMeshProUGUI abilityNameValue;
    [SerializeField]
    private TextMeshProUGUI abilityDescriptionValue;
    [SerializeField]
    private TextMeshProUGUI passiveNameValue;
    [SerializeField]
    private TextMeshProUGUI passiveDescriptionValue;


    private void Awake()
    {
        avatarButton.onClick.AddListener(ToggleStats);

        player = GameObject.FindGameObjectWithTag("Player");
        UpdateStats();

        statsCG = stats.GetComponent<CanvasGroup>();
    }

    public void UpdateStats()
    {
        speed = player.GetComponent<EntityBehavior>().speed;
        attack = player.GetComponent<EntityCombat>().attack;
        abilityName = player.GetComponent<EntityCombat>().ability.name;
        abilityDescription = player.GetComponent<EntityCombat>().ability.description;

        speedValue.text = speed.ToString();
        attackValue.text = attack.ToString();
        abilityNameValue.text = abilityName;
        abilityDescriptionValue.text = abilityDescription;
        passiveNameValue.text = passiveName;
        passiveDescriptionValue.text = passiveDescription;
    }

    public void ToggleStats()
    {
        if (statsOpen)
        {
            statsCG.alpha = 0;
            statsCG.interactable = false;
            statsCG.blocksRaycasts = false;
        }
        else
        {
            UpdateStats();
            statsCG.alpha = 1;
            statsCG.interactable = true;
            statsCG.blocksRaycasts = true;
        }

        statsOpen = !statsOpen;
    }
}
