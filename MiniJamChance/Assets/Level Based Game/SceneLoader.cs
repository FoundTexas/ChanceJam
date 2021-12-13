using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    Animator anim;
    bool b = true;
    bool IsActive = false;


    public void Reload()
    {
        Load(SceneManager.GetActiveScene().buildIndex);
    }

    public void Load(int index)
    {

        anim = GameObject.FindGameObjectWithTag("Fader").GetComponent<Animator>();
        StartCoroutine(loadLevel(index));

    }

    IEnumerator loadLevel(int i)
    {
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        if (b)
        {
            b = false;
            SceneManager.LoadScene(i);
        }
    }
    public void GoTOScene(int i)
    {
        SceneManager.LoadScene(i);
    }
    public void ActiveDesactive(GameObject objectToAD)
    {
        IsActive = !IsActive;
        objectToAD.SetActive(IsActive);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Load(3);
        }
    }
}
