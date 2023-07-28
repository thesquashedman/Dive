using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Written by Justin

Plays sounds for the character.

Array:
0: Damaged
1: Dies
2: Suffocated
3,4,5: SawRev, Loop, WindDown
6: Gun


*/
public class CharacterSound : EntitySound
{
    string currWeapon = "";

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        //Player takes damage
        EventManager.current.onDealDamagePlayer += damaged;
        //Player attacks 
        EventManager.current.onPlayPlayerRecoil += playerAttack;
        //Player stops attacking
        EventManager.current.onPlayerStopAttack += stopAttack;
        //Player switches their weapon
        EventManager.current.onPlayerSwitchWeapon += switchWeapon;
        //Player dies
        EventManager.current.onPlayerDeath += died;
        //Player suffocates
        EventManager.current.onPlayerSuffocate += suffocating;

        currWeapon = "";
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.I)) {
            EventManager.current.dealDamagePlayer(0f);
        } else if(Input.GetKeyDown(KeyCode.O)) {
            EventManager.current.PlayerAttack();
        } else if(Input.GetKeyDown(KeyCode.P)) {
            EventManager.current.PlayerStopAttack();
        } else if(Input.GetKeyDown(KeyCode.L)) {
            EventManager.current.PlayerSwitchWeapon("Saw");
        } else if(Input.GetKeyDown(KeyCode.K)) {
            EventManager.current.playerDeath();
        } else if(Input.GetKeyDown(KeyCode.M)) {
            EventManager.current.playerSuffocate();
        }
        */
    }

    //Plays audio[0] when player is damaged
    void damaged(float dmg) {
        audios[0].Play();
    }

    //Plays audio[3+] when player attacks, depending on the weapon
    //Saw uses audio[3,4,5]
    //Gun uses audio[6]
    void playerAttack() {
        Debug.Log("Player attack");
        Debug.Log(currWeapon);
        if(currWeapon == "ProjectileGun") {
            audios[6].Play();
        } else if (currWeapon == "Saw"){
            audios[3].Play();
            StartCoroutine(sawLoop());
        }
    }

    IEnumerator sawLoop() {
        while(audios[3].isPlaying) {
            yield return null;
        }

        audios[4].Play();
    }

    //Stops the audio when player stops attacking
    void stopAttack() {
        if(currWeapon == "Saw" && (audios[4].isPlaying || audios[3].isPlaying)) {
            audios[3].Stop();
            audios[4].Stop();
            audios[5].Play();
        } 
    }

    //Internal bookkeeping
    void switchWeapon(string weapon) {
        if(currWeapon == "Saw") {
            stopAttack();
        }

        currWeapon = weapon;
    }

    //Plays audio[1] when player dies
    void died() {
        audios[1].Play();
    }

    //Plays audio[2] when player suffocates
    void suffocating() {
        audios[2].Play();
    }



    private void OnDisable() {
        //Player takes damage
        EventManager.current.onDealDamagePlayer -= damaged;
        //Player attacks 
        EventManager.current.onPlayPlayerRecoil -= playerAttack;
        //Player stops attacking
        EventManager.current.onPlayerStopAttack -= stopAttack;
        //Player switches their weapon
        EventManager.current.onPlayerSwitchWeapon -= switchWeapon;
        //Player dies
        EventManager.current.onPlayerDeath -= died;
        //Player suffocates
        EventManager.current.onPlayerSuffocate -= suffocating;
    }
}
