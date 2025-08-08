using UnityEngine;

public class playerAnimator : MonoBehaviour
{
    [SerializeField] private player player;
    private const string IS_WALKING = "IsWalking";
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
        
    }
    void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
