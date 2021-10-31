Feature: US01
	Registro en la plataforma

@scopeBinding
Scenario: El estudiante desea crear una cuenta nueva
	Given el estudiante ingrese a la plataforma
	When elija una opción de registro
	And llene sus datos
	Then se creara la cuenta



