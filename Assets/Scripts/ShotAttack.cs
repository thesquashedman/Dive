using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAttack : AttackSystem
{
    public bool attackOnce;
    public float damagePeriods;

    public GameObject atackElement;

    //public GameObject objectToSpawn; // Assign the object prefab you want to spawn in the Inspector
    public float spawnInterval = 2.0f; // Time interval in seconds between spawns
    public float forceAmount = 10.0f; // Amount of force to add to the object

    private float timer = 0f;

    public Transform shotPos;

    public bool OneBulletPerPress = false;

    //private AttackElement attackElement;

    private void Start()
    {
        atackElement.SetActive(false);
    }

    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
    }

    public override void Attack(string tragetTag)
    {
        if (OneBulletPerPress)
        {
            SpawnObject();
        }
        else if (timer <= 0f)
        {
            SpawnObject();
            timer = spawnInterval;
        }
        //atackElement.GetComponent<AttackElement>().SetAttackTargetTag(tragetTag);
        //this.CloseAttackStart();
    }

    void SpawnObject()
    {
        // Instantiate the object at the position of the spawner
        GameObject spawnedObject = Instantiate(atackElement, shotPos.position, Quaternion.identity);
        spawnedObject.SetActive(true);

        // Add force to the spawned object
        Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();

        // Debug.Log("BBBBBBBBBBBBBBBBBBBB");
        if (rb != null)
        {
            // Debug.Log("CCCCCCCCCCCCCCCCC");
            rb.AddForce(transform.right * forceAmount, ForceMode2D.Impulse);
        }
    }

    public override void StopAttack()
    {
        this.CloseAttackEnd();
    }

    public void CloseAttackStart()
    {
        // Debug.Log("AAAAAAAAA");
        atackElement.SetActive(true);
    }

    public void CloseAttackEnd()
    {
        atackElement.SetActive(false);
    }

    public void CloseAttackFinsih()
    {
        atackElement.SetActive(false);
    }
}
