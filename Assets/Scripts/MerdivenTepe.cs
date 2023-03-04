using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerdivenTepe : MonoBehaviour
{

	public EdgeCollider2D platform;


	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.transform.name.Equals("PlayerHuman"))
		{
			platform.enabled = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.transform.name.Equals("PlayerHuman"))
		{
			platform.enabled = false;
		}
	}
}
