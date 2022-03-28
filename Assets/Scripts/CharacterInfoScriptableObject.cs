using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Character", menuName = "Character Info Scriptable")]
public class CharacterInfoScriptableObject : ScriptableObject
{
    public string characterName;
    public string description;
    public int maxPV;
    public int valueAttaque;
    public Sprite profilPicture;
    public Ability ability0;
    public Ability ability1;
    public Ability ability2;
}