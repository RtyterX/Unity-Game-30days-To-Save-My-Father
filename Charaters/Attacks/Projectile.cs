using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Animator myAnimator;
    public Rigidbody2D myRigidbody;

    public float speed;
    public float lifeTime;

    public float collideTime;

    public bool started;
    public float loadTime;

    public GameObject target;

    public GameObject destroyEffect;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        started = false;


        //if (!started) 
       // {
            // Animação
            //myAnimator.SetBool("NotStarted", true);
           // Invoke("StartProjectile", loadTime);

            // Ativa o DestroyProjectile e Determina o tempo de Vida
           // Invoke("DestroyProjectile", lifeTime);
      //  }

    }

    public void Update()
    {
        if (!started)
        {
            // Para o Movimento
           myRigidbody.velocity = Vector2.zero;
        }
        else
        {
            // Animação
          //  myAnimator.SetBool("Moving", true);
         //   myAnimator.SetBool("NotStarted", false);

            // Movimento
            transform.Translate(target.transform.position * speed * Time.deltaTime);
        }
    }

    public void StartProjectile()
    {
        started = true;
    }

    public void DestroyProjectile()
    {
        if (destroyEffect)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);

    }

   // public void OnCollisionEnter2D(Collision2D col)
   // {
       // Animação
   //     myAnimator.SetBool("Collid", true);
    //    myAnimator.SetBool("Moving", false);
    //    transform.position = col.transform.position;
   //     Invoke("DestroyProjectile", collideTime);
   // }

}

