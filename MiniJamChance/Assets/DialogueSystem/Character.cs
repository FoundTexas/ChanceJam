using UnityEngine;

[CreateAssetMenu(fileName = "new Character", menuName = "Character")]

public class Character : ScriptableObject
{
    public string fullName;
    public Sprite[] portait;
    public AudioClip[] voice;
}
