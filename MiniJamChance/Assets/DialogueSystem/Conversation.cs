using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public struct Line
{
    public Character character;
    public int ImageIndex;
    public int ImageIndexTwo;
    public int VoiceIndex;
    [TextArea(2, 5)]
    public string text;
}

[CreateAssetMenu(fileName = "new Conversation", menuName = "Conversation")]

public class Conversation : ScriptableObject
{
    public Character Left;
    public Character Right;
    public Line[] lines;
}
