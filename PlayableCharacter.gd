extends Character

func _input(event):
	if(event.is_action_pressed("attack") && attack_cooldown.is_stopped()):
		attack()

func attack():
	attack_cooldown.start()
	print('attack')
