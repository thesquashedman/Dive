using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FixObject : MonoBehaviour
{

    
    public Light2D light2D;
    public Color FixedColor;
    public GameObject text;
    public ProgressFill myProgress;

    float value = 0;
    public float curent = 0;
    public float fixSpeed = 10;
    public bool isFixed = false;
    
    //Give this object a task name to trigger other events on completion with.
    public string myName = "";

    public float interactDistance = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFixed)
        {
            if(PavelPlayerSettingStates.current.isInteracting)
            {
                if(Vector2.Distance(transform.position, PavelPlayerSettingStates.current.transform.position) > interactDistance)
                {
                    return;
                }
                myProgress.setActive(true);
                Fix(fixSpeed * Time.deltaTime);
                
            }
            else
            {
                if(Vector2.Distance(transform.position, PavelPlayerSettingStates.current.transform.position) > interactDistance) {
                    myProgress.setActive(false);
                }

                if(curent <= 0)
                {
                    curent = 0;
                } else {
                    curent -= fixSpeed * Time.deltaTime;
                }
            }

            myProgress.setCurr(curent);
        }
    }

    void StartFixing()
    {

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }

    public void Fix(float fixAmount) {
        curent += fixAmount;

        if (curent >= 100) {
            EventManager.current.taskCompleted(myName);

            light2D.color = FixedColor;
            
            ParticleSystem myParticle = GetComponentInChildren<ParticleSystem>();
            if(myParticle != null) {
                myParticle.Stop();
            }
            
            isFixed = true;
            text.SetActive(false);
            myProgress.setActive(false);
        }
    }
}
