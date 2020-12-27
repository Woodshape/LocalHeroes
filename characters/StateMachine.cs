using Godot;
using System;
using Godot.Collections;

public class StateMachine : Node
{
	public enum States
	{
		IDLE,
		RUN,
		DODGE
	}
	
	protected PlayableCharacter Parent;

	private States state;
	public States State
	{
		get => state;
		set
		{
			PreviousState = state;
			state = value;

			if (PreviousState != null)
			{
				ExitState(PreviousState, state);
			}
			if (state != null)
			{
				EnterState(state, PreviousState);
			}
		}
	}
	
	protected States PreviousState;

	public override void _Ready()
	{
		base._Ready();

		this.Parent = GetParent<PlayableCharacter>();
	}

	public override void _PhysicsProcess(float delta)
	{
		base._PhysicsProcess(delta);
		
		if (state != null)
		{
			StateLogic(delta);
			States transition = GetTransition(delta);
			if (transition != null)
			{
				State = state;
			}
		}
	}

	public virtual void StateLogic(float delta) { }

	public virtual States GetTransition(float delta)
	{
		return States.IDLE;
	}

	public virtual void EnterState(States newState, States oldState) { }
	
	public virtual void ExitState(States oldState, States newState) { }
}
