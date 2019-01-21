DELETE FROM User;
DELETE FROM Rating;

INSERT INTO User(login) VALUES
('superhero'),
('kryminal'),
('kociarz'),
('sport'),
('szpital-komedia')
;

INSERT INTO Rating(user_login, programme_id, rating_value) VALUES
('superhero', 2228, 1),
('superhero', 4125, 1),
('superhero', 4812, 1),
('superhero', 8719, 0),
('kryminal', 4571, 1),
('kryminal', 4572, 1),
('kryminal', 4573, 1),
('kryminal', 4574, 1),
('kryminal', 4575, 1),
('kryminal', 4579, 1),
('kryminal', 4580, 1),
('kryminal', 4581, 1),
('kryminal', 4582, 1),
('kryminal', 4587, 1),
('kociarz', 9061, 1),
('kociarz', 11243, 1),
('kociarz', 11265, 1),
('kociarz', 467, 1),
('sport', 3802, 1),
('sport', 3805, 1),
('sport', 1745, 1),
('sport', 3844, 1),
('sport', 12582, 1),
('szpital-komedia', 8846, 1),
('szpital-komedia', 2057, 1),
('szpital-komedia', 2217, 1),
('szpital-komedia', 4215, 1),
('szpital-komedia', 8529, 1),
('szpital-komedia', 13843, 1)
;