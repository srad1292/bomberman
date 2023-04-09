using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] GameObject pickupPrefab;

    public GameObject GetPickupPrefab() {
        return pickupPrefab;
    }


}
