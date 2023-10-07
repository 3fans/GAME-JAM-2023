extends CharacterBody2D

var targeted_entity = null;
var direction
var speed : int = 15
var state = "wander";
var wander_state = "idle"
var rotation_timer = 0;
var idle_timer = 0;
var move_timer = 0;
var rotation_rand = 1;
# Called when the node enters the scene tree for the first time.
func _ready():
	pass

# Called every frame. 'delta' is the elapsed time sinace the previous frame.
func _process(delta):
	if state == "chase":
		rotation = global_position.angle_to_point(targeted_entity.global_position)  + deg_to_rad(90)
		velocity = (Vector2(sin(rotation),(-1) * cos(rotation)) * speed)
		move_and_slide()
	if state == "wander":
		if (wander_state == "moving") and (move_timer <= 0):
			rotation_timer = (randf()*2)+1
			wander_state = "rotating"
			rotation_rand = randi_range(1,3) - 2
		elif rotation_timer > 0:
			rotate(deg_to_rad((50 * rotation_rand)) * delta)
			rotation_timer -= delta
		elif (rotation_timer <= 0) and (wander_state == "rotating"):
			idle_timer = (randf()*2)+1
			wander_state = "idle"
		elif idle_timer > 0:
			idle_timer -= delta
		elif (idle_timer <= 0) and (wander_state == "idle"):
			move_timer = (randf()*2)+1
			wander_state = "moving"
		elif move_timer > 0:
			move_timer -= delta
			velocity = (Vector2(sin(rotation),(-1) * cos(rotation)) * speed)
			move_and_slide()
		else:
			print("error else")
			
		
		var rotate_time = randf() * 2
		
		rotate(deg_to_rad(15) * delta)

func _on_vision_body_exited(body):
	if body.get_collision_mask_value(5):
		if targeted_entity == body:
			targeted_entity = null
			state = "wander"
		

func _on_alert_body_entered(body):
	if body.get_collision_mask_value(5):
		if targeted_entity == null:
			targeted_entity = body
			state = "chase"
			
