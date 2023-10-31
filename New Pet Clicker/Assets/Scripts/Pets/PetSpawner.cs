using UnityEngine;

public class PetSpawner : MonoBehaviour
{
    public GameObject catPrefab;
    public GameObject dogPrefab;
    public GameObject rabbitPrefab;
    public GameObject hamsterPrefab;
    public Transform petContainer; // This is the SelectedPetContainer where pets will be instantiated.

    private void Start()
    {
        GameObject spawnedPet = null; // This will store a reference to the instantiated pet.

        switch (PetSelection.Instance.SelectedPet)
        {
            case PetSelection.PetType.Cat:
                spawnedPet = Instantiate(catPrefab, petContainer.position, Quaternion.identity);
                break;
            case PetSelection.PetType.Dog:
                spawnedPet = Instantiate(dogPrefab, petContainer.position, Quaternion.identity);
                break;
            case PetSelection.PetType.Rabbit:
                spawnedPet = Instantiate(rabbitPrefab, petContainer.position, Quaternion.identity);
                break;
            case PetSelection.PetType.Hamster:
                spawnedPet = Instantiate(hamsterPrefab, petContainer.position, Quaternion.identity);
                break;
        }

        // Set the spawned pet as a child of the SelectedPetContainer
        if (spawnedPet != null)
            spawnedPet.transform.SetParent(petContainer);
    }
}
