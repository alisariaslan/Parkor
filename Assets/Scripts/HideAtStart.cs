using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAtStart : MonoBehaviour
{
	private SpriteRenderer sprite_render;
	public bool hide_at_start = true;
	// Start is called before the first frame update
	void Start()
	{
		sprite_render = GetComponent<SpriteRenderer>();
		if (hide_at_start)
			GameObject.Destroy(sprite_render);
	}
}
