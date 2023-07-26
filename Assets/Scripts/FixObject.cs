using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixObject : MonoBehaviour
{

    public GameObject initial;
    public GameObject afterFix;

    float value = 0;
    float curent = 0;
    public float fixSpeed = 10;
    bool isFixed = false;
    
    //Give this object a task name to trigger other events on completion with.
    public string myName = "";

    // Start is called before the first frame update
    void Start()
    {
        initial.SetActive(true);
        afterFix.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFixed)
        {

        
            if(PavelPlayerSettingStates.current.isInteracting)
            {
                Fix(fixSpeed * Time.deltaTime);
                
            }
            else
            {
                if(curent <= 0)
                {
                    curent = 0;
                }
                else
                {
                    curent -= fixSpeed * Time.deltaTime;
                }
            }
        }
    }
    void StartFixing()
    {

    }

    public void Fix(float fixAmount) {
        curent += fixAmount;
        if (curent >= 100) {
            EventManager.current.taskCompleted(myName);
            initial.SetActive(false);
            afterFix.SetActive(true);
            isFixed = true;
        }
    }
}
