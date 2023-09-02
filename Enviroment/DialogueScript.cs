using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] private bool PlayerCanStartDialog;

    // Components
    [Header("Components")]
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private Text dialogText;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject passTextButton;
    private ComponentManager componentManager;

    // Dialog
    [Header("Dialog")]
    public int currentDialogue;
    public string[] dialog;
    public float typeSpeed;

    // Timer
    [Header("Timer")]
    [SerializeField] private bool timerActive;
    [SerializeField] private float timer;
    [SerializeField] private float textPassDelay;
    [SerializeField] private bool passText;
    private float startNewTextDelay;
    private float TextDelay;


    void Awake()
    {
        // Components
        componentManager = GameObject.FindGameObjectWithTag("Component Manager").GetComponent<ComponentManager>();
        player = componentManager.player;
        dialogBox = componentManager.dialogBox;
        dialogText = componentManager.dialogBox.transform.Find("Text").GetComponent<Text>();
        passTextButton = componentManager.passTextButton;

        // Start Values
        currentDialogue = 0;
        passText = true;
        timerActive = false;
        startNewTextDelay = 0.2f;


        // -------- Defensive -------- 
        if (textPassDelay == 0)
        {
            textPassDelay = 0.4f;
        }
        if (typeSpeed == 0)
        {
            typeSpeed = 0.02f;
        }
        if (dialog.Length == 0)
        {
            dialog = new string[] {"No Text Applied"};
        }
        // --------------------------- 
    }

    void Update()
    {
        // Timer Check
        if (timer > TextDelay)
        {
            passText = true;
            timer = 0;
            timerActive = false;

            // Active Pass Text Button
            if (dialogBox.activeInHierarchy)
            {
                passTextButton.SetActive(true);
            }
        }

        // If Press Space
        if (Input.GetAxisRaw("Interect") != 0 && PlayerCanStartDialog && passText)
        {
            // Start Timer
            passTextButton.SetActive(false);
            timerActive = true;
            passText = false;

            // If Dialogue Box is Active
            if (dialogBox.activeInHierarchy)
            {
                currentDialogue++;

                // Deactive Dialogue Box
                if (currentDialogue >= dialog.Length)
                {
                    // Close Dialogue Box
                    dialogBox.SetActive(false);
                    passTextButton.SetActive(false);
                    currentDialogue = 0;

                    // Player can Move
                    PlayerBehaviour playerB = player.GetComponent<PlayerBehaviour>();
                    playerB.isPaused = false;
                    playerB.state = StateMachine.Idle;

                    // Timer Value Change
                    TextDelay = startNewTextDelay;
                }

                // Timer
                timer += Time.deltaTime;
                passText = false;

                // Write Text
                StartCoroutine(TypewriterEffectCo(dialog[currentDialogue]));
            }

            // If Dialogue Box is Not Active
            else
            {
                // Active Dialog Box
                dialogBox.SetActive(true);

                // Write Text
                StartCoroutine(TypewriterEffectCo(dialog[currentDialogue]));

                // Player cant Move
                PlayerBehaviour playerB = player.GetComponent<PlayerBehaviour>();
                playerB.isPaused = true;
                playerB.state = StateMachine.Interect;
                playerB.myRigidbody.velocity = new Vector2(0f, 0f);
                playerB.myAnimator.SetBool("walking", false);

                // Timer Value Restart
                TextDelay = textPassDelay;

            }
        }
    }


    // --------- Timer ---------

    // Must be FixedUpdate to get real time in game

    // If timer is started, Count Time
    public void FixedUpdate()
    {
        if (timerActive == true)
        {
            timer += Time.deltaTime;
        }
    }


    // --------- Enter/Exit Trigger Area ---------

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Debug.Log("Player in Range");
            PlayerCanStartDialog = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Debug.Log("Player left Range");
            PlayerCanStartDialog = false;
            dialogBox.SetActive(false);
        }
    }


    // --- Typewriter Effect ---
    IEnumerator TypewriterEffectCo(string line)
    {
        dialogText.text = "";

        foreach (char letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
