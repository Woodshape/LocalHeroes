using Godot;
using System;

public class Hitbox : Node2D
{

	[Signal]
	delegate void damaged(int amount, int knockback_strength);

	private void Damage()
	{
		EmitSignal(nameof(damaged), 12);
	}
}
