extends Character

func _input(event):
	if(event.is_action_pressed("attack") && attack_cooldown.is_stopped()):
		print("attack")
		attack_cooldown.start()
