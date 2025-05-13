using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    // === GLOBAL VARIABLES ===
    public int score = 0;
    public bool hasKey = false;
    public string currentCheckpointId = "";
    public DoorRotation door; 
    [Header("Lighting Control")]
    public int respawnCount = 0;
    private Light[] sceneLights;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Find all lights in the scene
        sceneLights = FindObjectsOfType<Light>();
        door = FindObjectOfType<DoorRotation>();
    }

    private void CalculateRespawnState(int respawnCount)
    {
        switch (respawnCount)
        {
            case 0:
                UpdateLightRedColors(0f);
                door.show();
                break;
            case 1:
                UpdateLightRedColors(0.5f);
                door.hide();
                break;
            case 2:
                UpdateLightRedColors(1f);
                break;
            default:
                UpdateLightRedColors(0f);
                break;
        }
    }

    private void Update()
    {
        CalculateRespawnState(respawnCount);
    }

    private void UpdateLightRedColors(float percentage)
    {
        Color color = new Color(1f, 1f - percentage, 1f - percentage);
        foreach (var light in sceneLights)
        {
            if (light != null)
            {
                light.color = color;
            }
        }
    }
}
