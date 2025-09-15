using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class inimigo : MonoBehaviour
{
    //Variaveis
    //Ser� usada para movimentar o agente (inimigo)
    private NavMeshAgent agent;

    //Sinaliza o alvo do inimigo
    [SerializeField] private Transform alvo;

    //Controla as anima��es do inimigo
    private Animator anim;

    //Conta o numero de ataques realizados
    [SerializeField] int atkCound = 0;

    //Armazena o tempo que passou desde o �ltimo ataque
    [SerializeField] float tempoAtk = 0.0f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(alvo.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //15/09
    void Update()
    {
        //Se o alvo n�o for nulo(existe um alvo), ele chama o m�todo
        if(alvo != null)
        {
            Distancia();
        }
    }

    private void Distancia()
    {
       if(Vector3.Distance(transform.position, alvo.position) <= 3.0f 
            && atkCound == 0)//Calcula a distancia 
        {
            anim.SetTrigger("varAtk");
            tempoAtk = 0.0f;
            atkCound++;
        }
       //Verifica se o estado atual da anima��o � "attack"
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            agent.ResetPath();
        }
        else
        {
            agent.SetDestination(alvo.position);
            tempoAtk += Time.deltaTime;
            if(tempoAtk >= 0.1f)
            {
                atkCound = 0;
            }
        }
    }
}
