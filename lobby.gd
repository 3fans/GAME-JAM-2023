extends Node2D


func _on_area_2d_body_entered(body):
	if body.get_visibility_layer_bit(1):
		get_tree().change_scene_to_file("res://Maps/pro_gen_lvl_1.tscn")


func _on_sand_man_area_body_entered(body):
	if body.get_visibility_layer_bit(1):
		get_tree().change_scene_to_file("res://Maps/sand_man_shop.tscn")
