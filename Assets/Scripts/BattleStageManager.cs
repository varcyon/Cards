using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum Turn {
    Player,
    Enemy
}
public class BattleStageManager : MonoBehaviour {
    public static BattleStageManager i;
    public Transform handLoc;
    public BattleManagerSO BM;
    [SerializeField]
    private List<Transform> playerCreatureSlotLocs = new List<Transform>(6);
    [SerializeField]
    private List<Transform> enemyCreatureSlotLocs = new List<Transform>(6);

    public TMP_Text playerHealth;

    public int numOfEnemyCreatures = 0;
    public int numOfEnemyCreaturesSet = 0;
    public int numOfPlayerCreatures = 0;
    public int numOfPlayerCreaturesSet = 0;

    public bool creaturesSet;
    public bool allEnemiesKilled;

    public Turn combatTurn = Turn.Player;
    private void Awake() {
        if (i != this) {
            i = this;
        }

    }

    private void Start() {
        for (int i = 0; i < 6; i++) {
            BM.enemyCreatureSlots[i] = null;
            BM.playerCreatureSlots[i] = null;
        }
        BM.playerHealth = BM.startingPlayerHealth;
        playerHealth.text = BM.playerHealth.ToString();

        numOfEnemyCreatures = UnityEngine.Random.Range(1, 7);
        while (numOfEnemyCreaturesSet < numOfEnemyCreatures) {
            SetEnemyCreature();
        }
        numOfPlayerCreatures = UnityEngine.Random.Range(1, 7);
        while (numOfPlayerCreaturesSet < numOfPlayerCreatures) {
            SetPlayerCreature();
        }
        creaturesSet = true;
        if (creaturesSet) {
            StartCoroutine(RunBattle());
        }
    }


    private IEnumerator RunBattle() {
        // allEnemiesKilled = CheckIfThereAreEnemyCreatures();
        int target;
        int attacker;

        while (AnyAttackers(BM.playerCreatureSlots) && AnyDefenders(BM.enemyCreatureSlots)|| AnyAttackers(BM.enemyCreatureSlots) && AnyDefenders(BM.playerCreatureSlots) || AnyAttackers(BM.enemyCreatureSlots)) {

            if (combatTurn == Turn.Player) {
               // Debug.Log("Is Player Turn");
                if (AnyAttackers(BM.playerCreatureSlots) && AnyDefenders(BM.enemyCreatureSlots)) {

                    target = FindTarget(BM.enemyCreatureSlots);
                    attacker = FindAttacker(BM.playerCreatureSlots);

                    CardManager attackingCreature = BM.playerCreatureSlots[attacker].GetComponent<CardManager>();
                    CardManager defendingCreature = BM.enemyCreatureSlots[target].GetComponent<CardManager>();

                    attackingCreature.IsActive = true;
                    defendingCreature.IsTargeted = true;
                    print(attackingCreature.cardName.text + " is attacking " + defendingCreature.cardName.text);
                    yield return new WaitForSecondsRealtime(2f);

                    print("PLAYERS: " + defendingCreature.cardName.text + " takes " + attackingCreature.currentAttack + " damage.");
                    print("ENEMIES: " + attackingCreature.cardName.text + " takes " + defendingCreature.currentAttack + " damage.");
                    attackingCreature.TakeDamage(defendingCreature.currentAttack);
                    defendingCreature.TakeDamage(attackingCreature.currentAttack);

                    attackingCreature.CanAttack = false;
                    attackingCreature.IsActive = false;
                    defendingCreature.IsTargeted = false;

                }
                combatTurn = Turn.Enemy;
                yield return new WaitForSecondsRealtime(2f);
            }

            if (combatTurn == Turn.Enemy) {
               // Debug.Log("Is Enemy Turn");
                if (AnyAttackers(BM.enemyCreatureSlots) && AnyDefenders(BM.playerCreatureSlots)) {

                    target = FindTarget(BM.playerCreatureSlots);
                    attacker = FindAttacker(BM.enemyCreatureSlots);

                    CardManager attackingCreature = BM.enemyCreatureSlots[attacker].GetComponent<CardManager>();
                    CardManager defendingCreature = BM.playerCreatureSlots[target].GetComponent<CardManager>();

                    attackingCreature.IsActive = true;
                    defendingCreature.IsTargeted = true;
                    print(attackingCreature.cardName.text + " is attacking " + defendingCreature.cardName.text);
                    yield return new WaitForSecondsRealtime(2f);

                    print("PLAYERS: " + defendingCreature.cardName.text + " takes " + attackingCreature.currentAttack + " damage.");
                    print("ENEMIES: " + attackingCreature.cardName.text + " takes " + defendingCreature.currentAttack + " damage.");
                    attackingCreature.TakeDamage(defendingCreature.currentAttack);
                    defendingCreature.TakeDamage(attackingCreature.currentAttack);

                    attackingCreature.CanAttack = false;
                    attackingCreature.IsActive = false;
                    defendingCreature.IsTargeted = false;
                } else
                if (AnyAttackers(BM.enemyCreatureSlots) && !AnyDefenders(BM.playerCreatureSlots)) {
                    attacker = FindAttacker(BM.enemyCreatureSlots);
                    CardManager attackingCreature = BM.enemyCreatureSlots[attacker].GetComponent<CardManager>();
                    print("ATTACK PLAYER");
                    BM.PlayerTakeDamage(attackingCreature.currentAttack);
                    print(attackingCreature.cardName.text + " deals " + attackingCreature.currentAttack + " damage to the player");
                    playerHealth.text = BM.playerHealth.ToString();
                    
                    attackingCreature.CanAttack = false;

                }

                combatTurn = Turn.Player;

                yield return new WaitForSecondsRealtime(2f);
            }
        }
        //   Debug.Log(AnyAttackers(BM.playerCreatureSlots) + ":" + AnyDefenders(BM.enemyCreatureSlots));
        // Debug.Log(AnyAttackers(BM.enemyCreatureSlots) + ":" + AnyDefenders(BM.playerCreatureSlots));

        print("No more battles to be had");
    }
    //checks to see if any creatures in the list can attack ( CanAttack = true )
    private bool AnyAttackers(List<GameObject> creatureList) {
       // Debug.Log("Checking Attackers");
        bool attackersLeft = false;
        foreach (GameObject creature in creatureList) {
            if (creature != null && creature.GetComponent<CardManager>().IsAlive && creature.GetComponent<CardManager>().CanAttack) {
                attackersLeft = true;
                break;
            }
        }
     //   Debug.Log(attackersLeft);
        return attackersLeft;
    }
    private bool AnyDefenders(List<GameObject> creatureList) {
       // Debug.Log("Checking Defenders");
        bool defendersLeft = false;
        foreach (GameObject creature in creatureList) {
            if (creature != null && creature.GetComponent<CardManager>().IsAlive) {
                defendersLeft = true;
                break;
            }
        }
      //  Debug.Log(defendersLeft);
        return defendersLeft;
    }
    private int FindAttacker(List<GameObject> creatureList) {
        int q = -1;
        for (int i = 0; i < creatureList.Count; i++) {
            if (creatureList[i] != null && creatureList[i].GetComponent<CardManager>().IsAlive && creatureList[i].GetComponent<CardManager>().CanAttack) {
                q = i;
                break;
            }
        }
        return q;


    }

    private int FindTarget(List<GameObject> creatureList) {
        int index = 0;
        do {
            index = UnityEngine.Random.Range(0, creatureList.Count);

        } while (creatureList[index] == null || !creatureList[index].GetComponent<CardManager>().IsAlive);
        return index;
    }

    private bool CheckIfThereAreEnemyCreatures() {
        bool a = false;
        foreach (GameObject item in BM.enemyCreatureSlots) {
            if (item == null) {
                a = true;
            } else {
                a = false;
                break;
            }
        }
        return a;
    }

    private void SetEnemyCreature() {
        int slot = UnityEngine.Random.Range(0, 6);
        if (BM.enemyCreatureSlots[slot] == null) {
            int randomCreature = UnityEngine.Random.Range(0, BM.Creatures.Count);
            GameObject eGo = Instantiate(BM.creatureCard, enemyCreatureSlotLocs[slot]);
            CardManager eCM = eGo.GetComponent<CardManager>();
            eCM.cardAsset = BM.Creatures[randomCreature];
            eCM.SetCard();
            eCM.cardSlot = slot;
            eCM.Inplay = true;
            eCM.CanAttack = true;
            eCM.IsAlive = true;
            BM.enemyCreatureSlots[slot] = eGo;
            numOfEnemyCreaturesSet++;
        }
    }


    private void SetPlayerCreature() {
        int slot = UnityEngine.Random.Range(0, 6);
        if (BM.playerCreatureSlots[slot] == null) {
            int randomCreature = UnityEngine.Random.Range(0, BM.Creatures.Count);
            GameObject pGo = Instantiate(BM.creatureCard, playerCreatureSlotLocs[slot]);
            CardManager pCM = pGo.GetComponent<CardManager>();
            pCM.cardAsset = BM.Creatures[randomCreature];
            pCM.SetCard();
            pCM.cardSlot = slot;
            pCM.Inplay = true;
            pCM.IsMine = true;
            pCM.CanAttack = true;
            pCM.IsAlive = true;
            BM.playerCreatureSlots[slot] = pGo;
            numOfPlayerCreaturesSet++;

        }
    }

}
