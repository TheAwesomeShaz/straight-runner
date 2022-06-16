using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    bool isAlive;
    Animator anim;
    Rigidbody rb;

    public static Player instance;
    float _xAxis;
    [SerializeField] bool hasReachedEnd;
    [SerializeField] bool hasStartedAttack;

    [SerializeField] bool isRunning;
    [SerializeField] float leftRightSpeed;
    [SerializeField] float runSpeed;

    private void Awake()
    {
        instance = this;
        isAlive = true;
    }

    private void Start()
    {
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInput();
        if (hasReachedEnd && Input.GetMouseButtonDown(0))
        {
            hasStartedAttack = true;
        }
    }

    private void FixedUpdate()
    {
        HandleRunning();
    }

    void HandleRunning()
    {
        //Start running on Tap
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && (!isRunning && !hasReachedEnd))
        {
            //anim.SetBool("Running", true);    
            isRunning = true;
            return;
        }

        //Apply Forward force
        if (isRunning)
        {
            PlayerMovement();
        }
    }

    void HandleInput()
    {
        float h = Input.GetAxis("Horizontal");

        _xAxis = h;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                _xAxis = Mathf.Clamp(touch.deltaPosition.x / 30f, -5f, 5f);
            }
            else
            {
                _xAxis = 0f;
            }
        }
    }

    private void PlayerMovement()
    {
        Vector3 movePosition = new Vector3(_xAxis * leftRightSpeed * Time.deltaTime, 0f, 1f * runSpeed * Time.deltaTime);

        if (transform.position.x <= -.9f && movePosition.x < 0)
            movePosition.x = 0f;
        else if (transform.position.x >= .9f && movePosition.x > 0)
            movePosition.x = 0f;

        rb.velocity = movePosition * leftRightSpeed * Time.deltaTime;
    }

}