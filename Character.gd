extends KinematicBody2D
class_name Character

onready var attack_cooldown = $Timers/AttackCooldown

export var movespeed_units := 8.0

var velocity := Vector2()
var move_input := Vector2()

func _physics_process(delta):
	move_input.x = -Input.get_action_strength("move_left") + Input.get_action_strength("move_right")
	move_input.y = -Input.get_action_strength("move_up") + Input.get_action_strength("move_down")
	move_input = move_input.normalized()
	
	velocity = move_input * movespeed_units * 24
	velocity = move_and_slide(velocity)
