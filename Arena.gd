extends Node2D

onready var characters = $Characters

func _ready():
	Globals.arena = self

func add_character(character : Character):
	characters.append(character)
