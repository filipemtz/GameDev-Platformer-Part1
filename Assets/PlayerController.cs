using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MOVEMENT_SPEED = 10.0f;
    public float JUMP_FORCE = 800.0f;

    // radio do circulo para determinar se o personagem está no chão
    float GROUND_CHECK_RADIUS = .2f;

    // os itens abaixo serao definidos no editor
    public LayerMask jumpableLayers;  // layers sobre os quais o personagem pode saltar
    public Transform groundCheckPosition;  // posicao do obj usado para ground check

    private Rigidbody2D rb;

    bool facingRight = true;

    public GameObject projectilePrefab;
    public GameObject laserPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    bool IsGrounded()
    {
        // busca todos os itens com colisao abaixo do personagem
		Collider2D[] colliders = Physics2D.OverlapCircleAll(
            groundCheckPosition.position,
            GROUND_CHECK_RADIUS,
            jumpableLayers);

        // se pelo menos uma colisao nao eh com o proprio
        // personsagem, o personagem esta' no chao.
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				return true;
		}

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        float vx = Input.GetAxisRaw("Horizontal") * MOVEMENT_SPEED;
        float vy = rb.velocity.y;  // not changed
        rb.velocity = new Vector2(vx, vy);

        if ((vx > 0 && !facingRight) || (vx < 0 && facingRight))
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }

        if (IsGrounded() && Input.GetButtonDown("Jump"))
            rb.AddForce(transform.up * JUMP_FORCE);

        if (Input.GetButtonDown("Fire1"))
            Instantiate(projectilePrefab,
                        gameObject.transform.position,
                        gameObject.transform.rotation);

        if (Input.GetButtonDown("Fire2"))
        {
            Instantiate(laserPrefab,
                        gameObject.transform.position,
                        gameObject.transform.rotation);
        }
    }
}



