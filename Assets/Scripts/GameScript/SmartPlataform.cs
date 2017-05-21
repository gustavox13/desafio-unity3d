using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPlataform : MonoBehaviour
{
    [SerializeField]
    private float MinY;
    [SerializeField]
    private float MaxY;
    [SerializeField]
    private float MinX;
    [SerializeField]
    private float MaxX;
    [SerializeField]
    private float VelocidadeX;
    [SerializeField]
    private float VelocidadeY;
    private Rigidbody2D PlataformRb;

    //ATRIBUTOS
    public static float direcaoP = 0;

    void Start()
    {

        PlataformRb = GetComponent<Rigidbody2D>();
        PlataformRb.velocity = new Vector2(VelocidadeX, VelocidadeY);
    }

    void Update()
    {

        if (transform.localPosition.y > MaxY)
            PlataformRb.velocity = new Vector2(PlataformRb.velocity.x, -VelocidadeY);

        else if (transform.localPosition.y < MinY)
            PlataformRb.velocity = new Vector2(PlataformRb.velocity.x, VelocidadeY);


        if (transform.localPosition.x > MaxX)
            PlataformRb.velocity = new Vector2(-VelocidadeX, PlataformRb.velocity.y);

        else if (transform.localPosition.x < MinX)
            PlataformRb.velocity = new Vector2(VelocidadeX, PlataformRb.velocity.y);

        if(PlataformRb.velocity.x != 0)       
        direcaoP = PlataformRb.velocity.x; //ATRIBUINDO DIRECAO PLATAFORMA
       
    }

   
}
