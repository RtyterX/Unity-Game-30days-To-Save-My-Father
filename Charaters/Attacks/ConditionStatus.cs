using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TypeStatus
{
    Healthy,
    Poisoned,
    Electrocuted,
    Burning,
    Frozen,
}

public class ConditionStatus : MonoBehaviour
{
    [SerializeField] TypeStatus typeStatus;
    //  [SerializeField] HealthBar healthBar;
    [SerializeField] DamageType damageType;
    public PlayerMovement2D playerMovement;


    public void Start()
    {
        typeStatus = TypeStatus.Healthy;
        playerMovement = gameObject.GetComponent<PlayerMovement2D>();
    }

    public void Update()
    {
        CheckStatus();
    }

    public void CheckStatus()
    {
        // Poisoned
        if (typeStatus == TypeStatus.Poisoned)
        {
            Debug.Log("Envenenado");

            StartCoroutine(PoisonCo());

        }

        // Eletrocuted
        if (typeStatus == TypeStatus.Electrocuted)
        {
            Debug.Log("Eletrocutado");

            StartCoroutine(ElectructedCo());

        }

        // Burning
        if (typeStatus == TypeStatus.Burning)
        {
            Debug.Log("Queimando");

            StartCoroutine(BurningCo());

        }

        // Frozen
        if (typeStatus == TypeStatus.Frozen)
        {
            Debug.Log("Congelado");

            StartCoroutine(FrozenCo());

        }

        // Healthy
        if (typeStatus == TypeStatus.Healthy)
        {
            StopAllCoroutines();
        }

    }


    // ---------  Rotinas  ---------

    public IEnumerator PoisonCo()
    {
        gameObject.TryGetComponent<HealthController>(out HealthController playerHealthComp);

        for (int i = 0; i < 9; i++)
        {
            damageType = DamageType.Physic;

            playerHealthComp.TakeDamage(1, 0, damageType);

            // Codigo de deixa o personagem verde
            // Audio de Poison
            // Codigo que Aparece o Icone no HealthBar

            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(10);
        typeStatus = TypeStatus.Healthy;
    }


    public IEnumerator ElectructedCo()
    {
        gameObject.TryGetComponent<HealthController>(out HealthController playerHealthComp);

        damageType = DamageType.Electric;

        for (int i = 0; i < 7; i++)
        {
            playerHealthComp.TakeDamage(2, 0, damageType);

            // Codigo de deixa o personagem verde
            // Audio de Poison
            // Codigo que Aparece o Icone no HealthBar

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(10);
        typeStatus = TypeStatus.Healthy;
    }


    public IEnumerator BurningCo()
    {
        gameObject.TryGetComponent<HealthController>(out HealthController playerHealthComp);

        damageType = DamageType.Fire;

        for (int i = 0; i < 15; i++)
        {
            playerHealthComp.TakeDamage(1, 0, damageType);

            // Codigo de deixa o personagem Vermelho
            // Audio de Burn
            // Codigo que Aparece o Icone no HealthBar

            yield return new WaitForSeconds(0.6f);
        }

        yield return new WaitForSeconds(10);
        typeStatus = TypeStatus.Healthy;
    }


    public IEnumerator FrozenCo()
    {
        gameObject.TryGetComponent<HealthController>(out HealthController playerHealthComp);
        gameObject.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehaviourComp);

        damageType = DamageType.Ice;

        playerHealthComp.TakeDamage(10, 0, damageType);

        playerMovement.isPaused = true;

        // Codigo de deixa o personagem Congelado
        // Audio de Frozen
        // Codigo que Aparece o Icone no HealthBar

        yield return new WaitForSeconds(10);
        typeStatus = TypeStatus.Healthy;
    }
}


