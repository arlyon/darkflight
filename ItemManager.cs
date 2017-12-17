using System;
using Jint;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private void Start()
    {
        var engine = new Engine()
                .SetValue("log", new Action<object>(Debug.Log))
            ;
    
        engine.Execute(@"
            function hello() { 
                log('Hello World');
            };
            
            hello();
    ");
    }
}