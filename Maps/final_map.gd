extends Node2D


func _on_final_body_entered(body):
		if body.get_visibility_layer_bit(1):
			get_tree().change_scene_to_file("res://Maps/final_screen.tscn")
