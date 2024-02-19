using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //we want simple movement
    public Vector3 moveVector;
    public float walkSpeed;
    public float runSpeed;

    public Vector2 turnVector;
    private float tempYrot;
    private float tempXrot;
    public float turnXSpeed;
    public float turnYSpeed;

    public Rigidbody rbody;
    public float jumpPower;
    private int jumpCounter;
    public int jumpCounterMax;

    public bool isRunning;
    public bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rbody = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        turnVector = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        tempXrot += turnVector.x * turnXSpeed;
        tempYrot += -turnVector.y * turnYSpeed;
        tempYrot = Mathf.Clamp(tempYrot, -45, 45);
        transform.localRotation = Quaternion.Euler(tempYrot, tempXrot, 0);

        //Sprint input check
        if (Input.GetKey(KeyCode.LeftShift) && moveVector != Vector3.zero)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (moveVector != Vector3.zero && isRunning == false)
        {
            //we are walking forward
            transform.Translate(moveVector * walkSpeed * Time.deltaTime);
        }
        else if (moveVector != Vector3.zero && isRunning == true)
        {
            //we are running
            transform.Translate(moveVector * runSpeed * Time.deltaTime);
        }
    }
}
