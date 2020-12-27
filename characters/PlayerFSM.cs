using Godot;
using System;
using System.Collections.Generic;

public class PlayerFSM : StateMachine
{
	private const double STOP_THRESHOLD = 8.0;

	public override void _Ready()
	{
		base._Ready();

		State = States.IDLE;
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);

		if (@event.IsActionPressed("dodge"))
		{
			
		}
	}

	public override States GetTransition(float delta)
	{
		States state = States.IDLE;
		switch (State)
		{
			case States.IDLE:
				if (Parent.Velocity.Length() >= STOP_THRESHOLD)
				{
					return States.RUN;
				}
				break;
			case States.RUN:
				if (Parent.Velocity.Length() < STOP_THRESHOLD)
				{
					return States.IDLE;
				}
				break;
		}

		return state;
	}

	public override void EnterState(States newState, States oldState)
	{
		GD.Print(newState);
	}
}
