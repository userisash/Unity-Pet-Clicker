using UnityEngine;

public class PetSelection : MonoBehaviour
{
    public static PetSelection Instance;  // Singleton pattern

    public enum PetType { None, Cat, Dog, Rabbit, Hamster }
    public PetType SelectedPet = PetType.None;

    private void Awake()
    {
        // Ensure only one instance of PetSelectionManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps the DataManager when transitioning scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectCat()
    {
        PetSelection.Instance.SelectedPet = PetSelection.PetType.Cat;
        Debug.Log("Selected Cat");
        // Load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void SelectDog()
    {
        PetSelection.Instance.SelectedPet = PetSelection.PetType.Dog;
        Debug.Log("Selected Dog");
        // Load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void SelectRabbit()
    {
        PetSelection.Instance.SelectedPet = PetSelection.PetType.Rabbit;
        Debug.Log("Selected Rabbit");
        // Load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void SelectHamster()
    {
        PetSelection.Instance.SelectedPet = PetSelection.PetType.Hamster;
        Debug.Log("Selected Hamster");
        // Load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
