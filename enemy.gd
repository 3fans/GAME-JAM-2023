extends CharacterBody2D

var i = 1
var targeted_entity = null;
var direction

var state = "wander";
# Called when the node enters the scene tree for the first time.
func _ready():
	pass

# Called every frame. 'delta' is the elapsed time sinace the previous frame.
func _process(delta):
	if state == "chase" and i == 0:
		rotation = global_position.angle_to_point(targeted_entity.global_position)  + deg_to_rad(90)
		velocity = (Vector2(sin(rotation),(-1) * cos(rotation)) * 10)
		move_and_slide()

func _on_vision_body_exited(body):
	print("exit")
	if targeted_entity == body:
		targeted_entity = null
		state = "wander"
		print("wander")
		

func _on_alert_body_entered(body):
	print("entered")
	if targeted_entity == null:
		targeted_entity = body
		state = "chase"
		print("chase")
	if i == 1:
		i = 0
		targeted_entity = null
		state = "wander"
