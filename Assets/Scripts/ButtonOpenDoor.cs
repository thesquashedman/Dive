using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOpenDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject door;
    public Transform doorMovePoint;
    bool isOpen = false;
    public float interactDistance = 1f;

    public float speed = 0.01f;
    public bool startOpening = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(!isOpen)
        {
            if(PavelPlayerSettingStates.current.isInteracting)
            {
                if(Vector2.Distance(transform.position, PavelPlayerSettingStates.current.transform.position) > interactDistance)
                {
                    return;
                }
                //door.transform.position = Vector2.MoveTowards(door.transform.position, doorMovePoint.position, speed);
                if (!startOpening) {
                    AudioManager.instance.Play("MetalDoor");
                }
                startOpening = true;
                if (Vector2.Distance(door.transform.position, doorMovePoint.position) < 0.1f)
                {
                    isOpen = true;
                }
                
            }
            
            


            
        }
    }

    private void FixedUpdate()
    {
        if (startOpening)
        {
            door.transform.position = Vector2.MoveTowards(door.transform.position, doorMovePoint.position, speed);
        }
    }

    void OpenDoor()
    {
        door.transform.position = doorMovePoint.position;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }
}
