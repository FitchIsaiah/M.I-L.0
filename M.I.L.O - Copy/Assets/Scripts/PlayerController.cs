using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public float jumpForce;
    public float gravityScale = 5f;
    public Vector3 moveDirection;
    public CharacterController charController;

    private Camera theCam;

    public GameObject playerModel;
    public float rotateSpeed;

    public bool isKnocking;
    public float knockBackLength = .5f;
    private float knockbackCounter;
    public Vector2 knockbackPower;

    public GameObject[] playerPieces;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        theCam = Camera.main;
    }


    void Update()
    {
       
        if (!isKnocking)
        {
            float yStore = moveDirection.y;
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection.Normalize();
            moveDirection = moveDirection * moveSpeed;
            moveDirection.y = yStore;

            if (charController.isGrounded)
            {
                moveDirection.y = 0f;


                if (Input.GetButtonDown("Jump"))
                {

                    moveDirection.y = jumpForce;
                }

            }
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            //transform.position = transform.position + (moveSpeed * Time.deltaTime * moveDirection);
            charController.Move(moveDirection * Time.deltaTime);

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {

                transform.rotation = Quaternion.Euler(0f, theCam.transform.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));


                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);

            }

        }

        if (isKnocking)
        {
            knockbackCounter -= Time.deltaTime;

            float yStore = moveDirection.y;
            moveDirection = playerModel.transform.forward * -knockbackPower.x; 
            moveDirection.y = yStore;


            if (charController.isGrounded)
            {
                moveDirection.y = 0f;

            }
              

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            charController.Move(moveDirection * Time.deltaTime);


            if (knockbackCounter <= 0)
            {
                isKnocking = false;
            }
        }


        
    } 


    public void Knockback()
    {
        isKnocking = true;
        knockbackCounter = knockBackLength;
        Debug.Log("Knocked Back");
        moveDirection.y = knockbackPower.y;
        charController.Move(moveDirection * Time.deltaTime);

    }

}
