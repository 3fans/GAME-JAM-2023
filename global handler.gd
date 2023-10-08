extends Node
signal on_load(can_dash, dash_time)
func _ready():
	emit_signal("on_load",Global.can_dash, Global.dash_time)
