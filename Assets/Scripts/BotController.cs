using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public GameObject bot;
    int maxGlassware = 5;
    int currentGlassware;
    int glassesCleared = 0;
    int maxTrays = 8;
    int currentTrays;
    int traysCleared = 0;
    int currentPoop;
    int poopCleared = 0;

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
        currentPoop = 0;
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
                Interactable interactable = interactableObject.GetComponent<Interactable>();
                interactable.Interact(bot);
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

    // Returns whether the change in glassware succeeded or not.
    public bool ChangeGlassware(int amount)
    {
        if (currentPoop > 0)
        {
            Debug.Log("Drop off poop before picking up glassware");
            return false;
        }
        else if (currentGlassware == maxGlassware)
        {
            Debug.Log("Already carrying max glasses");
            return false;
        }
        else
        {
            currentGlassware = Mathf.Clamp(currentGlassware + amount, 0, maxGlassware);
            Debug.Log("Glassware: " + currentGlassware + "/" + maxGlassware);
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

    // Returns whether the change in trays succeeded or not.
    public bool ChangeTrays(int amount)
    {
        if (currentPoop > 0)
        {
            Debug.Log("Drop off poop before picking up tray");
            return false;
        }
        else if (currentTrays == maxTrays)
        {
            Debug.Log("Already carrying max trays");
            return false;
        }
        else
        {
            currentTrays = Mathf.Clamp(currentTrays + amount, 0, maxTrays);
            Debug.Log("Trays: " + currentTrays + "/" + maxTrays);
            return true;
        }
    }

    public bool ClearTrays()
    {
        if (currentTrays > 0)
        {
            traysCleared = traysCleared + currentTrays;
            currentTrays = 0;
            Debug.Log("Total trays cleared = " + traysCleared);
            return true;
        }
        return false;
    }

    // Returns whether the change in poop succeeded or not.
    public bool ChangePoop(int amount)
    {
        if (currentGlassware > 0 || currentTrays > 0)
        {
            Debug.Log("Drop off glassware/trays before picking up poop");
            return false;
        }
        else
        {
            currentPoop = currentPoop + amount;
            Debug.Log("Carrying poop: " + currentPoop);
            return true;
        }
    }

    public bool ClearPoop()
    {
        if (currentPoop > 0)
        {
            poopCleared = poopCleared + currentPoop;
            currentPoop = 0;
            Debug.Log("Total poop cleared = " + poopCleared);
            return true;
        }
        return false;
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
