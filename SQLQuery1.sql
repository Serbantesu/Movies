CREATE DATABASE JCervantesExample

USE JCervantesExample

CREATE TABLE Usuario(
IdUsuario INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50),
APaterno VARCHAR(50),
AMaterno VARCHAR(50),
Edad INT,
Telefono VARCHAR(10),
Correo VARCHAR(50)
);

CREATE PROCEDURE UsuarioGetAll
AS
SELECT 
	IdUsuario, 
	Nombre, 
	APaterno, 
	AMaterno, 
	Edad,
	Telefono, 
	Correo
FROM Usuario

CREATE PROCEDURE UsuarioGetById
@IdUsuario INT
AS
SELECT
	IdUsuario, 
	Nombre, 
	APaterno, 
	AMaterno, 
	Edad,
	Telefono, 
	Correo
FROM Usuario 
WHERE IdUsuario = @IdUsuario

CREATE PROCEDURE UsuarioAdd
@Nombre VARCHAR(50),
@APaterno VARCHAR(50),
@AMaterno VARCHAR(50),
@Edad INT,
@Telefono VARCHAR(10),
@Correo VARCHAR(50)
AS
INSERT INTO Usuario(
	Nombre, 
	APaterno, 
	AMaterno, 
	Edad,
	Telefono, 
	Correo)
VALUES(
	@Nombre,
	@APaterno, 
	@AMaterno, 
	@Edad, 
	@Telefono, 
	@Correo)

CREATE PROCEDURE UsuarioUpdate
@IdUsuario INT,
@Nombre VARCHAR(50),
@APaterno VARCHAR(50),
@AMaterno VARCHAR(50),
@Edad INT,
@Telefono VARCHAR(10),
@Correo VARCHAR(50)
AS
UPDATE Usuario
	SET
	Nombre = @Nombre,
	APaterno = @APaterno,
	AMaterno = @AMaterno, 
	Edad = @Edad, 
	Telefono = @Telefono, 
	Correo = @Correo
WHERE IdUsuario = @IdUsuario

CREATE PROCEDURE UsuarioDelete
@IdUsuario INT
AS
DELETE FROM Usuario
WHERE IdUsuario = @IdUsuario