using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {


    private Rigidbody2D playerRb;
    private Animator animator;
    public Color Cor = Color.red;
    public string objectTag;

    //MOVIMENTO
    private bool verifyRight;
    float horizontal;
   
    //DETECTAR PLATAFORMA
    public LayerMask plataforma;
    public bool verificaChao; //VERIFICADOR
    public float raioChao;
    public Vector2 pontoColisao = Vector2.zero;

    //DETECTA PAREDE
    public LayerMask parede;
    public bool verificaParede;//VERIFICADOR
    public float raioParede;
    public Vector2 pontoColisaoParede = Vector2.zero;

    //DETECTA PLATAFORMA INTELIGENTE
    public bool verificaSmartChao; //VERIFICADOR
    public LayerMask smartChao;
   

    //vATRIBUTOS
    public float speed = 10;
    public float forcaPulo = 1300;
    private bool isRunning = false;
    
    

    void Start () {
        playerRb = GetComponent<Rigidbody2D>();      
        animator = GetComponent<Animator>();
        verifyRight = transform.localScale.x > 0;
	}

    void FixedUpdate()
    {

        horizontal = Input.GetAxis("Horizontal");
        Move(horizontal);
        ChangeDirection(horizontal);
        VerifyChao();
        VerifyParede();
        ControlaLayers();

        //ATRIBUIR VELOCIDADE PLATAFORMA AO PLAYER
        if (verificaSmartChao)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x + SmartPlataform.direcaoP, playerRb.velocity.y);
        }

        //COMANDO PULAR
        if (Input.GetButton("Jump"))
        {
            Pular();
        }

        //COMANDO CORRER
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }




    //MORTE
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == objectTag)
        {
            animator.SetBool("dead", true);
            StartCoroutine("Die");      
        }
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(1);    
        SceneManager.LoadScene("Game");
    }

    //ANDAR / CORRER
    private void Move(float h)
    {
        if (!verificaParede)
        {
            playerRb.velocity = new Vector2(h * speed, playerRb.velocity.y);
            animator.SetFloat("walk", Mathf.Abs(h));
      
            if (isRunning && h != 0)
            {
                speed = 20;
                playerRb.velocity = new Vector2(h * speed, playerRb.velocity.y); 
                animator.SetFloat("correr", 11);


            }
            else
            {
                speed = 10; 
                animator.SetFloat("correr", 0);
            }
        }
        else
        {
            animator.SetFloat("walk", 0.0f);      
            animator.SetFloat("correr", 0);
        }
    }

    //PULAR
    private void Pular()
    {
        if((verificaChao || verificaSmartChao) && playerRb.velocity.y <= 5)
        {
            playerRb.AddForce(new Vector2(0, forcaPulo));
            animator.SetTrigger("jump");
        }
    }

    //CAIR
    private void Cair()
    {
        if ((!verificaChao || !verificaSmartChao) && playerRb.velocity.y <= 0)
        {
            animator.SetBool("fall", true);
            animator.ResetTrigger("jump");
        }
        if (verificaChao || verificaSmartChao)
        {
            animator.SetBool("fall", false);
        }

    }

    //VIRANDO
    private void ChangeDirection(float horizontal)
    {
        if (horizontal > 0 && !verifyRight || horizontal < 0 && verifyRight)
        {
            verifyRight = !verifyRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    //VERIFICAR CHAO / CHAO INTELIGENTE
    private void VerifyChao()
    {
        var pontoPosicao = pontoColisao;
        pontoPosicao.x += transform.position.x;
        pontoPosicao.y += transform.position.y;
        verificaChao = Physics2D.OverlapCircle(pontoPosicao, raioChao, plataforma); // CHAO
        verificaSmartChao = Physics2D.OverlapCircle(pontoPosicao, raioChao, smartChao); // chao INTELIGENTE
        Cair();
    }

    //VERIDICAR PAREDE
    private void VerifyParede()
    {
        var pontoPosicao = pontoColisaoParede;
        pontoPosicao.x += transform.position.x;
        pontoPosicao.y += transform.position.y;
        verificaParede = Physics2D.OverlapCircle(pontoPosicao, raioParede, parede);  
    }

    //DESENHAR O PONTO DE VERIFICACAO
    private void OnDrawGizmos()
    {
        Gizmos.color = Cor;
        var pontoPosicao = pontoColisao;
        pontoPosicao.x += transform.position.x;
        pontoPosicao.y += transform.position.y;
        var pontoPosicaoP = pontoColisaoParede;
        pontoPosicaoP.x += transform.position.x;
        pontoPosicaoP.y += transform.position.y;
        Gizmos.DrawWireSphere(pontoPosicao, raioChao);
        Gizmos.DrawWireSphere(pontoPosicaoP, raioParede);//VERIFICAR PAREDE TESTES
    }

    //CONTROLA AS DUAS LAYERS
    void ControlaLayers()
    {
        if (!verificaChao)
        {
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }

}
