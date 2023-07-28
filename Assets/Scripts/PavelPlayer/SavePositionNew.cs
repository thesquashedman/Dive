using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lowscope.Saving;
using System;

/// <summary>
/// Example class of how to store a position.
/// Also very useful for people looking for a simple way to store a position.
/// </summary>

//[AddComponentMenu("Saving/Components/Save Position"), DisallowMultipleComponent]
public class SavePositionNew : MonoBehaviour, ISaveable
{
    Vector3 lastPosition;

    [Serializable]
    public struct SaveData
    {
        public Vector3 position;
    }

    public void OnLoad(string data)
    {
        var pos = JsonUtility.FromJson<SaveData>(data).position;
        transform.position = pos;
        lastPosition = pos;
    }

    public string OnSave()
    {
        lastPosition = transform.position;
        return JsonUtility.ToJson(new SaveData { position = lastPosition });
    }

    public bool OnSaveCondition()
    {
        return lastPosition != transform.position && !PavelPlayerSettingStates.current.isDead;
    }
}