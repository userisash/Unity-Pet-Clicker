using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PetInventoryUI : MonoBehaviour
{
    public GameObject petItemPrefab;
    public Transform inventoryContainer;
    public Image selectedPetDisplay;
    public Button confirmSelectionButton; // Reference to the confirm selection button in the UI
    public PetInventory petInventory; // Your scriptable object or another form of storage for owned pets.

    private Pet currentlySelectedPet; // Temporarily store the selected pet here

    private void Start()
    {
        PopulateInventory();
        confirmSelectionButton.onClick.AddListener(ConfirmSelection); // Add a listener to the confirm button
        confirmSelectionButton.interactable = false; // Start with the confirm button disabled
        string selectedPetName = PlayerPrefs.GetString("SelectedPet", "");
        Pet petToSelect = selectedPetName != "" ? FindPetByName(selectedPetName) : null;
        SelectPet(petToSelect ?? petInventory.ownedPets[0]);
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
        return null; // Pet not found
    }

    void PopulateInventory()
    {
        foreach (Pet pet in petInventory.ownedPets)
        {
            GameObject item = Instantiate(petItemPrefab, inventoryContainer);
            item.GetComponent<Image>().sprite = pet.petSprite;
            item.GetComponent<Button>().onClick.AddListener(() => SelectPet(pet));
        }
    }

    void SelectPet(Pet pet)
    {
        selectedPetDisplay.sprite = pet.petSprite;
        currentlySelectedPet = pet; // Temporarily store the selected pet
        confirmSelectionButton.interactable = true; // Enable the confirm button
    }

    void ConfirmSelection()
    {
        if (currentlySelectedPet != null)
        {
            GameManager.Instance.SetSelectedPet(currentlySelectedPet); // Confirm the selection in the GameManager
            SceneManager.LoadScene("MainScene"); // Load the main game scene
        }
    }
}
