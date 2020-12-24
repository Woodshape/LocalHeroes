extends Enemy

func _physics_process(delta):
	var tar = get_target()
	if tar == null:
		tar = get_ideal_target()
		
	if tar != null:
		move_towards(tar.global_position)
		
	#_apply_movement()	
