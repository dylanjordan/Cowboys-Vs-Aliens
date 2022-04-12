using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private Queue<string> sentences;

    public Text nameText;
    public Text npcDialogue;

    public AudioClip openAudio;
    public AudioClip closeAudio;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }


    public void StartDialogue(Dialogue dialogue)
    {
        FindObjectOfType<PlayerMovement>().SetCanMove(false);

        animator.SetBool("IsOpen", true);


        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
        PlaySound(openAudio);
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            PlaySound(closeAudio);
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        PlaySound(openAudio);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        npcDialogue.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            npcDialogue.text += letter;
            yield return new WaitForSeconds(0.025f);
        }
    }

    void EndDialogue()
    {
        FindObjectOfType<PlayerMovement>().SetCanMove(true);
        animator.SetBool("IsOpen", false);
    }

    public void PlaySound(AudioClip noise)
    {
        AudioSource.PlayClipAtPoint(noise, transform.position);
    }
}
