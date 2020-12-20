extends KinematicBody2D
class_name Character

const PIXEL_UNITS = 24

onready var attack_cooldown = $Timers/AttackCooldown

export var movespeed_units := 8.0

var velocity := Vector2()
var move_input := Vector2()


func _on_Hitbox_damaged(amount, knockback_strength, damage_source, attacker):
	pass # Replace with function body.
