using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Lowscope.Saving;

public class FixObject : MonoBehaviour, ISaveable
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

    bool temp = false;

    public struct SaveData
    {
        public bool isFixed;
    }

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if(temp)
        {
            EventManager.current.taskCompleted(myName);
            temp = false;
        }
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

    public string OnSave()
    {
        return JsonUtility.ToJson(new SaveData { isFixed = isFixed });
    }

    public void OnLoad(string data)
    {
        
        isFixed = JsonUtility.FromJson<SaveData>(data).isFixed;
        if(isFixed)
        {
            temp = true;
            light2D.color = FixedColor;
            
            ParticleSystem myParticle = GetComponentInChildren<ParticleSystem>();
            if(myParticle != null) {
                myParticle.Stop();
            }
            
            text.SetActive(false);
            myProgress.setActive(false);
        }
    }

    public bool OnSaveCondition()
    {
        return !PavelPlayerSettingStates.current.isDead;
    }
}
