Feature: US10
	Ir a cita

@mytag
Scenario: El especialista va a la cita
	Given el especialista ingrese a la plataforma
	When esté en su perfil
	And selecciona la opción de reserva de consultas
	And seleccione la cita acordada
	Then podrá estar en una reunión con el estudiante