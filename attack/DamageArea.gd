extends Area2D
class_name DamageArea

signal hit()

export var damage_amount = 1
export var knockback = 1
export var use_exceptions = false

var attacker
var exceptions = []

func on_hit(hitbox):
	if use_exceptions:
		exceptions.append(hitbox)
	emit_signal("hit")
