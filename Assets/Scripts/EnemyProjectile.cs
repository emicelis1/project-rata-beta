using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 12f;
    public float lifetime = 4f;
    private Vector3 moveDirection;
    private int damageToDeal;

    public void Setup(Vector3 direction, int damage)
    {
        moveDirection = direction;
        damageToDeal = damage;
        // Se destruye automáticamente después de unos segundos si no choca con nada
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Mueve el proyectil en la dirección calculada
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si golpea al jugador
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.DamagePlayer(damageToDeal);
            Destroy(gameObject); // Destruir la bala tras impactar
        }

        // Opcional: Descomentar si querés que choque con paredes estáticas del mapa
        // if (other.gameObject.CompareTag("Obstaculo")) { Destroy(gameObject); }
    }
}