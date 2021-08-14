using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Camera cam;
    private Animator animator;
    private Rigidbody2D rb;
    private PlayerController playerController;
    public bool startBool = true;
    public GameObject dashParticle;
    private GameObject dashParticlePrefab;
    float transformBoxDuraction;
    float duracao;
    private TrailRenderer dashTrail;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        dashTrail = GetComponent<TrailRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        switch (playerController.moveInputX == 0 && playerController.isGrouded)
        {
            case true:
                animator.SetBool("Stop", true);
                break;

            case false:
                animator.SetBool("Stop", false);
                break;

        }

        switch (playerController.isGrouded)
        {
            case true:
                animator.SetBool("isJumping", false);
                break;

            case false:
                animator.SetBool("isJumping", true);
                break;

        }

        switch (playerController.dashMovement)
        {
            case true:
                animator.SetBool("dash", true);
                StartCoroutine(DashMovement());
                dashParticlePrefab = (GameObject)Instantiate(dashParticle, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                Destroy(dashParticlePrefab, 0.15f);

                break;

            case false:
                animator.SetBool("dash", false);
                break;

        }

        if (playerController.dashC)
        {
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(Color.magenta, 0.0f), new GradientColorKey(Color.white, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(1, 0.0f), new GradientAlphaKey(0, 1.0f) }
            );
            dashTrail.colorGradient = gradient;
        }

        
        if(playerController.moveInputX == 0 && playerController.transformBox)
        {
        playerController.controlDad = false;
        // animator.SetBool("box", playerController.transformBox);
        }


    }

    // public void StartGame()
    // {
    //     playerController.StopGame();
    //     animator.Play("PlayerEye");
    //     duracao = animator.GetCurrentAnimatorStateInfo(0).length;
    //     StartCoroutine(StartDuracao(duracao));
    // }

    public void PlayerBox()
    {
        transformBoxDuraction = animator.GetCurrentAnimatorStateInfo(0).length;
        if(playerController.transformBox)
        {
            animator.SetTrigger("boxWake");
            animator.SetBool("box", false);
            StartCoroutine(BoxTransform(transformBoxDuraction));
        }
        

    }

    IEnumerator StartDuracao(float duracao)
    {
        yield return new WaitForSeconds(duracao + 1.7f);
        startBool = false;
    }

    IEnumerator DashMovement()
    {
        yield return new WaitForSeconds(.1f);

        playerController.dashMovement = false;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1, 0.0f), new GradientAlphaKey(0, 1.0f) }
        );
        dashTrail.colorGradient = gradient;
        
    }
    IEnumerator BoxTransform(float transformBoxDuraction)
    {
        yield return new WaitForSeconds(transformBoxDuraction + 5f);
        Debug.Log("ok2");
        playerController.controlDad = true;
        playerController.transformBox = false;
        animator.ResetTrigger("boxWake");
        animator.SetBool("box", true);
    }

}
