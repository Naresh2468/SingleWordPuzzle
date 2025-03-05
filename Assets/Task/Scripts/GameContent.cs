using UnityEngine;
[CreateAssetMenu(fileName ="GameContent", menuName = "Tool/GameContent")]
public class GameContent :ScriptableObject
{
    [TextArea(3, 10)]
    public string[] textStatements;
    public Sprite[] Contents;
}

