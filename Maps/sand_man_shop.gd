extends Node2D

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass


func _on_sand_button_up():
	if Global.sandwich >= 1:
		Global.sandwich -= 1
		Global.start_time +=5


func _on_buy_dash_button_up():
	Global.can_dash = true


func _on_back_to_lobby_button_up():
	get_tree().change_scene_to_file("res://lobby.tscn")


func _on_reduce_dash_time_button_up():
	if Global.dash_time >= .5 and Global.sandwich >= 4:
		Global.dash_time -= .1
		Global.sandwich -= 4
