/**	
 *  Project 1 - Lego Blocks
 *  MySingleton.cs
 *  Purpose: Restricts the instantiation of a class to one "single" instance;
 *  to persist throughout all scenes in the project.
 *  
 *  @author Professor Karamian
 *  10/8/19
 *  COMP 465
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySingleton<T> : MonoBehaviour where T : Component
{
    public static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = string.Format("--{0}", typeof(T).Name);
                    instance = obj.AddComponent<T>();
                }
                Debug.Log("ing_ instance created :");
            }
            return instance;
        }
    }

    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
