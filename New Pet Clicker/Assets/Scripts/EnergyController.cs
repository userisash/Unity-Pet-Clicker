using UnityEngine;
using UnityEngine.UI;

public class EnergyController : MonoBehaviour
{
    public Energy energy;
    public Slider energySlider; // Drag your energy slider here in the Unity Editor

    public static EnergyController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        energy.Initialize(energySlider);
    }

    void Update()
    {
        energy.UpdateEnergy();
    }
}
