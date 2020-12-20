extends KinematicBody2D
class_name Character

const PIXEL_UNITS = 24

export var movespeed_units := 8.0
export var max_health := 10.0

onready var attack_cooldown = $Timers/AttackCooldown
onready var health = max_health setget set_health

var velocity := Vector2()
var move_input := Vector2()


func _on_Hitbox_damaged(amount, knockback_strength, damage_source, attacker):
	var temp = health - amount
	var animation = $DamageAnimator
	animation.play("damage")
	set_health(temp)

func set_health(value):
	health = value
	if health <= 0:
		die()
	elif health > max_health:
		health = max_health

func die():
	queue_free()
