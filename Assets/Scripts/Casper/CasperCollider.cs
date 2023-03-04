using UnityEngine;

public class CasperCollider : MonoBehaviour
{
	private CasperController casperController;

	void Start()
	{
		casperController = GetComponentInParent<CasperController>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Fener"))
		{
			casperController.Dead();
		}
	}
}
