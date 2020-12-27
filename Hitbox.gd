extends Area2D

signal damaged(amount, knockback_strength, damage_source, attacker)
signal immunity_started()
signal immunity_ended()

onready var immunity_timer = $ImmunityTimer
onready var parent = get_parent()

export var immunity_duration := 0.0

var exceptions = []

func add_exeptions(node : Node2D):
	if node != null:
		exceptions.append(node)
		node.connect("tree_exiting", self, "remove_exeption", [node])
		
func remove_exeption(node : Node2D):
	if node in exceptions:
		exceptions.erase(node)

func _on_Hitbox_area_entered(area):
	# don't damage if
	# we want to skip this hitbox or we are immune
	if area in exceptions || immunity_timer.is_stopped():
		return

	# or if we ignore our own hitbox
	if area is DamageArea:
		if !(self in area.exceptions):
			damage(area.get_base_damage() + area.damage_amount, area.knockback_strength, area, area.attacker)

func damage(amount, knockback_strength, source, attacker):
	emit_signal("damaged", amount, knockback_strength, source, attacker)
	if immunity_duration > 0:
		immunity_timer.start(immunity_duration)


func _on_ImmunityTimer_timeout():
	emit_signal("immunity_ended")
