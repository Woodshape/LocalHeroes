using Godot;
using System;

public class Character : KinematicBody2D
{
	public static int PIXELS = 24;

	[Export]
	protected float movespeed_units = 8.0f;

	protected Timer attackCooldown;
	protected Vector2 _velocity;
	protected Vector2 _moveInput;

	public override void _Ready()
	{
		base._Ready();

		attackCooldown = GetNode<Timer>("Timers/AttackCooldown");
	}
	
	private void _onHitboxDamaged(int amount)
	{
	// Replace with function body.
	}
}



