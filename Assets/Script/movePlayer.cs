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
        //-------------------01/09/2025------------------
        if(y < 0)//Indo para trás
        {
            anim.SetBool("varRun", false);
            anim.SetBool("varRunBack", true);
            anim.SetBool("varRunLeft", false);
            anim.SetBool("varRunRight", false);
        }
        if(x > 0 && y == 0)//Indo apenas para a direita
        {
            anim.SetBool("varRun", false);
            anim.SetBool("varRunBack", false);
            anim.SetBool("varRunLeft", false);
            anim.SetBool("varRunRight", true);
        }
        if(x < 0 && y == 0)//Indo apenas para a esquerda
        {
            anim.SetBool("varRun", false);
            anim.SetBool("varRunBack", false);
            anim.SetBool("varRunLeft", true);
            anim.SetBool("varRunRight", false);
        }
        if(y > 0 && x < 0)//Indo  para a esquerda
        {
            anim.SetBool("varRun", true);
            anim.SetBool("varRunBack", false);
            anim.SetBool("varRunLeft", false);
            anim.SetBool("varRunRight", false);

        }
        if (y > 0 && x > 0)//Indo  para a direita
        {
            anim.SetBool("varRun", true);
            anim.SetBool("varRunBack", false);
            anim.SetBool("varRunLeft", false);
            anim.SetBool("varRunRight", false);

        }
        if(y < 0 && x > 0)//Trás e direita
        {
            anim.SetBool("varRun", false);
            anim.SetBool("varRunBack", true);
            anim.SetBool("varRunLeft", false);
            anim.SetBool("varRunRight", false);
        }
        if(y < 0 && x < 0)//Trás e esquerda
        {
            anim.SetBool("varRun", false);
            anim.SetBool("varRunBack", true);
            anim.SetBool("varRunLeft", false);
            anim.SetBool("varRunRight", false);
        }
        //Ditar o direcionador que vai ser utilizado
        moveDirection = transform.TransformDirection(moveDirection);

        //Movimentar o personagem
        transform.position += moveDirection * speed * Time.deltaTime;

        if(isGrounded && Input.GetButton("Jump"))
        {
            anim.SetBool("varJump", true);
            //Executar o pulo com um certo delay 
            Invoke("Pular", 0.5f);
        }
    }
    private void Pular() {
        //03/09
        //Define a força do pulo
        jumpForce = 2.6f;

        //Aplica a força do pulo na direção vertical (Y) usando a 
        //propriedade velocity do controller
        controller.velocity = new Vector3(0f, jumpForce, 0f);

        //Desativa as animações de corrida (em todas as direções), 
        //já que o personagem está pulando
        anim.SetBool("varRun", false);
        anim.SetBool("varRunBack", false);
        anim.SetBool("varRunLeft", false);
        anim.SetBool("varRunRight", false);
    }
    //Método usado para desenhar elementos de depuração na cena
    //(visivel apenas no editor na Unity)
    private void OnDrawGizmos()
    {
        //Definir a cor do gizmo como ciano
        Gizmos.color = Color.cyan;

        //desenha uma esfera "false" (apenas o contorno) na posição do personagem  +offset
        //da colisão com o raio definido por 'areaCollisionChao' para representar a área
        //de colisão com o chão
        Gizmos.DrawWireSphere(transform.position + collisionPosition, areaCollisionChao);
    }
}
