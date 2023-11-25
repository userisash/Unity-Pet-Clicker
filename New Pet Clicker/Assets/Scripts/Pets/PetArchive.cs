using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PetArchive", menuName = "Pet Inventory/Pet Archive")]
public class PetArchive : ScriptableObject
{
    public List<Pet> allPets;
}
