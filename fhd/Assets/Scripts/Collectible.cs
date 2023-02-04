using UnityEngine;


public class Collectible : MonoBehaviour
{
    public void Collect()
    {
        CollectibleState.Instance.collectablesCollected++;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) Collect();
    }
}




