using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove3 : MonoBehaviour
{
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float moveSpd;

    private CharacterController charController;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;

    [SerializeField] private int jumpCount = 0;
    [SerializeField] private int maxJumps = 2;

    private bool isJumping;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float horizInput = Input.GetAxis(horizontalInputName) * moveSpd;
        float vertInput = Input.GetAxis(verticalInputName) * moveSpd;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        charController.SimpleMove(forwardMovement + rightMovement);

        jumpInput();
    }

    private void jumpInput()


    {
        if (!isJumping)
            jumpCount = 0;
        if (Input.GetKeyDown(jumpKey) && !isJumping )
        {
           
            isJumping = true;
            StartCoroutine(jumpEvent());
        }
        else if(Input.GetKeyDown(jumpKey) && isJumping && jumpCount < maxJumps)
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
            charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            airTime += Time.deltaTime;
            yield return null;

        }
        while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above); 

        isJumping = false; 
    }
}
