extends Node2D

export var cooldown := 0.2
export var attack_duration := 0.2
export var skill_duration := 1.0
export var radius := 8.0

onready var attack_animator = $AttackAnimator
onready var tween = $Tween
onready var damage_area = $Pivot/DamageArea
onready var pivot = $Pivot

var is_skill_active = false
var active_skill_time = 0.0
var swipe_direction = -1.0

func _ready():
	rotation = -PI / 2.0
	pivot.rotation = -PI / 3.0
	
func _process(delta):
	if is_skill_active:
		active_skill_time += delta
		if active_skill_time > skill_duration:
			attack_animator.get_animation("whirlwind").loop = false
			is_skill_active = false
			active_skill_time = 0.0

func update_position(look : Vector2):
	rotation = look.angle()
#	var angle_offset = PI / 2.0 * swipe
#	position = look.rotated(angle_offset) * radius
#	var rotation_angle_offset = PI / 4.0 * swipe
#	rotation = position.rotated(rotation_angle_offset).angle()

func attack():
	if !is_skill_active:
		if swipe_direction > 0:
			attack_animator.play("swipe_right")
		else:
			attack_animator.play("swipe_left")

	#	# switch side to swipe from
		swipe_direction = -swipe_direction
		
#	tween.interpolate_property(self, "swipe", swipe, -swipe, attack_duration, Tween.TRANS_CIRC, Tween.EASE_IN_OUT)
#	tween.interpolate_callback(damage_area.get_node("CollisionShape2D"), attack_duration, "set", "disabled", true)
#	tween.interpolate_property(self, "rotation", PI / 2.0 * sign(swipe_direction), PI / 2.0 * -sign(swipe_direction), attack_duration, Tween.TRANS_CIRC, Tween.EASE_IN_OUT)
#	tween.interpolate_property(pivot, "rotation", PI / 3.0 * sign(swipe_direction), PI / 3.0 * -sign(swipe_direction), attack_duration, Tween.TRANS_CIRC, Tween.EASE_IN_OUT)
#	tween.start()
#	swipe_direction = -swipe_direction
#	damage_area.get_node("CollisionShape2D").disabled = false

func skill():
	# whirlwind - can hit enemies multiple times (collision shape get's enabled and disabled)
	if !is_skill_active:
		attack_animator.play("whirlwind")
		attack_animator.get_animation("whirlwind").loop = true
		is_skill_active = true
	
#	tween.interpolate_property(self, "swipe", -1, -1.5, 0.1, Tween.TRANS_CIRC, Tween.EASE_IN_OUT)
#	tween.interpolate_property(self, "swipe", -1.5, 4, 0.4, Tween.TRANS_CIRC, Tween.EASE_IN_OUT, 0.2)
#	tween.start()
	
