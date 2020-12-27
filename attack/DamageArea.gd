extends Area2D
class_name DamageArea

signal hit()

export var damage_amount = 10
export var knockback_strength = 1
export var use_exceptions = false
export (NodePath) var attacker = null

var exceptions = []

func _ready():
	if attacker != null:
		attacker = get_node_or_null(attacker)

func on_hit(hitbox):
	if use_exceptions:
		exceptions.append(hitbox)
	emit_signal("hit")

func get_base_damage():
	var damage = 0
	if attacker != null && attacker is Character:
		damage = attacker.base_damage
		
	return damage	
