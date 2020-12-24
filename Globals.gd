extends Node

const UNIT_SIZE = 24

var player = null
var time = 0

var level = null
var arena = null

func _ready():
	pass

func _process(delta):
	time += delta
