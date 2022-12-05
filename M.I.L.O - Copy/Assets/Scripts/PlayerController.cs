using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale = 5f;
    public Vector3 moveDirection;
    public CharacterController charController;

    private Camera theCam;

    public GameObject playerModel;
    public float rotateSpeed;


    void Start()
    {
        theCam = Camera.main;
    }

   
    void Update()
    {
        float yStore = moveDirection.y;
        //moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

       
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        moveDirection.Normalize();
        moveDirection = moveDirection * moveSpeed;
        moveDirection.y = yStore;
        
        if (Input.GetButtonDown("Jump"))
        {

            moveDirection.y = jumpForce;
        }


        moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

        //transform.position = transform.position + (moveSpeed * Time.deltaTime * moveDirection);
        charController.Move(moveDirection * Time.deltaTime);

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") !=0)
        {

            transform.rotation = Quaternion.Euler(0f, theCam.transform.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            

            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);

        }
    } 
}
