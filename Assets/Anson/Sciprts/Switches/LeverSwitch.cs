using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverSwitch : SwitchInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        if (interactState == InteractState.On)
        {
            OnOn();
        }else if (interactState == InteractState.Off)
        {
            OnOff();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
