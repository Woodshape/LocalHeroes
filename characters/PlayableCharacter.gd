extends Character

const HAND_RADIUS = 24

func _ready():
	weapon = $Pivot/Weapon
	damage_animator.play("reset")

func _unhandled_input(event):
	if(event.is_action_pressed("attack") && attack_cooldown.is_stopped()):
		attack()
	elif(event.is_action_pressed("skill") && attack_cooldown.is_stopped()):
		skill_attack()

func update_facing():
	facing = (get_global_mouse_position() - global_position).normalized()

func update_velocity():
	velocity = lerp(velocity, move_input * movespeed * Globals.UNIT_SIZE, get_move_weight())

func update_move_input():
	move_input.x = -Input.get_action_strength("move_left") + Input.get_action_strength("move_right")
	move_input.y = -Input.get_action_strength("move_up") + Input.get_action_strength("move_down")
	move_input = move_input.normalized()
	
	if move_input.length() != 0:
		direction = move_input

func dodge():
	update_move_input()
	velocity = direction * dodgespeed * Globals.UNIT_SIZE

func attack():
	if weapon != null:
		attack_cooldown.start(weapon.cooldown)
		weapon.attack()

func skill_attack():
	if weapon != null:
		attack_cooldown.start(weapon.cooldown)
		weapon.skill()
