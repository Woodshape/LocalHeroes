extends Character

const HAND_RADIUS = 24

func _ready():
	weapon = $Pivot/Weapon

func _physics_process(delta):
	move_input.x = -Input.get_action_strength("move_left") + Input.get_action_strength("move_right")
	move_input.y = -Input.get_action_strength("move_up") + Input.get_action_strength("move_down")
	move_input = move_input.normalized()
	
	velocity = lerp(velocity, move_input * movespeed_units * Globals.UNIT_SIZE, get_move_weight())
	velocity = move_and_slide(velocity)
	
	look = (get_global_mouse_position() - global_position).normalized()

func _unhandled_input(event):
	if(event.is_action_pressed("attack") && attack_cooldown.is_stopped()):
		attack()
	elif(event.is_action_pressed("skill") && attack_cooldown.is_stopped()):
		skill_attack()

func attack():
	if weapon != null:
		attack_cooldown.start(weapon.cooldown)
		weapon.attack()
		
#	var attack_area = preload("res://attack/AttackArea.tscn").instance()
#	add_child(attack_area)
#
#	var mouse = get_global_mouse_position() - global_position
#	attack_area.position = mouse.normalized() * HAND_RADIUS
#	attack_area.set_attacker(self)

func skill_attack():
	if weapon != null:
		attack_cooldown.start(weapon.cooldown)
		weapon.skill()
