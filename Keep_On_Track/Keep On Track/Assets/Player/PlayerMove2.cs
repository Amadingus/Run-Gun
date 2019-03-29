using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{
    public Rigidbody rb;

    public float forwardThrust = 2000f;
    public float upwardThrust = 100f;
    public float sideThrust1 = 500f;
    public float sideThrust2 = -500f;

    private CharacterController charController;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;

    [SerializeField] private int jumpCount = 0;
    [SerializeField] private int maxJumps = 2;

    private bool isJumping;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(0, 0, forwardThrust * Time.deltaTime);

        if (Input.GetKey("d"))
        {
            rb.AddForce(sideThrust1 * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(sideThrust2 * Time.deltaTime, 0, 0);
        }

    }

    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        jumpInput();
    }

    private void jumpInput()
    { 
    if (!isJumping)
            jumpCount = 0;
        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {

            isJumping = true;
            StartCoroutine(jumpEvent());
}
        else if (Input.GetKeyDown(jumpKey) && isJumping && jumpCount<maxJumps)
        {
            jumpCount += 1;
            isJumping = true;
            StartCoroutine(jumpEvent());
        }
    }

    private IEnumerator jumpEvent()
    {
        float airTime = 0.0f;

        do
        {
            float jumpForce = jumpFallOff.Evaluate(airTime);
            rb.AddForce(0, 500 * jumpForce * jumpMultiplier * Time.deltaTime, 0);
            airTime += Time.deltaTime;
            yield return null;

        }
        while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        isJumping = false;
    }
}

