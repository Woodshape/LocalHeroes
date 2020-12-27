extends StateMachine

const STOP_THRESHOLD = 8.0

func _ready():
	add_state("idle")
	add_state("run")
	add_state("dodge")
	
	call_deferred("set_state", states.idle)

func _input(event):
	if event.is_action_pressed("dodge"):
		# check if state is IDLE or RUN
		if [states.idle, states.run].has(state):
			set_state(states.dodge)

func _state_logic(delta):
	match state:
		states.idle, states.run:
			parent.update_move_input()
		states.dodge:
			parent.dodge()
	
	parent.update_velocity()
	parent.update_facing()
	parent.apply_movement()

func _get_transition(delta):
	match state:
		states.idle:
			if parent.velocity.length() >= STOP_THRESHOLD:
				return states.run
		states.run:
			if parent.velocity.length() < STOP_THRESHOLD:
				return states.idle
		states.dodge:
			if parent.dodge_cooldown.is_stopped():
				return states.idle

func _enter_state(new_state, old_state):
	match new_state:
		states.dodge:
			parent.dodge_cooldown.start(0.3)
			parent.animator.play("dodge")

func _exit_state(old_state, new_state):
	pass
