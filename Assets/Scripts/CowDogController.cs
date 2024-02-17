using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowDogController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2d;
    bool napping = false;
    int direction = 1;
    float speed = 1.0f;
    float changeTime = 3.0f;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            bool takeNap = Random.Range(0, 10) < 5;
            timer = takeNap ? changeTime * 2 : changeTime;
            napping = takeNap;
            if (!napping)
            {
                direction = -direction;
            }
        }
    }

    void FixedUpdate()
    {
        if (napping)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", 1);
            return;
        }
        Vector2 position = rigidbody2d.position;

        position.x = position.x + speed * direction * Time.deltaTime;
        animator.SetFloat("Move X", direction);
        animator.SetFloat("Move Y", 0);

        rigidbody2d.MovePosition(position);
    }
}
