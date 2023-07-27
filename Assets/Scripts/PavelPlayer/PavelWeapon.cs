using UnityEngine;
using Lowscope.Saving;

public class PavelWeapon : MonoBehaviour, ISaveable
{
    // Start is called before the first frame update
    public string weaponName = "Default";
    public bool isAquired = false;

    struct SaveData
    {
        public bool isAquired;
    }

    public virtual void OnLoad(string data)
    {
        isAquired = JsonUtility.FromJson<SaveData>(data).isAquired;
    }

    public virtual string OnSave()
    {
        return JsonUtility.ToJson(new SaveData { isAquired = isAquired });
    }

    public virtual bool OnSaveCondition()
    {
        return true;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
