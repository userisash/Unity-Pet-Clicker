using UnityEngine;

public class PetSliderController : MonoBehaviour
{
    public Transform[] pets;
    public float offsetBetweenPets;
    public int currentPetIndex = 1;
    public float lerpSpeed = 5f;

    private Vector3 targetPosition;

    private void Start()
    {
        // Set the initial target position to the current position
        targetPosition = transform.position;
        UpdateVisiblePets();
    }

    private void Update()
    {
        // Lerp the position for smooth transition
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
    }

    public void MoveRight()
    {
        if (currentPetIndex < pets.Length - 1)
        {
            currentPetIndex++;
            targetPosition -= new Vector3(offsetBetweenPets, 0, 0);
            UpdateVisiblePets();
        }
    }

    public void MoveLeft()
    {
        if (currentPetIndex > 0)
        {
            currentPetIndex--;
            targetPosition += new Vector3(offsetBetweenPets, 0, 0);
            UpdateVisiblePets();
        }
    }

    private void UpdateVisiblePets()
    {
        for (int i = 0; i < pets.Length; i++)
        {
            if (i == currentPetIndex)
                pets[i].localScale = new Vector3(2.5f, 2.5f, 2.5f); // make the current pet bigger
            else
                pets[i].localScale = Vector3.one; // reset the size of other pets

            // Hide the last or the first pet based on the current index
            if ((i == 0 && currentPetIndex == pets.Length - 1) || (i == pets.Length - 1 && currentPetIndex == 0))
                pets[i].gameObject.SetActive(false);
            else
                pets[i].gameObject.SetActive(true);
        }
    }
}
