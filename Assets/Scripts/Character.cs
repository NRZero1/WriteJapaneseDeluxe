using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "New Character")]
public class Character : ScriptableObject
{
    public string hiraganaName;
    public Sprite illustration;
}