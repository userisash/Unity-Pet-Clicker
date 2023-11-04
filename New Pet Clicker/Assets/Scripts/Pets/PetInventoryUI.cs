using UnityEngine;
using UnityEngine.UI;

public class PetInventoryUI : MonoBehaviour
{
    public GameObject petItemPrefab;
    public Transform inventoryContainer;
    public Image selectedPetDisplay;
    public Button confirmSelectionButton; // Reference to the confirm selection button
    public PetInventory petInventory; // Your scriptable object or another form of storage for owned pets.

    private Pet selectedPet; // Temporarily store the selected pet here

    private void Start()
    {
        PopulateInventory();
        confirmSelectionButton.onClick.AddListener(ConfirmSelection); // Add a listener to the confirm button
        confirmSelectionButton.interactable = false; // Start with the confirm button disabled

        // Optionally select a default pet at start.
        if (petInventory.ownedPets.Count > 0)
        {
            SelectPet(petInventory.ownedPets[0]);
        }
    }

    void PopulateInventory()
    {
        foreach (Pet pet in petInventory.ownedPets)
        {
            GameObject item = Instantiate(petItemPrefab, inventoryContainer);
            item.GetComponent<Image>().sprite = pet.petSprite;
            // Use a lambda expression to pass the pet to the SelectPet function.
            item.GetComponent<Button>().onClick.AddListener(() => SelectPet(pet));
        }
    }

    void SelectPet(Pet pet)
    {
        selectedPetDisplay.sprite = pet.petSprite;
        selectedPet = pet; // Store the selected pet
        confirmSelectionButton.interactable = true; // Enable the confirm button
    }

    public void ConfirmSelection()
    {
        if (selectedPet != null)
        {
            GameManager.Instance.SetSelectedPet(selectedPet); // Set the pet in the GameManager when confirmed
            // Optionally, switch to the main scene after confirming the selection
            // SceneManager.LoadScene("MainScene");
        }
    }
}
