using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeakerUI : MonoBehaviour
{
    public bool writing = false;
    bool inside;
    public Image Portrait;
    public Text FullName;
    public Text dialog;

    //public float delay = 0.1f;
    private string currentText = "";

    private Character speaker;

    public int IndexPortait;
    public int IndexPortaitTwo;

    public Character Speaker
    {
        get { return speaker; }
        set {
            speaker = value;
            FullName.text = speaker.fullName;
        }
    }
    public string Dialogue;

    public AudioSource audio;
    public AudioClip clip;
    //{
        //set {dialog.text = value; }
    //}

    public bool HasSpeaker()
    {
        return speaker != null; 
    }

    public bool SpeakerIs(Character character)
    {
        return speaker == character;
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        Portrait.sprite = null;
        gameObject.SetActive(false);
    }

    public void ConversationText(int index, int indexTwo, AudioClip a)
    {
        writing = true;
        inside = false;
        Debug.Log("Start D");
        audio.clip = a;
        IndexPortait = index;
        Portrait.sprite = speaker.portait[IndexPortait];
        IndexPortaitTwo = indexTwo;
        StartCoroutine(ShowText());
    }

    public void Update()
    {
        if (writing)
        {
            if (inside)
            {
                if (Input.GetKeyDown("space"))
                {
                    Debug.Log("pp");
                    inside = false;
                    writing = false;
                    //StopCoroutine(ShowText());
                }
            }
        }

    }

    IEnumerator ShowText()
    {
        writing = true;
        currentText = "";
        for (int i = 0; i < Dialogue.Length; i++)
        {
            if (writing)
            {
                
                currentText += Dialogue.Substring(i, 1);
                dialog.GetComponent<Text>().text = currentText;


                if (Dialogue.Substring(i, 1) == "," || Dialogue.Substring(i, 1) == "!" || Dialogue.Substring(i, 1) == "?")
                {

                    yield return new WaitForSeconds(0.2f);
                    //audio.Stop();
                }
                else if (Dialogue.Substring(i, 1) == " ")
                {
                    //audio.Play();
                    yield return new WaitForSeconds(0.07f);
                    //audio.Stop();
                }
                else
                {
                    audio.Play();
                    yield return new WaitForSeconds(0.07f);
                    audio.Stop();
                    inside = true;
                }
            }
            else if (!writing)
            {

                FindObjectOfType<DialogDisplay>().canComt();
                currentText = Dialogue;
                dialog.GetComponent<Text>().text = currentText;

                Debug.Log("interupt");
                break;
            }
        }

        inside = false;

        Debug.Log("end");
        FindObjectOfType<DialogDisplay>().canComt();

        LastImage();

    }

    void LastImage()
	{
        writing = true;
        inside = false;
        //Portrait.sprite = speaker.portait[IndexPortait];
        //yield return new WaitForSeconds(1f);
        Portrait.sprite = speaker.portait[IndexPortaitTwo];
	}


}
