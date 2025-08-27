using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    //variaveis
    private Rigidbody controller;//Controlador do Rigidbody
    public Animator anim;//Controlador das animações 
    public float speed;//Velocidade do player 
    public float root;//Rotação do player

    private Vector3 moveDirection; //Direcionador de movimentação

    public float x;
    public float y;

    private float jumpForce;
    public bool isGrounded = false;
    public float areaCollisionChao;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Vector3 collisionPosition;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        //Trava o cursor do mouse na tela
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Definir o eixo do mouse
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(new Vector3(0, mouseX * root * Time.deltaTime, 0));

        Mover();
        if(!isGrounded && controller.velocity.y < 0)
        {
            anim.SetBool("varJump", false);
        }
    }
    private void Mover() {
        /* transform.position + collisionPosition:  Define a posição central da esfera
         transform.position é a posição atual do objeto, e collisionPosition é um vetor
        que ajusta essa posição*/

        var groundCheckChao = Physics.OverlapSphere(transform.position + collisionPosition, 
            areaCollisionChao, layer);

        if (groundCheckChao.Length != 0)
        {
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");

            isGrounded = true;
        }
        else
        {
            anim.SetBool("varRun", false);
            anim.SetBool("varRunBack", false);
            anim.SetBool("varRunLeft", false);
            anim.SetBool("varRunRight", false);

            isGrounded = false;
        }

        moveDirection = new Vector3(x, 0, y);

        if(x == 0 && y == 0)//Parado
        {
            anim.SetBool("varRun", false);
            anim.SetBool("varRunBack", false);
            anim.SetBool("varRunLeft", false);
            anim.SetBool("varRunRight", false);
        }
        if(y > 0)//Indo para frente
        {
            anim.SetBool("varRun", true);
            anim.SetBool("varRunBack", false);
            anim.SetBool("varRunLeft", false);
            anim.SetBool("varRunRight", false);
        }
   
    }
    private void Pular() { }
    private void OnDrawGizmos()
    {
        
    }
}
