using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 10;
    
    public string triggerAttack = "Attack";
    public string triggerDeath = "Death";

    public Animator animator;
    public HealthBase healthBase;

    private void Awake()
    {
        if (healthBase != null)
            healthBase.OnKill += OnEnemyKill;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.transform.name);

        var health = collision.gameObject.GetComponent<HealthBase>();

        if (health != null)
        {
            health.Damage(damage);
            PlayAttackAnimation();
        }
            
    }

    private void OnEnemyKill()
    {
        healthBase.OnKill -= OnEnemyKill;
        gameObject.GetComponent<Collider2D>().enabled = false;
        PlayKillAnimation();
    }

    private void PlayAttackAnimation()
    {
        animator.SetTrigger(triggerAttack);
    }

    private void PlayKillAnimation()
    {
        animator.SetTrigger(triggerDeath);
    }

    public void Damage(int amount)
    {
        healthBase.Damage(amount);
    }
}
