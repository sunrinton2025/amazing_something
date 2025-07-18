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

        healthObject.OnDamageFinal(onHurtFinal);
    }

    void onHurtFinal(HealthObject.OnDamageFinalEv ev) {
        IndicatorManager.Instance.GenerateText(ev.Damage.ToString(), transform.position + new Vector3(Random.Range(-2, 2), Random.Range(1, 2)), Color.cyan);
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
