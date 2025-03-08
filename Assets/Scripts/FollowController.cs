using UnityEngine;

public class FollowController : MonoBehaviour
{
    public GameObject Player;
    public bool isFollowing;
    public float followSpeed = 3f;
    public float followDistance = 2f;
    private Vector3 moveDirection = Vector3.zero; // No movement
    Animator anim;

    [SerializeField]
    private CharacterController controller;
    
    // Start is called before the first frame update
    void Start()
    {
        // Find the GameObject with the tag "Player" when the game starts
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("isRunning", false); 
        controller = gameObject.GetComponent<CharacterController>();

    }
    private void Update()
    {
        transform.LookAt(Player.transform);
        if (isFollowing)
        {
            if (Vector3.Distance(Player.transform.position, transform.position) > followDistance)
            {
                anim.SetBool("isRunning", true);

                var moveDirection = Vector3.Normalize(Player.transform.position - transform.position);
                controller.SimpleMove(moveDirection * followSpeed);
            }
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) isFollowing = true;
    }
}