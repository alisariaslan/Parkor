using UnityEngine;

public class CarRoofCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            GetComponentInParent<CarController>().Damage(1000);
            Destroy(this.gameObject);
        }
    }
}
