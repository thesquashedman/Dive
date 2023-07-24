using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : EntitySound
{
    //Own id to check inputs against
    private int myID;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        //Subscribe to events
        EventManager.current.onEnemyAttack += attack;
        EventManager.current.onEnemyAttackSuccess += atkDealt;
        EventManager.current.onDealDamageEnemy += takeDamage;
        EventManager.current.onEnemyDeath += die;

        //Set own id
        myID = gameObject.GetInstanceID();
    }

    private void Update() {
        //For demo purposes only
        /*
        if(Input.GetKeyDown(KeyCode.I)) {
            EventManager.current.EnemyAttack(myID);
        } else if(Input.GetKeyDown(KeyCode.O)) {
            EventManager.current.EnemyAtkDealt(myID);
        } else if(Input.GetKeyDown(KeyCode.P)) {
            EventManager.current.DealDamageEnemy(myID, 1f);
        } else if(Input.GetKeyDown(KeyCode.L)) {
            EventManager.current.EnemyDeath(myID);
        }
        */
    }

    //When onEnemyAttack event is triggered, plays the sound in position 0
    private void attack(int objectID) {
        if(objectID == myID) {
            audios[0].Play();
        }
    }

    //When onEnemyAttackSuccess event is triggered, plays the sound in position 1
    private void atkDealt(int objectID) {
        if(objectID == myID) {
            audios[1].Play();
        }
    }

    //When onDealDamageEnemy event is triggered, plays the sound in position 2
    private void takeDamage(int objectID, float damage) {
        if(objectID == myID) {
            audios[2].Stop();
            audios[2].Play();
        }
    }

    //When onEnemyDeath event is triggered, plays the sound in position 3 and
    // unsubscribes from events.
    private void die(int objectID) {
        if(objectID == myID) {
            audios[3].Play();
            EventManager.current.onEnemyAttack -= attack;
            EventManager.current.onEnemyAttackSuccess -= atkDealt;
            EventManager.current.onDealDamageEnemy -= takeDamage;
            EventManager.current.onEnemyDeath -= die;
        }
    }

    //Remove subscriptions when disabled
    private void OnDisable() {
        EventManager.current.onEnemyAttack -= attack;
        EventManager.current.onEnemyAttackSuccess -= atkDealt;
        EventManager.current.onDealDamageEnemy -= takeDamage;
        EventManager.current.onEnemyDeath -= die;
    }

}
