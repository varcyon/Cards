using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TargetingOptions {
    NoTarget,
    AllCreaturesAndCharacters,
    AllCreatures,
    EnemyCreatures,
    YourCreatures,
    AllCharacters,
    EnemyCharacters,
    YourCharacter
}

public enum EffectTypes {
    Frost
}

public enum CardTypes {
    Spell,
    Creature
}
[CreateAssetMenu]
public class CardAsset : ScriptableObject
{
    [Header("General Info")]
    public CharacterAsset characterAsset; // if null card is neutral 
    [TextArea(3, 4)]
    public string Desctription;
    public int CastCost;
    public Sprite CardImage;
    public CardTypes CardType;

    [Header("Creature Info")]
    public int MaxHealth; // if its 0 its a spell card
    public int Attack;
    public int AttackPerTurn;
    public bool Charge;
    public string CreatureScriptName;
    public int specialCreatureAmount;

    [Header("Spell info")]
    public EffectTypes EffectType;
    public string SpellScriptName;
    public int specialSpellAmount;
    public TargetingOptions Targets;
}
