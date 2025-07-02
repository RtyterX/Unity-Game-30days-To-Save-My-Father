using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AranhaScript : EnemyState
{
    public float newSpeed;
    public float jumpRadius;

    public InputController playerInput;

    Vector2 nextMove;

    public GameObject projectile;

    public override void Start()
    {
        base.Start();
        canMove = true;
        canAttack = true;
    }
    

    public override void ChangeState(StateMachine enemyState)
    {
        base.ChangeState(enemyState);
        switch (state)
        {
            case StateMachine.Idle:
                ReturnMovement();
                ReturnAttack();
                break;
            case StateMachine.Interect:
                canMove = false;
                canAttack = false;
                break;
            case StateMachine.Attacking:
                canMove = false;
                canAttack = false;
                break;
            case StateMachine.Stagger:
                canMove = false;
                canAttack = false;
                break;
            case StateMachine.Dead:
                canMove = false;
                canAttack = false;
                break;
        }
    }

    public override void Update()
    {
        base.Update();

        // Movement
        if (canMove)
        {
            if (Vector3.Distance(target.transform.position, transform.position) <= attackRadius)
            {
                FollowTargetLine();
            }
            else if (Vector3.Distance(target.transform.position, transform.position) <= jumpRadius)
            {
                // JumpBack();
            }
            else if (Vector3.Distance(target.transform.position, transform.position) <= battleRadius)
            {
                ChaseMoviment(newSpeed);
            }
        }
        else
        {
            // Not Move
        }

        // Attack
        if (canAttack)
        {
            if (Vector3.Distance(target.transform.position, transform.position) <= attackRadius)
            {

                if (Vector3.Distance(target.transform.position, transform.position) <= jumpRadius)
                {
                    JumpBack();
                }
                if (transform.position.x <= target.transform.position.x + 0.10f && transform.position.x >= target.transform.position.x - 0.10f)
                {
                    Debug.Log("Chegou Aqui");
                    ShootAttack();
                }
                if (transform.position.y <= target.transform.position.y + 0.10f && transform.position.y >= target.transform.position.y - 0.10f)
                {
                    ShootAttack();
                }
            }

        }

    }


    public void FollowTargetLine()
    {
        if (playerInput.Vertical > 0)
        {
            nextMove = new Vector2(target.transform.position.x, gameObject.transform.position.y + 1);
        }
        else if (playerInput.Vertical < 0)
        {
            nextMove = new Vector2(target.transform.position.x, gameObject.transform.position.y - 1);
        }
        else if (playerInput.Horizontal > 0)
        {
            nextMove = new Vector2(gameObject.transform.position.x + 1, target.transform.position.y);
        }
        else if (playerInput.Horizontal < 0)
        {
            nextMove = new Vector2(gameObject.transform.position.x - 1, target.transform.position.y);
        }

        Vector3 move = Vector3.MoveTowards(transform.position, nextMove, newSpeed * Time.deltaTime);
        myRigidbody.MovePosition(move);
    }


    public void JumpBack()
    {
        canMove = false;

        if (playerInput.Horizontal > 0)
        {
            nextMove = new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y);
        }
        else if (playerInput.Horizontal < 0)
        {
            nextMove = new Vector2(gameObject.transform.position.x - 1, gameObject.transform.position.y);
        }
        else if (playerInput.Vertical > 0)
        {
            nextMove = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1);
        }
        else if (playerInput.Vertical < 0)
        {
            nextMove = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1);
        }

        Vector3 move = Vector3.MoveTowards(transform.position, nextMove, 40 * Time.deltaTime);
        myRigidbody.MovePosition(move);

        Invoke("ReturnMovement", 0.7f);

        Debug.Log("Realizou JumpBack");
    }


    public void ShootAttack()
    {
        canMove = false;
        canAttack = false;

        Instantiate(projectile, new Vector2(transform.position.x, transform.position.y - 0.17f), transform.rotation);

        Invoke("ReturnMovement", 0.7f);
        Invoke("ReturnAttack", 2.4f);

        Debug.Log(name + "Atacou");
    }

    public void ShootAttack2(bool directionX)
    {
        canMove = false;
        canAttack = false;

        if (target.transform.position.x > gameObject.transform.position.x)
        {
            Instantiate(projectile, new Vector2(transform.position.x, transform.position.y - 0.17f), transform.rotation);
        }
        else
        {
            Instantiate(projectile, new Vector2(transform.position.x, transform.position.y - 0.17f), transform.rotation);
        }
        if (target.transform.position.y > gameObject.transform.position.y)
        {
            Instantiate(projectile, new Vector2(transform.position.x, transform.position.y - 0.17f), transform.rotation);
        }
        else
        {
            Instantiate(projectile, new Vector2(transform.position.x, transform.position.y - 0.17f), transform.rotation);
        }   

        Invoke("ReturnMovement", 0.7f);
        Invoke("ReturnAttack", 2.4f);
    }


    public void ReturnMovement()
    {
        canMove = true;
    }

    public void ReturnAttack()
    {
        canAttack = true;
    }


}
