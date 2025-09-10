using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class inimigo : MonoBehaviour
{
    //Variaveis
    //Será usada para movimentar o agente (inimigo)
    private NavMeshAgent agent;

    //Sinaliza o alvo do inimigo
    [SerializeField] private Transform alvo;

    //Controla as animações do inimigo
    private Animator anim;

    //Conta o numero de ataques realizados
    [SerializeField] int atkCound = 0;

    //Armazena o tempo que passou desde o último ataque
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
    void Update()
    {
        
    }
}
