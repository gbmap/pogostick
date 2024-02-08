using UnityEngine;

namespace DRAP.Objectives
{
    public class Collectible : MonoBehaviour
    {
        public System.Action<Collectible> OnCollect;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Collect();
                Destroy(gameObject);
            }
        }

        private void Collect()
        {
            Debug.Log("Collectible collected.");
            OnCollect?.Invoke(this);
        }
    }
}