using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestUnityEventScriot : MonoBehaviour
{
    [SerializeField]
    private UnityEvent unityEvent; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Play event")]
    public void PlayUnityEvent()
    {
        unityEvent.Invoke();
    }
}
