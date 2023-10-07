extends Area2D
var local_timer = 0

# Called when the node enters the scene tree for the first time.
func _ready():
	monitoring = false
	monitorable = false


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if monitoring == true:
		local_timer -= delta
		if local_timer <= 0:
			monitoring = false
			monitorable = false
	


func _on_playerv_1_attack(attack_direction):
	if attack_direction == "left":
		monitoring = true
		monitorable = true
		local_timer = .25
		
		
