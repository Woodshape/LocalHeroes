extends Node2D

onready var timer = $Timer

func _on_Timer_timeout():
	queue_free()
