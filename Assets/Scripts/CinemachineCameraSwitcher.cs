using UnityEngine;
// using UnityEngine.InputSystem;

public class CinemachineCameraSwitcher : MonoBehaviour
{
    [SerializeField]
    // private InputAction myAction;
    private Animator animator;
    public bool normalCamera = false;
    public bool enemyCamera = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // private void OnEnable()
    // {
    //     myAction.Enable();
    // }

    // private void OnDisable()
    // {
    //     myAction.Disable();
    // }
    // // Start is called before the first frame update
    void Start()
    {
        // myAction.performed += _ => SwitchState();\
    }

    // // Update is called once per frame
    void Update()
    {

    }

    public void SwitchState()
    {
        if(enemyCamera)
        {
            animator.Play("Fight Camera Follow");
        }
        else
        {
            if(normalCamera)
            {
                animator.Play("Normal Camera Follow");
            }
            else
            {
                animator.Play("Zoomed Camera Follow");
            }
        }
    }
}
