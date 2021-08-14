using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody2D rb;

    [Header("Rewired Settings")]
    [SerializeField] private int playerID = 0;
    [SerializeField] private Player player;

    [Header("Movements Settings")]
    private float speed = 7;
    private float jumpForce = 15;
    private float jumpInput;
    public float moveInputX;
    public float moveInputY;
    public bool isGrouded;
    public Transform feetPos;
    private float checkRadius = 0.5f;
    public LayerMask WIsGround;
    private float jumpTimeCounter;
    private float jumpTime = 0.2f;
    private bool isJumping;

    [Header("Player DoubleJump")]

    private int doubleJump;
    private int doubleJumpValue = 1;

    [Header("Player Dash")]
    private float dashSpeed = 55;
    private float dashTime;
    private float startDashTime = .1f;
    private int direction;
    private bool dashConnect;
    public bool dashC;
    private int dashActive;
    public bool dashEnable;
    private bool dashDirection;
    public bool dashMovement;
    private int dashAir;
    private bool dashLimit = true;

    [Header("Player Caixa")]
    public bool transformBox;

    [Header("Player Attack")]
    private float timeToAttack;
    [SerializeField] private float startTimeToAttack = 0.3f;
    public Transform attackPos;
    [SerializeField] private float attackRange = .74f;
    public LayerMask damageMask;
    public int damagePlayer = 5;
    private bool attacked;
    private bool attackTimer;
    private bool attackStart;

    [Header("Player Animations")]
    public bool controlDad = true;
    private PlayerAnimations playerAnimations;

    void Awake()
    {
        doubleJump = doubleJumpValue;

        

    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimations = GetComponent<PlayerAnimations>();

        dashTime = startDashTime;
        dashC = false;
        dashEnable = true;
        dashAir = 1;
        dashLimit = false;
        transformBox = false;
        //especificando para o rewired pegar o player 0 onde possui nossos controles
        player = ReInput.players.GetPlayer(playerID);
    }

    void Update()
    {
        // moveInput = input.Player.Movement.ReadValue<Vector2>(); //new input system example
        // jumpInput = input.Player.Jump.ReadValue<float>();

        moveInputX = player.GetAxis("Move Horizontal");
        moveInputY = player.GetAxis("Move Vertical");
        
        rb.velocity = new Vector2(moveInputX * speed, rb.velocity.y);

        isGrouded = Physics2D.OverlapCircle(feetPos.position, checkRadius, WIsGround);

        onJump();
        

        if (!isGrouded && dashAir > 0 && dashTime > 0)
        {
            dashEnable = true;
            dashAir--;
        }
        if (!isGrouded && dashAir == 0)
        {
            dashEnable = false;
        }
        if (isGrouded)
        {
            dashAir = 1;
            dashLimit = false;
        }

        OnDash();

        // switch (playerAnimations.startBool)
        // {
        //     case true:
        //         playerAnimations.StartGame();
        //         break;

        //     case false:
        //         controlDad = true;
        //         break;

        // }

        if (isGrouded && !isJumping)
        {
            doubleJump = doubleJumpValue;
        }

        //flip
        if (moveInputX > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveInputX < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (dashTime <= 0)
        {
            StartCoroutine(DashTimer(dashTime));
        }

        IEnumerator DashTimer(float dashTime)
        {
            yield return new WaitForSeconds(dashTime + 1f);
            dashEnable = true;
        
        }

        // if(attacked)
        // {
        //     Collider2D[] EnemiesDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, damageMask);
        //         for (int i = 0; i < EnemiesDamage.Length; i++)
        //         {
        //             EnemiesDamage[i].GetComponent<EnemyController>().TakeDamage();
        //         }
        // }
        // if(attackTimer)
        // {
        //     timeToAttack -= Time.deltaTime;
        // }
        // if(attackStart)
        // {
        //     timeToAttack = startTimeToAttack;
        // }
        // if(timeToAttack > 0)
        // {
        //     attacked = false;
        //     attackTimer = false;
        //     attackStart = false;
        // }

        TeleportPlayer();
    }

    public void OnDash()
    {

        if (controlDad)
        {
            if (dashEnable)
            {

                if (direction == 0)
                {
                    // input.Player.Dash.started += ctx =>
                    // {
                    //     if (moveInput.x < 0)
                    //     {
                    //         direction = 1;
                    //     }
                    //     else if (moveInput.x > 0)
                    //     {
                    //         direction = 2;
                    //     }
                    // };

                }
                else
                {
                    if (dashTime <= 0)
                    {
                    direction = 0;
                    dashTime = startDashTime;
                    rb.velocity = Vector2.zero;
                    dashEnable = false;
                    }
                    else
                    {

                        dashTime -= Time.deltaTime;

                        if (direction == 1)
                        {
                            rb.velocity = new Vector2(-dashSpeed, rb.velocity.y);

                        }
                        else if (direction == 2)
                        {
                            rb.velocity = new Vector2(dashSpeed, rb.velocity.y);

                        }

                    }
                }

            }
        }

    }

    public void onJump()
    {
        // player.GetButtonDown("Jump")
        if (controlDad)
        {
                    Debug.Log(jumpTimeCounter);
                    
            if (isGrouded) //getkeydown wasPressedThisFrame ctx.started
            {
            //     input.Player.Jump.started += ctx =>
            // {
            //     jumpTimeCounter = jumpTime;
            //     rb.velocity = Vector2.up * (jumpForce + 5);
            //     // rb.velocity = new Vector2(moveInput.x * speed + 2, rb.velocity.y);
            // };


            }

            if (isJumping) //getkey isPressed ctx.performed
            {
            //     input.Player.Jump.performed += ctx =>
            // {
            //     if (jumpTimeCounter > 0)
            //     {
            //         rb.velocity = Vector2.up * (jumpForce - 3);
            //         jumpTimeCounter -= Time.deltaTime;
            //     }
            //     else
            //     {
            //         isJumping = false;
            //     }
            // };
            }

            //getkeyup wasReleasedThisFrame ctx.canceled  
        //     input.Player.Jump.canceled += ctx =>
        //    {
        //        isJumping = false;

        //        if (rb.velocity.y >= 1)
        //        {
        //            rb.velocity -= rb.velocity;
        //        }
        //    };

            // if (doubleJump > 0 && !isGrouded)
            // {
            //     input.Player.Jump.started += ctx =>
            // {
            //     rb.velocity = Vector2.up * (30 / 2.3f);
            //     doubleJump--;
            // };
            // }
        }

    }

    // public void OnAttack(InputAction.CallbackContext ctx)
    // {
    //     if(controlDad)
    //     {
    //     if(timeToAttack <= 0)
    //     {
    //         if(ctx.started)
    //         {
    //             attacked = true;
    //         }
    //         if(ctx.canceled)
    //         {
    //             attacked = false;
    //         }

    //         attackStart = true;
    //     }
    //     else
    //     {
    //         attackTimer = true;
    //     }
    // }
    // }
    // public void OnBox(InputAction.CallbackContext ctx)
    // {
    //     if(controlDad)
    //     {
    //         transformBox = true;
    //         playerAnimations.PlayerBox();
    //     }
    // }

    void TeleportPlayer()
    {
        if (transform.position.y <= -50)
        {
            Debug.Log(transform.position.y);
            transform.position = new Vector2(0, 0);
        }
    }


    // public void StopGame()
    // {
    //     controlDad = false;
    // }

    //     void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(attackPos.position, attackRange);
    // }

}

