using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject mobileControls;
    public Joystick joystick;

    int maxGlassware = 5;
    int currentGlassware;
    int glassesCleared = 0;
    int maxTrays = 10;
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
    float speed = 3.2f;

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
        bool useMobileControls = OptionsControl.instance.GetMobileOptionValue();
        mobileControls.SetActive(useMobileControls);
        
        // Mobile Controls
        if (useMobileControls)
        {
            if (joystick.Horizontal >= 0.1f)
            {
                horizontal = joystick.Horizontal;
            }
            else if (joystick.Horizontal <= -0.1f)
            {
                horizontal = joystick.Horizontal;
            }
            else
            {
                horizontal = 0.0f;
            }

            if (joystick.Vertical >= 0.1f)
            {
                vertical = joystick.Vertical;
            }
            else if (joystick.Vertical <= -0.1f)
            {
                vertical = joystick.Vertical;
            }
            else
            {
                vertical = 0.0f;
            }
        }
        else
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }

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
            Interact();
        }
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void Interact()
    {
        if (!PauseControl.gameIsPaused && !GameManager.gameIsEnded)
        {
            // Shift the position up a little due to bottom pivot.
            Vector2 raycastOrigin = new Vector2(rigidbody2d.position.x, rigidbody2d.position.y + 0.6f);
            RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, lookDirection, 1.22f, LayerMask.GetMask("Interactable"));
            if (hit.collider != null)
            {
                GameObject interactableObject = hit.collider.gameObject;
                Debug.Log("Hit object " + interactableObject);
                Interactable interactable = interactableObject.GetComponent<Interactable>();
                interactable.Interact();
            }
        }
    }

    // Returns whether the change in glassware succeeded or not.
    // 1 if change in glassware succeeded.
    // 0 if no change in glassware occurs.
    // -1 if all glassware was dropped.
    public int ChangeGlassware(int amount)
    {
        if (currentPoop > 0)
        {
            AlertControl.instance.ShowAlert("Drop off poop before picking up glassware");
            return 0;
        }
        else if (currentGlassware >= maxGlassware)
        {
            // Logic to determine if glassware gets dropped.
            int percentChanceOfDropping = 6 + (9 * (currentGlassware - maxGlassware)) + Mathf.Clamp(currentTrays - 2, 0, maxTrays);
            Debug.Log("Percent chance of dropping: " + percentChanceOfDropping + "%");
            if (Random.Range(0, 100) < percentChanceOfDropping)
            {
                currentGlassware = 0;
                AlertControl.instance.ShowAlert("Dropped all glasses!");
                Debug.Log("Dropped! Glassware: " + currentGlassware + "/" + maxGlassware);
                UIManager.instance.SetBeerValue(0);
                return -1;
            }
            else
            {
                Debug.Log("Phew!");
                return PickUpGlassware(amount);
            }
        }
        else
        {
            return PickUpGlassware(amount);
        }
    }

    int PickUpGlassware(int amount)
    {
        currentGlassware += amount;
        Debug.Log("Glassware: " + currentGlassware + "/" + maxGlassware);
        UIManager.instance.SetBeerValue(currentGlassware);
        if (currentGlassware == maxGlassware)
        {
            AlertControl.instance.ShowAlert("Warning: Trying to carry more glasses may result in dropping them");
        }
        return 1;
    }

    public bool ClearGlassware()
    {
        if (currentPoop > 0)
        {
            AlertControl.instance.ShowAlert("Poop goes in the trash can (:");
            return false;
        }
        if (currentGlassware > 0)
        {
            ScoreControl.instance.IncrementGlassware(currentGlassware);
            glassesCleared = glassesCleared + currentGlassware;
            currentGlassware = 0;
            Debug.Log("Total glassware cleared = " + glassesCleared);
            UIManager.instance.SetBeerValue(0);
            return true;
        }
        return false;
    }

    // Returns whether the change in trays succeeded or not.
    public bool ChangeTrays(int amount)
    {
        if (currentPoop > 0)
        {
            AlertControl.instance.ShowAlert("Drop off poop before picking up tray");
            return false;
        }
        else if (currentTrays == maxTrays)
        {
            AlertControl.instance.ShowAlert("Already carrying max trays");
            return false;
        }
        else
        {
            currentTrays = Mathf.Clamp(currentTrays + amount, 0, maxTrays);
            Debug.Log("Trays: " + currentTrays + "/" + maxTrays);
            UIManager.instance.SetTrayValue(currentTrays);
            return true;
        }
    }

    public bool ClearTrays()
    {
        if (currentTrays > 0)
        {
            ScoreControl.instance.IncrementTrays(currentTrays);
            traysCleared = traysCleared + currentTrays;
            currentTrays = 0;
            Debug.Log("Total trays cleared = " + traysCleared);
            UIManager.instance.SetTrayValue(0);
            return true;
        }
        return false;
    }

    // Returns whether the change in poop succeeded or not.
    public bool ChangePoop(int amount)
    {
        if (currentGlassware > 0 || currentTrays > 0)
        {
            AlertControl.instance.ShowAlert("Drop off glassware & trays before picking up poop");
            return false;
        }
        else
        {
            currentPoop = currentPoop + amount;
            Debug.Log("Carrying poop: " + currentPoop);
            UIManager.instance.SetPoopValue(currentPoop);
            return true;
        }
    }

    public bool ClearPoop()
    {
        if (currentPoop > 0)
        {
            ScoreControl.instance.IncrementPoop(currentPoop);
            poopCleared = poopCleared + currentPoop;
            currentPoop = 0;
            Debug.Log("Total poop cleared = " + poopCleared);
            UIManager.instance.SetPoopValue(0);
            return true;
        }
        return false;
    }

    public void ResetAllInteractables()
    {
        currentGlassware = 0;
        currentTrays = 0;
        currentPoop = 0;
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
