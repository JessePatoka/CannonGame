using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    public float explosion_rate = 1f;
    public float explosion_max = 10f;
    public float explosion_speed = 1f;
    public float current_radius = 0f;

    public GameObject Explosion;

    bool exploded = false;
    CircleCollider2D explosion_radius;
    // Use this for initialization
    void Start()
    {
        explosion_radius = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(exploded)
        {
            if (current_radius < explosion_max)
            {
                current_radius += explosion_rate;
            }
            else
            {
                PlayExplosion();
                Object.Destroy(this.gameObject.transform.parent.gameObject);
            }

            explosion_radius.radius = current_radius;

        }
    }
    private void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);
        explosion.transform.position = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.StartsWith("Player"))
        {
            exploded = true;
            Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidbody != null)
            {
                Vector2 target = other.gameObject.transform.position;
                Vector2 bomb = gameObject.transform.position;
                Vector2 direction = 1000f * (target - bomb);

                Rigidbody2D otherRigidbody = other.GetComponent<Rigidbody2D>();
                otherRigidbody.AddForce(direction);
            }
        }
    }
}
