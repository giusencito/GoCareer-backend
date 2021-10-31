Feature: US05
	 Notificaciones a especialistas

@mytag
Scenario: El estudiante envía un mensaje
	Given el estudiante ingrese a la plataforma
	When dé click al botón de especialistas
	And seleccione un especialista
	Then se podrá escribir un mensaje a este