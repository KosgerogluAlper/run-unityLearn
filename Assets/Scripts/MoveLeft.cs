using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    
    private PlayerController playerController;

   readonly private float leftBound = -15;

    [SerializeField] private float speed = 30;
    public float Speed { get { return speed; } set { speed = value; } }

    void Start()
    {
        playerController= GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
       
        if (playerController.gameOver == false)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.left);
        }
        if(transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }


    }
}
