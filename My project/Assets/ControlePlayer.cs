using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ControlePlayer : MonoBehaviour
{

    float sentido = 0; // o nome desse sujeito é campo, que é uma variavel declarada no escopo da classe

    [Range(0f, 6f)]
    [SerializeField]
    float forcaPulo = 4f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velAtual = GetComponent<Rigidbody2D>().velocity;
        velAtual.x = 5f * sentido;
        GetComponent<Rigidbody2D>().velocity = velAtual;
    }

    public void Sentido(CallbackContext context)
    {
        //Debug.Log(context.ReadValue<float>());
        sentido = context.ReadValue<float>();
        //transform.Translate(0.5f * context.ReadValue<float>(), 0, 0);
    }

    public void Pular(CallbackContext context)
    {
        if (context.ReadValue<float>() == 1)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forcaPulo), ForceMode2D.Impulse);
        }
    }

}