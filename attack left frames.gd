extends Sprite2D

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass


func _on_playerv_1_attack(attack_direction):
	if attack_direction == "left":
		visible = true


func _on_playerv_1_moving():
	visible = false
