extends KinematicBody2D
class_name Character

enum Factions {ALLY, ENEMY}

const KNOCKBACK_VELOCITY = 350

export var movespeed_units := 8.0
export var max_health := 100
export var knockback_modifier := 1.0
export (Factions) var faction = 0

onready var damage_animator = $DamageAnimator
onready var attack_cooldown = $Timers/AttackCooldown
onready var hitbox = $Hitbox
onready var pivot = $Pivot

onready var health = max_health setget set_health

var velocity := Vector2()
var move_input := Vector2()
var look := Vector2()

var weapon = null
var target = null setget set_target,get_target

func _physics_process(delta):
	pivot.rotation = (get_global_mouse_position() - pivot.global_position).angle()
#	if weapon != null:
#		weapon.update_position(look)
	
func _apply_movement():
	velocity = move_and_slide(velocity)	
	
func move_towards(to):
	var displacement = to - global_position
	var normal = displacement.normalized()
	velocity = lerp(velocity, normal * movespeed_units * Globals.UNIT_SIZE, get_move_weight())
	
func get_move_weight():
	return 0.2
	
func get_ideal_target():
	var potential_targets = []
	var nearest = null
	var distance = INF

	var characters = Globals.arena.characters
	for character in characters.get_children():
		if character.faction == Factions.ALLY:
			var distanceTo = (character.global_position - global_position).length()
			if distanceTo < distance:
				nearest = character
				distance = distanceTo
				
	return nearest

func _on_Hitbox_damaged(amount, knockback_strength, damage_source, attacker):
	self.health -= amount
	
	#	knockback
	if damage_source != null:
		knockback(knockback_strength, damage_source.global_position)
	
	damage_animator.play("damage")
	if hitbox.immunity_duration != 0:
		damage_animator.queue("invulnerability")

func knockback(knockback_strength, knockback_source):
	var normal = (global_position - knockback_source).normalized()
	
	if knockback_modifier != 0:
		velocity = knockback_strength * normal * knockback_modifier * KNOCKBACK_VELOCITY

func set_target(value):
	if value is WeakRef:
		target = value
	else:
		target = weakref(value)

func get_target():
	var temp = null
	if target is WeakRef:
		temp = target.get_ref()
	
	return temp

func set_health(value):
	health = clamp(value, 0, max_health)
	if health <= 0:
		die()

func die():
	queue_free()


func _on_Hitbox_immunity_ended():
	damage_animator.play("reset")
