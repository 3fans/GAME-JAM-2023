extends Node

var start_time = 15
var sandwich = 0
var can_dash : bool = false
var dash_time : float = .8
var total_time = 0

func _process(delta):
	total_time += delta
