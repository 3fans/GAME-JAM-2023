extends Control


# Called when the node enters the scene tree for the first time.
func _ready():
	load("res://Game Jam/First_Track.mp3");


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass


func _on_start_button_up():
	get_tree().change_scene_to_file("res://lobby.tscn");


func _on_options_button_up():
	var options;


func _on_quit_button_up():
	pass # Replace with function body.
