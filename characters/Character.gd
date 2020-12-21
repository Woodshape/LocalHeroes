extends KinematicBody2D
class_name Character

const PIXEL_UNITS = 24

export var movespeed_units := 8.0
export var max_health := 10.0

onready var damage_animator = $DamageAnimator
onready var attack_cooldown = $Timers/AttackCooldown
onready var hitbox = $Hitbox

onready var health = max_health setget set_health

var velocity := Vector2()
var move_input := Vector2()


func _on_Hitbox_damaged(amount, knockback_strength, damage_source, attacker):
	self.health -= amount
	damage_animator.play("damage")
	if hitbox.immunity_duration != 0:
		damage_animator.queue("invulnerability")

func set_health(value):
	health = clamp(value, 0, max_health)
	if health <= 0:
		die()

func die():
	queue_free()


func _on_Hitbox_immunity_ended():
	damage_animator.play("reset")
