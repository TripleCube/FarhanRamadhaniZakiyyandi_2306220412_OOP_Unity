using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(HitboxComponent))]
public class InvincibilityComponent : MonoBehaviour
{
    [SerializeField] private int blinkingCount = 7;
    [SerializeField] private float blinkInterval = 0.1f;
    [SerializeField] private Material blinkMaterial;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    public bool isInvincible = false;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    // Method to activate invincibility and start blinking effect
    public void ActivateInvincibility()
    {
        if (!isInvincible)
        {
            StartCoroutine(InvincibilityBlinkingRoutine());
        }
    }

    // Coroutine for the blinking effect
    private IEnumerator InvincibilityBlinkingRoutine()
    {
        isInvincible = true;

        for (int i = 0; i < blinkingCount; i++)
        {
            // Toggle the material for blinking effect
            spriteRenderer.material = blinkMaterial;
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.material = originalMaterial;
            yield return new WaitForSeconds(blinkInterval);
        }

        isInvincible = false; // End of invincibility period
    }
}
