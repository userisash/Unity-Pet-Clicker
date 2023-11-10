using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PetInventory petInventory;
    [SerializeField]
    private GameObject currentPetInstance;
    [SerializeField]
    private Transform petSpawnLocation; // Assign this to the transform where pets should appear

    private string selectedPetKey = "SelectedPet";
    private Pet selectedPet;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadSelectedPet();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        petSpawnLocation = GameObject.FindWithTag("PetSpawnLocationTag").transform; // Find the petSpawnLocation by tag

        if (selectedPet != null && scene.name == "MainScene") // Replace "MainScene" with your main scene's name
        {
            InstantiateSelectedPet();
        }
    }

    public void SetSelectedPet(Pet newSelectedPet)
    {
        selectedPet = newSelectedPet;

        // Save the selected pet's name to PlayerPrefs
        PlayerPrefs.SetString(selectedPetKey, selectedPet.petName);
        PlayerPrefs.Save();
    }

    private void LoadSelectedPet()
    {
        string petName = PlayerPrefs.GetString(selectedPetKey, "");
        selectedPet = !string.IsNullOrEmpty(petName) ? FindPetByName(petName) : null;

        if (selectedPet != null)
        {
            InstantiateSelectedPet();
        }
    }

    private Pet FindPetByName(string petName)
    {
        foreach (var pet in petInventory.ownedPets)
        {
            if (pet.petName == petName)
            {
                return pet;
            }
        }

        // If we reach this point, no pet with the given name was found.
        return null;
    }

    private void InstantiateSelectedPet()
    {
        // If there's already a pet in the scene, destroy it
        if (currentPetInstance != null)
        {
            Destroy(currentPetInstance);
        }

        // Instantiate the new pet at the petSpawnLocation
        currentPetInstance = Instantiate(selectedPet.petPrefab, petSpawnLocation.position, Quaternion.identity);

        // Optionally parent it to the spawn location (if you want to maintain a clean hierarchy)
        currentPetInstance.transform.SetParent(petSpawnLocation, false);

        // If the pet has any default animations, they should automatically play if the Animator component is set up correctly.
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}