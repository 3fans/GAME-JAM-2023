extends Label

signal timer_depleted

var player_time : float = Global.start_time
# Called when the node enters the scene tree for the first time.
func _ready():
	text = str(int(player_time))
	


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta): 
	player_time -= delta
	text = str(int(player_time))
	if player_time <= 0:
		timer_depleted.emit()


func _on_area_2d_body_entered(body):
	if body.get_visibility_layer_bit(6):
		text = str(int(Global.start_time))
		set_process(false)
	else:
		set_process(true)


func _on_playerv_1_damage():
	player_time -= 5
