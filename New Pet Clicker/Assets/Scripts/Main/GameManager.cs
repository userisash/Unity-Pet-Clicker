using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameObject currentPetInstance;
    public Transform petSpawnLocation; // You need to set this reference in the main scene
    private Pet selectedPet;
    private bool petConfirmed = false; // Add a flag to check if the pet selection has been confirmed

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // You can use the scene name or build index to check if it's the main game scene
        if (scene.name == "MainScene" && petConfirmed) // Check if the pet has been confirmed
        {
            // Find the petSpawnLocation in the current scene
            petSpawnLocation = GameObject.FindWithTag("PetSpawnLocationTag").transform;
            SpawnSelectedPet();
        }
    }

    public void SetSelectedPet(Pet newSelectedPet)
    {
        selectedPet = newSelectedPet;
    }

    // Call this method from your UI button to confirm the selection of the pet
    public void ConfirmPetSelection()
    {
        petConfirmed = true;
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            SpawnSelectedPet();
        }
        else
        {
            // Load the Main Scene if not already there
            SceneManager.LoadScene("MainScene");
        }
    }

    private void SpawnSelectedPet()
    {
        // If there's already a pet in the scene, destroy it
        if (currentPetInstance != null)
        {
            Destroy(currentPetInstance);
        }

        // Instantiate the new pet at the petSpawnLocation
        if (petSpawnLocation != null && selectedPet != null)
        {
            currentPetInstance = Instantiate(selectedPet.petPrefab, petSpawnLocation.position, Quaternion.identity);

            // Optionally parent it to the spawn location (if you want to maintain a clean hierarchy)
            currentPetInstance.transform.SetParent(petSpawnLocation);

            // If the pet has any default animations, they should automatically play if the Animator component is set up correctly.
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

