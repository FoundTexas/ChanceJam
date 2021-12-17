using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Dialogos
{
    [TextArea(3, 8)]
    public string[] textos;
}

public class ChangeText : MonoBehaviour
{
    
    public Dialogos[] dialogo;

    public Text texto;
    //[TextArea(3,8)]
    //public List<string> textos;
    private void Start()
    {
    
    }
    public void ChangeTexto(int idTexto)
    {
        //texto.text = textos[idTexto].ToString();
        texto.text = dialogo[PlayerPrefs.GetInt("lang")].textos[idTexto];
    }
}
