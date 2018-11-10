using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class IntroDialogue : MonoBehaviour
{

    public TextMeshProUGUI text;
    public Button continueButton;
    public AudioClip typingSound;
    public float delay;

    [TextArea(3, 10)]
    public string[] sentences;

    private int index;
    private AudioSource typingSource;

    private void Start()
    {
        typingSource = GetComponent<AudioSource>();
        text.text = "";
        StartCoroutine(Type());
    }

    private void Update()
    {
        if (text.text == sentences[index])
        {
            continueButton.interactable = true; ;
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            text.text += letter;
            typingSource.PlayOneShot(typingSound, 1f);
            yield return new WaitForSeconds(Random.Range(delay - 0.06f, delay + 0.06f));

        }
    }


    public void NextSentence()
    {
        if (index == sentences.Length - 1)
        {
            SceneManager.LoadScene(2);
        }
        continueButton.interactable = false;
        if (index < sentences.Length - 1)
        {
            index++;
            text.text = "";
            StartCoroutine(Type());
        }
        else
        {
            text.text = "";
            continueButton.interactable = false;
        }
    }
}
