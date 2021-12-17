using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string Name;
    public Conversation[] conversation;

    public AudioClip found;
    AudioSource audios;

    public void Start()
    {
        //audios = GetComponent<AudioSource>();
        //audios.clip = found;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //audios.Play();
            FindObjectOfType<DialogDisplay>().StarDialogue(conversation[PlayerPrefs.GetInt("lang")]);

            Destroy(this.gameObject);
            //Destroy(this.gameObject);
        }
    }

}
