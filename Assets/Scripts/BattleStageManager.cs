using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleStageManager : MonoBehaviour {
    public static BattleStageManager i;
    public Transform handLoc;
    public BattleManagerSO BM;
    [SerializeField]
    private List<Transform> playerCreatureSlotLocs = new List<Transform>(6);
    [SerializeField]
    private List<Transform> enemyCreatureSlotLocs = new List<Transform>(6);
    public int numOfEnemyCreatures = 0;
    public int numOfEnemyCreaturesSet = 0;
    public int numOfPlayerCreatures = 0;
    public int numOfPlayerCreaturesSet = 0;

    public bool creaturesSet;
    public bool allEnemiesKilled;
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
        

        numOfEnemyCreatures = UnityEngine.Random.Range(1, 7);
        while (numOfEnemyCreaturesSet < numOfEnemyCreatures) {
            SetEnemyCreature();
            Debug.Log("e " +numOfEnemyCreaturesSet);
        }
        numOfPlayerCreatures = UnityEngine.Random.Range(1, 7);
        while (numOfPlayerCreaturesSet < numOfPlayerCreatures) {
            SetPlayerCreature();
            Debug.Log("p "+numOfPlayerCreaturesSet);
        }
        int targetedEnemy = FindEnemyTarget();
        Debug.Log("Targeted: "+targetedEnemy);

        int pCreatureToAttack = FindPlayerAttack();
        Debug.Log("Attacking: "+pCreatureToAttack);

        playerCreatureSlotLocs[pCreatureToAttack].GetChild(0).GetComponent<CardManager>().IsActive = true;
        Debug.Log(BM.playerCreatureSlots[pCreatureToAttack].GetComponent<CardManager>().cardAsset.name + " is ready to attack!");

        enemyCreatureSlotLocs[targetedEnemy].GetChild(0).GetComponent<CardManager>().IsTargeted = true;
        Debug.Log(BM.enemyCreatureSlots[targetedEnemy].GetComponent<CardManager>().cardAsset.name + " is being targeted!");
        // creaturesSet = true;
        if (creaturesSet) {
            RunBattle();
        }
    }


    private void RunBattle() {
        allEnemiesKilled = CheckIfThereAreEnemyCreatures();
        int attackingPC = 0;
        int attackingEC = 0;
        //while (!allEnemiesKilled) {
            FindEnemyTarget();
            FindPlayerAttack();
       // }


    }

    private int FindPlayerAttack() {
        int q = -1;
        for (int i = 0; i < BM.playerCreatureSlots.Count; i++) {
            if (BM.playerCreatureSlots[i] != null) {
                q = i;
                break;
            }
        }
        return q;


    }

    private int FindEnemyTarget() {
        int index = 0;
        do {
            index = UnityEngine.Random.Range(0, BM.enemyCreatureSlots.Count);

        } while (BM.enemyCreatureSlots[index] == null);
        return index;
        /*
        //randomlly check a slot enemy slot 
       Debug.Log("Random Select index: " + index);
        GameObject e = BM.enemyCreatureSlots[index];
        //check if its null
        if (e == null) {
            //if its not
            FindEnemyTarget();
        } else {
        return index;
        }
        return -1;
        */
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
            BM.enemyCreatureSlots[slot] = Instantiate( BM.creatureCard);
            BM.enemyCreatureSlots[slot].GetComponent<CardManager>().cardAsset = BM.Creatures[randomCreature];
            BM.enemyCreatureSlots[slot].GetComponent<CardManager>().SetCard();
            BM.enemyCreatureSlots[slot].GetComponent<CardManager>().cardSlot = slot;
            BM.enemyCreatureSlots[slot].GetComponent<CardManager>().Inplay = true ;
            Instantiate(BM.enemyCreatureSlots[slot], enemyCreatureSlotLocs[slot]);
            numOfEnemyCreaturesSet++;
        }
    }


    private void SetPlayerCreature() {
        int slot = UnityEngine.Random.Range(0, 6);
        if (BM.playerCreatureSlots[slot] == null) {
            int randomCreature = UnityEngine.Random.Range(0, BM.Creatures.Count);
            BM.playerCreatureSlots[slot] = Instantiate( BM.creatureCard);
            BM.playerCreatureSlots[slot].GetComponent<CardManager>().cardAsset = BM.Creatures[randomCreature];
            BM.playerCreatureSlots[slot].GetComponent<CardManager>().SetCard();
            BM.playerCreatureSlots[slot].GetComponent<CardManager>().cardSlot = slot;
            BM.playerCreatureSlots[slot].GetComponent<CardManager>().Inplay = true;
            BM.playerCreatureSlots[slot].GetComponent<CardManager>().IsMine = true;
            BM.playerCreatureSlots[slot].GetComponent<CardManager>().CanAttack = true;
            Instantiate(BM.playerCreatureSlots[slot], playerCreatureSlotLocs[slot]);
            numOfPlayerCreaturesSet++;

        }
    }

}
