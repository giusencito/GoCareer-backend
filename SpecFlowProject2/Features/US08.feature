Feature: US08
	Respuesta al especialista

@mytag
Scenario: El especialista hace una repuesta
	Given el especialista ingrese a la plataforma
	When le dé click al botón de mensajería
	And elija un mensaje
	Then se podrá redactar una respuesta a este