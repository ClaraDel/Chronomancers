using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Character", menuName = "Character Info")]
public class CharacterInfoScriptable : ScriptableObject
{
    public string characterName;
    public string description;
    public int maxPV;
    public int valueAttaque;
    public Image profilPicture;
    public Ability ability0;
    public Ability ability1;
    public Ability ability2;
}