using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;

    public AudioSource audioTest;

    bool talk = false;
    bool next = false;

    [SerializeField] GameObject indicator;

    private void Start()
    {
        indicator.GetComponent<GameObject>();
        indicator.SetActive(false);
        audioTest = GetComponent<AudioSource>();
        audioTest.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            talk = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            next = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        talk = false;
        if (collision.CompareTag("Player"))
        {
            indicator.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (talk)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                talk = false;
            }
            if (next)
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
                next = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        indicator.SetActive(false);
        talk = false;
        next = false;
    }
}
