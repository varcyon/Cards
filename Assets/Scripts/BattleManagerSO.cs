using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class BattleManagerSO : ScriptableObject
{
    public int playerHealth = 30;
    public int startingPlayerHealth = 30;
    public List<CardAsset> playerDeck = new List<CardAsset>();
    public List<CardAsset> playerHand = new List<CardAsset>();

    public List<GameObject> playerCreatureSlots = new List<GameObject>(6);
    public List<GameObject> enemyCreatureSlots = new List<GameObject>(6);

    public List<CardAsset> Creatures = new List<CardAsset>();

    public GameObject creatureCard;
    public GameObject card;

    public void PlayerTakeDamage(int amount) {
        playerHealth -= amount;
    }
}
