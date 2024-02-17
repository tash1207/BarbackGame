using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public GameObject bot;
    public int maxGlassware = 5;
    int currentGlassware;
    int glassesCleared = 0;

    AudioSource audioSource;
    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 lookDirection = new Vector2(0, -1);
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentGlassware = 0;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Shift the position up a little due to bottom pivot.
            Vector2 raycastOrigin = new Vector2(rigidbody2d.position.x, rigidbody2d.position.y + 0.6f);
            RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, lookDirection, 1.3f, LayerMask.GetMask("Interactable"));
            if (hit.collider != null)
            {
                GameObject interactableObject = hit.collider.gameObject;
                Debug.Log("Hit object " + interactableObject);
                InteractableController interactableController = interactableObject.GetComponent<InteractableController>();
                interactableController.Interact(bot);
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + 3.2f * horizontal * Time.deltaTime;
        position.y = position.y + 3.2f * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    // Returns whether tha change in glassware succeeded or not.
    public bool ChangeGlassware(int amount)
    {
        if (currentGlassware == maxGlassware)
        {
            Debug.Log("Already carrying max glasses");
            return false;
        }
        else
        {
            currentGlassware = Mathf.Clamp(currentGlassware + amount, 0, maxGlassware);
            Debug.Log(currentGlassware + "/" + maxGlassware);
            return true;
        }
    }

    public bool ClearGlassware()
    {
        if (currentGlassware > 0)
        {
            glassesCleared = glassesCleared + currentGlassware;
            currentGlassware = 0;
            Debug.Log("Total glassware cleared = " + glassesCleared);
            return true;
        }
        return false;
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
