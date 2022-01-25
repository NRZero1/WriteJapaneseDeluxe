using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Material", menuName = "Learning Material")]
public class Material : ScriptableObject
{
    public string romajiName;
    public string hiraganaName;
    public Sprite illustration;
    public string meaning;
}
