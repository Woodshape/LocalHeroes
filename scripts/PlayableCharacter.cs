using Godot;
using System;

public class PlayableCharacter : Character
{
	private PackedScene attackAreaScene = (PackedScene)GD.Load("res://attack/AttackArea.tscn");

	public override void _UnhandledInput(InputEvent @event)
	{
		base._UnhandledInput(@event);

		if (@event.IsActionPressed("attack") && attackCooldown.IsStopped())
		{
			Attack();
		}
	}
	
	public override void _PhysicsProcess(float delta)
	{
		_moveInput.x = -Input.GetActionStrength("move_left") + Input.GetActionStrength("move_right");
		_moveInput.y = -Input.GetActionStrength("move_up") + Input.GetActionStrength("move_down");

		_moveInput.Normalized();

		_velocity = _moveInput * movespeed_units * PIXELS;
		_velocity = MoveAndSlide(_velocity);
	}

	private void Attack()
	{
		attackCooldown.Start();

		var attackArea = attackAreaScene.Instance();

		AddChild(attackArea);
	}
}
