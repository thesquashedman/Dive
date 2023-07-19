using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBehavior : FishEnemyBehavior
{
    // The radius from the habitat within which this worm will attack the player.
    public float initialAttackRange = 25f;
    public float subsequentAttackRange = 40f;

    // The estimated length of this worm.
    public float wormLength = 30f;

    // The bones of this worm.
    public List<GameObject> bodyBones = new List<GameObject>();
    public List<GameObject> jawBones = new List<GameObject>();

    // The prefabs for the colliders of this worm.
    public GameObject bodyColliderPrefab;
    public GameObject jawColliderPrefab;

    // Variables for attacking.
    public float attackSpeed = 25f;

    // Variables for idling.
    private float wiggleTimer = 0f;
    public float wiggleIntervalLowerBound = 2f;
    public float wiggleIntervalUpperBound = 4f;
    public float wiggleSpeed = 3f;
    private Vector3 initialPosition;

    // Whether this worm should be awaken and out of the wall.
    private bool isAwaken = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 80f;
        rotationSpeed = 250f;
        base.Start();
        // attackSystem.GetComponent<BodyPart>().enemyID = gameObject.GetInstanceID();
        initialPosition = transform.parent.transform.position;
        transform.position = habitat.transform.position;
        SwitchMode("idle");

        // Set up the colliders for this worm.
        int wormID = gameObject.GetInstanceID();
        for (int i = 0; i < bodyBones.Count; i++)
        {
            GameObject temp = Instantiate(bodyColliderPrefab);
            temp.transform.position = bodyBones[i].transform.position;
            temp.transform.parent = bodyBones[i].transform;
            temp.GetComponent<BodyPart>().enemyID = wormID;
        }

        for (int i = 0; i < jawBones.Count; i++)
        {
            GameObject temp = Instantiate(jawColliderPrefab);
            temp.transform.position = jawBones[i].transform.position;
            temp.transform.parent = jawBones[i].transform;
            temp.GetComponent<BodyPart>().enemyID = wormID;
        }
    }

    protected override void FixedUpdate()
    {
        if (mode != "dead")
        {
            if (mode == "attack")
            {
                Attack();
                CheckAttackRange();
            }
            else if (mode == "coolDown")
            {
                CoolDown();
            }
            else if (mode == "runAway")
            {
                RunAway();
            }
            else if (mode == "wander")
            {
                Wander();
            }
            else if (mode == "idle")
            {
                Idle();
                CheckAttackRange();
            }
        }
    }

    // This function checks whether the player is within this worm's attack range. If the player
    // is, switch to attack mode. If the player is not, switch to idle mode.
    private void CheckAttackRange()
    {
        if (isAwaken)
        {
            if (mode != "attack" && Vector2.Distance(habitat.transform.position, player.transform.position) <= initialAttackRange)
            {
                SwitchMode("attack");
            }
            else if (mode == "attack" && Vector2.Distance(habitat.transform.position, player.transform.position) > subsequentAttackRange)
            {
                SwitchMode("idle");
            }
        }
        else if (Vector2.Distance(habitat.transform.position, player.transform.position) <= initialAttackRange)
        {
            isAwaken = true;
            SwitchMode("attack");
        }
    }

    // This function moves this worm out of the wall.
    protected override void Attack()
    {
        if (transform.parent.transform.position != habitat.transform.position)
        {
            Vector2 position = Vector2.MoveTowards(transform.parent.transform.position, habitat.transform.position, attackSpeed * Time.deltaTime);
            transform.parent.transform.position = position;
        }
    }

    // This function moves this worm partially back into the wall and makes this worm
    // wiggle while idling.
    protected override void Idle()
    {
        if (isAwaken)
        {
            Vector2 returnDirection = ((Vector2)initialPosition - (Vector2)habitat.transform.position).normalized;
            Vector3 targetRootPosition = (Vector2)habitat.transform.position + returnDirection * wormLength * 3f / 4f;
            Vector3 targetHeadPosition = (Vector2)habitat.transform.position - returnDirection * wormLength / 4f;

            // Move this worm partially back.
            if (transform.parent.transform.position != targetRootPosition)
            {
                Vector2 position = Vector2.MoveTowards(transform.parent.transform.position, targetRootPosition, attackSpeed / 4f * Time.deltaTime);
                transform.parent.transform.position = position;
                
                Vector2 direction = ((Vector2)targetHeadPosition - (Vector2)transform.position).normalized;
                rigidbody.AddForce(direction * speed / 2f);
            }
            // Make the worm wiggle.
            else
            {
                wiggleTimer += Time.deltaTime;
                if (wiggleTimer < wiggleIntervalLowerBound)
                {
                    Vector2 direction = new Vector2(returnDirection.y, -returnDirection.x);
                    rigidbody.AddForce(direction * wiggleSpeed);
                }
                else if (wiggleTimer < wiggleIntervalUpperBound)
                {
                    Vector2 direction = new Vector2(-returnDirection.y, returnDirection.x);
                    rigidbody.AddForce(direction * wiggleSpeed);
                }
                else
                {
                    wiggleTimer = 0f;
                }
            }
        }

    }
}
