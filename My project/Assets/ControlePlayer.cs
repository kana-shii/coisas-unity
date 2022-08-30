using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ControlePlayer : MonoBehaviour
{

    float sentido = 0; // o nome desse sujeito é campo, que é uma variavel declarada no escopo da classe
    const float RAIO_JUMPABLE = 0.05f;
    int puloVezes = 1;
    int puloMax = 2; // somar

    [Range(0f, 20f)]
    [SerializeField]
    float forcaPulo = 8f;

    [SerializeField]
    LayerMask layerMask;

    bool noChao = false;

    Animator animator;
    Rigidbody2D rigidbody;

    Vector3 checkpoint;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        checkpoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velAtual = rigidbody.velocity;
        velAtual.x = 8f * sentido;
        rigidbody.velocity = velAtual;
        animator.SetBool("GROUNDED", noChao);
        if (sentido != 0)
        {
            gameObject.transform.localScale = new Vector3(0.7f * sentido, 0.7f, 0.7f);
            animator.SetBool("WALKING", true);
        } else
        {
            animator.SetBool("WALKING", false);
        }

        
    }

    private void FixedUpdate()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, RAIO_JUMPABLE, layerMask);
        Debug.Log(rigidbody.velocity.y);
        if (hit != null && (rigidbody.velocity.y <= 0.5f || puloVezes == 0))
        {
            if (hit.CompareTag("CHAO"))
            {
                noChao = true;
                animator.SetBool("JUMP", false);
                puloVezes = 0;
            } else if (hit.CompareTag("ADVERSARIO"))
            {
                Destroy(hit.gameObject);
            }
            
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
        if (context.ReadValue<float>() == 1 && puloVezes <= puloMax)
        {
            //GetComponent<Rigidbody2D>().velocity = new Vector3(0.001f, 0.001f, 0.001f);
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(new Vector2(0, forcaPulo), ForceMode2D.Impulse);
            animator.SetBool("JUMP", true);
            //GetComponent<Animator>().SetBool("GROUNDED", false);
            noChao = false;
            puloVezes += 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ADVERSARIO"))
        {
            transform.position = checkpoint;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CHECKPOINT"))
        {
            checkpoint = collision.transform.position;
            collision.gameObject.GetComponent<Animator>().SetBool("CHECK", true);
            Destroy(collision.gameObject.GetComponent<BoxCollider2D>());
        }
    }

}