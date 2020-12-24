extends Node2D

export var cooldown := 0.2
export var attack_duration := 0.2
export var radius := 8.0

onready var attack_animator = $AttackAnimator
onready var tween = $Tween
onready var damage_area = $DamageArea

var swipe = 1.0

func update_position(look : Vector2):
	pass
#	var angle_offset = PI / 2.0 * swipe
#	position = look.rotated(angle_offset) * radius
#
#	var rotation_angle_offset = PI / 4.0 * swipe
#	rotation = position.rotated(rotation_angle_offset).angle()

func attack():
	if swipe > 0:
		attack_animator.play("swipe_right")
	else:
		attack_animator.play("swipe_left")
	
	# switch side to swipe from
	swipe = -swipe
		
#	tween.interpolate_property(self, "swipe", swipe, -swipe, attack_duration, Tween.TRANS_CIRC, Tween.EASE_IN_OUT)
#	tween.interpolate_callback(damage_area.get_node("CollisionShape2D"), attack_duration, "set", "disabled", true)
#	tween.start()
#	damage_area.get_node("CollisionShape2D").disabled = false

func skill():
	pass
	# whirlwind
#	tween.interpolate_property(self, "swipe", -1, -1.5, 0.1, Tween.TRANS_CIRC, Tween.EASE_IN_OUT)
#	tween.interpolate_property(self, "swipe", -1.5, 4, 0.4, Tween.TRANS_CIRC, Tween.EASE_IN_OUT, 0.2)
#	tween.start()
	
