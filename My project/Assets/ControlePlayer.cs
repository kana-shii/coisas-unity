using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ControlePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sentido(CallbackContext context)
    {
        //Debug.Log(context.ReadValue<float>());
        transform.Translate(0.5f * context.ReadValue<float>(), 0, 0);
    }

}
