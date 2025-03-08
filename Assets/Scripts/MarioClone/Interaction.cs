
    
    using UnityEngine;

    public class Interaction : MonoBehaviour
    {
        public GameObject starPrefab;
        private bool isCollected = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !isCollected)
            {
                Debug.Log("Player collected star");
                CollectStar();
            }
        }

        private void CollectStar()
        {
            isCollected = true;
            GameManager.Instance.CollectStar();
            gameObject.SetActive(false);
        }
    }
