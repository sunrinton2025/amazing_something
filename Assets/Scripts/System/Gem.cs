using minyee2913.Utils;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public HealthObject healthObject;
    public SpriteRenderer spriteRenderer;
    public Sprite dropped;
    void Start()
    {
        healthObject = this.GetComponent<HealthObject>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        checkHealth();
    }
    void checkHealth()
    {
        if (healthObject.Health <= 0)
        {
            this.spriteRenderer.sprite = dropped;
        }
    }
}
