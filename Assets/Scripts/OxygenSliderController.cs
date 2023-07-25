using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OxygenSliderController : MonoBehaviour
{
    public PavelPlayerOxygen oxSys;
    float maxOxyScale = 10;
    public float oxLevel;
    // Start is called before the first frame update
    void Start()
    {
        maxOxyScale = this.gameObject.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        //this.gameObject.GetComponent<Transform>().transform.localScale.y = oxSys.GetOxygenLevel();

        oxLevel = (oxSys.GetOxygenLevel() / 100) * maxOxyScale;

        this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, oxLevel, this.gameObject.transform.localScale.z);
    }


}
