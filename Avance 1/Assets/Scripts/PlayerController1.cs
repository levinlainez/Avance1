using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    public float HorizontalMove;
    public float VerticalMove;

    private Vector3 playerInput;

    public CharacterController player;
    public float PlayerSpeed;
    private Vector3 MovePlayer;

    public float gravity = 9.8f;
    public float FallVelocity;
    public float JumpForce;

    public Camera MainCam;
    private Vector3 CamForward;
    private Vector3 CamRight;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMove = Input.GetAxis("Horizontal");
        VerticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(HorizontalMove, 0, VerticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        CamDirection();

        MovePlayer = playerInput.x * CamRight + playerInput.z * CamForward;

        MovePlayer = MovePlayer * PlayerSpeed;

        player.transform.LookAt(player.transform.position + MovePlayer);

        SetGravity();

        PlayerSkills();

        player.Move(MovePlayer * Time.deltaTime);

        Debug.Log(player.velocity.magnitude);
    }

    //Funcion de habilidad del jugador
    private void PlayerSkills()
    {
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            FallVelocity = JumpForce;
            MovePlayer.y = FallVelocity;
        }
    }

    //Funcion para determinar la direcciond de la camara
    void CamDirection()
    {
        CamForward = MainCam.transform.forward;
        CamRight = MainCam.transform.right;

        CamForward.y = 0;
        CamRight.y = 0;

        CamForward = CamForward.normalized;
        CamRight = CamRight.normalized;
    }

    //Funcion de gravedad y aceleracion
    void SetGravity()
    {
        if (player.isGrounded)
        {
            FallVelocity = -gravity * Time.deltaTime;
            MovePlayer.y = FallVelocity;
        }else
        {
            FallVelocity -= gravity * Time.deltaTime;
            MovePlayer.y = FallVelocity;
        }

    }
}
