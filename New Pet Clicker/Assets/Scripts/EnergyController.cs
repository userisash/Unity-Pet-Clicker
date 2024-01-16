using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyController : MonoBehaviour
{
    public Energy energy;
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
        energy.Initialize();
    }

    void Update()
    {
        energy.UpdateEnergy();
    }

}
