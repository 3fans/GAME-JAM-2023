extends CharacterBody2D
var health : int = 15
var speed : int = 60
var targeted_entity = null;
var direction
var state = "wander";
var wander_state = "idle"
var rotation_timer = 0;
var idle_timer = 0;
var move_timer = 0;
var rotation_rand = 1;
var damage_time;
var target_position;
var animate;
var sound
var death_timer = 0
# Called when the node enters the scene tree for the first time.
func _ready():
	animate = $snake_anim
	sound = $yoda_death

# Called every frame. 'delta' is the elapsed time sinace the previous frame.
func _physics_process(delta):
	if death_timer > 0:
		death_timer -= delta
	elif health <= 0:
		Global.sandwich += 1
		queue_free()
	if state == "chase":
		animate.play("move")
		rotation = global_position.angle_to_point(targeted_entity.global_position)  + deg_to_rad(-90)
		velocity = (Vector2(sin(rotation)*(-1),cos(rotation)) * speed)
		move_and_slide()
	if state == "wander":
		animate.play("move")
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
			velocity = (Vector2(sin(rotation)*(-1),cos(rotation)) * speed)
			move_and_slide()
		else:
			print("error else")
		rotate(deg_to_rad(15) * delta)
	if state == "damage" && damage_time > 0:
		velocity = velocity.lerp(target_position, .1)
		move_and_slide()
		damage_time -= delta
	elif state == "damage" && damage_time <= 0:
		state = "chase"
		
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



func _on_hit_box_area_entered(area):
	if area.get_collision_mask_value(9):
		health -= 1;
		state = "damage"
		damage_time = .3
		target_position = (position - area.global_position).normalized()
		velocity = target_position * speed * 15;
		velocity = target_position * speed;
		animate.play("damage")
	if health <= 0 and death_timer <= 0:
		sound.play()
		death_timer = .5
	

