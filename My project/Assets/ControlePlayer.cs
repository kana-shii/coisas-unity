using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ControlePlayer : MonoBehaviour
{

    float sentido = 0; // o nome desse sujeito é campo, que é uma variavel declarada no escopo da classe
    const float RAIO_JUMPABLE = 0.5f;

    [Range(0f, 20f)]
    [SerializeField]
    float forcaPulo = 8f;

    [SerializeField]
    LayerMask layerMask;

    bool noChao = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velAtual = GetComponent<Rigidbody2D>().velocity;
        velAtual.x = 8f * sentido;
        GetComponent<Rigidbody2D>().velocity = velAtual;
        GetComponent<Animator>().SetBool("GROUNDED", noChao);
        if (sentido != 0)
        {
            gameObject.transform.localScale = new Vector3(0.7f * sentido, 0.7f, 0.7f);
            GetComponent<Animator>().SetBool("WALKING", true);
        } else
        {
            GetComponent<Animator>().SetBool("WALKING", false);
        }

        
    }

    private void FixedUpdate()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, RAIO_JUMPABLE, layerMask);
        if (hit != null)
        {
            noChao = true;
        } else
        {
            noChao = false;
        }
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
            GetComponent<Animator>().SetTrigger("JUMP");
            //GetComponent<Animator>().SetBool("GROUNDED", false);
            noChao = false;
        }
    }

}