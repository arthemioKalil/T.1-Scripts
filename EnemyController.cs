using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 50;
    private Color damageColor = Color.white;
    private Renderer rend;
    private bool damageTaked;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject, 0.3f);
        }

        if(damageTaked)
        {
        rend.material.color = Color.Lerp(rend.material.color, damageColor, Time.deltaTime*0.5f);
        health -= playerController.damagePlayer;
        Debug.Log("Oh nao.");
        }

    }

    public void TakeDamage()
    {

        damageTaked = true;
    }
}
