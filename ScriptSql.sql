CREATE DATABASE UserManagerDB
GO
USE UserManagerDB
GO

CREATE TABLE TipoIdentificacion (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Codigo VARCHAR(5) NOT NULL UNIQUE,
    Nombre VARCHAR(50) NOT NULL
);

GO

INSERT INTO TipoIdentificacion (Codigo, Nombre) VALUES 
('CC', 'Cédula de Ciudadanía'),
('TI', 'Tarjeta de Identidad'),
('RC', 'Registro Civil'),
('PA', 'Pasaporte');

GO

CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    TipoIdentificacionId INT NOT NULL,
    NumeroIdentificacion VARCHAR(30) NOT NULL,
    CorreoElectronico VARCHAR(150) NOT NULL ,
    Contrasena NVARCHAR(256) NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT UQ_Usuarios_NumeroIdentificacion UNIQUE (TipoIdentificacionId, NumeroIdentificacion),
    CONSTRAINT FK_Usuarios_TipoIdentificacion FOREIGN KEY (TipoIdentificacionId)
        REFERENCES TipoIdentificacion(Id)
);

GO 

INSERT INTO Usuarios (Nombre, Apellido, TipoIdentificacionId , NumeroIdentificacion, CorreoElectronico, Contrasena) 
		VALUES ('Betsabe Junior', 'Hoyos Barrios', 1, '123456789', 'betsabehoyos@gmail.com', 'Bets4123*')

SELECT u.Id, u.Nombre, u.Apellido, t.Codigo, t.Nombre , u.NumeroIdentificacion, 
	   u.CorreoElectronico, u.Contrasena 
FROM Usuarios u
INNER JOIN 
	TipoIdentificacion t ON t.Id = u.TipoIdentificacionId


SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Users';
