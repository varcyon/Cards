using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class BattleManagerSO : ScriptableObject
{
    public int playerHealth = 30;
    
    public List<CardAsset> playerDeck = new List<CardAsset>();
    public List<CardAsset> playerHand = new List<CardAsset>();

    public List<CardAsset> playerCreatureSlots = new List<CardAsset>(6);
    public List<CardAsset> enemyCreatureSlots = new List<CardAsset>(6);

    public List<CardAsset> enemyCreatures = new List<CardAsset>();

    public GameObject creatureCard;
    public GameObject card;


}
