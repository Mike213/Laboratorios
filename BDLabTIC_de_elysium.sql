
 
/*

CREATE DATABASE BDLabTIC2

GO
USE BDLabTIC2
DROP  DATABASE BDLabTIC
CREATE DATABASE BDLabTIC

GO
USE BDLabTIC2
DROP  DATABASE BDLabTIC

*/
GO
CREATE DATABASE BDLabTIC

GO
USE BDLabTIC


CREATE TABLE Laboratorio(
	id int  IDENTITY (1,1) NOT NULL PRIMARY KEY,
	nombreLab nvarchar(30) NOT NULL,
	estado int NULL,
	)

CREATE TABLE Computadora(
	id int  IDENTITY (1,1) NOT NULL PRIMARY KEY,
	nombre nvarchar (50) NOT NULL,
	caracteristicas nvarchar(200) NOT NULL, 
	estado int NOT NULL ,
	idLab int NOT NULL 
)



CREATE TABLE Estado(
	id int  IDENTITY (1,1) NOT NULL PRIMARY KEY,
	nombreEst nvarchar(30) NOT NULL
)

CREATE TABLE Opcion(
	id int  IDENTITY (1,1) NOT NULL PRIMARY KEY,
	nombreOpcion nvarchar (100) NOT NULL,
	descripcionOpcion nvarchar (100) NULL,
	estadoOpcion int  NOT NULL

)


CREATE TABLE  Reservacion(
	id int  IDENTITY (1,1) NOT NULL PRIMARY KEY,
	idpersona int NOT NULL,
	tipoR int NOT NULL,  
	pcReservacion int NOT NULL, 
	fecha date NOT NULL, 
	horaEntrada time,
	horaSalida time ,
	observacion nvarchar(100) NULL
)



CREATE TABLE Rol(
	idrol int  IDENTITY (1,1) NOT NULL PRIMARY KEY,
	rolName nvarchar(30) NOT NULL,
	descripcion nvarchar(100) NOT NULL,
	estado int NOT NULL
)

CREATE TABLE RolOpcion(
	idRolOpciones int  IDENTITY (1,1) NOT NULL PRIMARY KEY,
	idOpcion int NOT NULL,
	idRol int NOT NULL
)

CREATE TABLE TipoReservacion(
	id int  IDENTITY (1,1) NOT NULL PRIMARY KEY,
	nombreTipoR varchar(30) NOT NULL,
	descripcion varchar(100) NOT NULL,
	estado int NULL
) 

CREATE TABLE Usuario(
	idUser int  IDENTITY (1,1) NOT NULL PRIMARY KEY,
	username nvarchar(30) NOT NULL, 
	email varchar(100) NOT NULL,
	pwd nvarchar(100) NULL,
	estado int NOT NULL,
	nombres nvarchar(100) NOT NULL,
	apellidos nvarchar(100) NOT NULL, 
	idRol int NOT NULL
	)
  
GO

ALTER TABLE computadora ADD CONSTRAINT fk_laboratorio_idlab FOREIGN KEY (idLab) REFERENCES laboratorio(id)
GO

 

ALTER TABLE Laboratorio ADD CONSTRAINT fk_estado_Laboratorio FOREIGN KEY (estado ) REFERENCES Estado(id)

GO 

ALTER TABLE Computadora ADD CONSTRAINT fk_estado_Computadora FOREIGN KEY (estado ) REFERENCES Estado(id)

GO 
ALTER TABLE Opcion ADD CONSTRAINT fk_estado_estadoOpcion FOREIGN KEY (estadoOpcion) REFERENCES Estado(id)

GO 

ALTER TABLE Reservacion ADD CONSTRAINT fk_Reservacion_idpersona FOREIGN KEY (idpersona) REFERENCES Usuario(idUser)

GO 

ALTER TABLE Reservacion ADD CONSTRAINT fk_Reservacion_tipoR FOREIGN KEY (tipoR) REFERENCES TipoReservacion(id)

GO 

ALTER TABLE Reservacion ADD CONSTRAINT fk_Reservacion_pcReservacion FOREIGN KEY (pcReservacion) REFERENCES Computadora(id)

GO 

ALTER TABLE Rol ADD CONSTRAINT fk_Rol_estado FOREIGN KEY (estado) REFERENCES Estado(id)

GO 

ALTER TABLE RolOpcion ADD CONSTRAINT fk_RolOpcion_idOpcion FOREIGN KEY (idOpcion) REFERENCES Opcion(id)

GO 

ALTER TABLE RolOpcion ADD CONSTRAINT fk_RolOpcion_idRol FOREIGN KEY (idRol) REFERENCES Rol(idrol)

GO 

ALTER TABLE TipoReservacion ADD CONSTRAINT fk_estado_TipoReservacion FOREIGN KEY (estado) REFERENCES Estado(id)

GO 

ALTER TABLE Usuario ADD CONSTRAINT fk_Rol_idRol FOREIGN KEY (idRol) REFERENCES Rol(idrol)

GO  
ALTER TABLE Usuario ADD CONSTRAINT fk_estado_estado FOREIGN KEY (estado) REFERENCES Estado(id)
  
GO

 
 


GO
INSERT INTO Estado  values ('activo')
INSERT INTO Estado  values ('inactivo')
 
INSERT INTO Laboratorio  values ('Nether',1) 
INSERT INTO Laboratorio  values ('Nether2',1) 
INSERT INTO Laboratorio  values ('Nether3',1) 

 
INSERT INTO Opcion  values ('realizar','EL usuario puede realizar una reservacion',1) 
INSERT INTO Opcion  values ('ver','EL usuario puedever las reservaciones',1) 

INSERT INTO Rol  values ('Estudiante', 'solo puede hacar reservaciones',1)
INSERT INTO Rol  values ('Administrador', 'Puede ver y hacer reservaciones',1) 
 
INSERT INTO Computadora  values ('Compu 1', 'caract',1,1)
INSERT INTO Computadora  values ('Compu 2', 'caract',1,2)
INSERT INTO Computadora  values ('Compu 3', 'caract',1,3)

INSERT INTO RolOpcion VALUES (1,2)
INSERT INTO RolOpcion VALUES (2,1)

INSERT INTO TipoReservacion  values ('Basico', 'Herramientas de Office',1)
INSERT INTO TipoReservacion  values ('Medio', 'Herramientas de Office + Herramientas de Diseño',1)
INSERT INTO TipoReservacion  values ('Avanzado', 'Herramientas de Office + Herramientas de Diseño + Herramientas de desarrollo',1)




INSERT INTO Usuario VALUES ('David01','david@gmail.com','1234',1,'David ','Morales ',1)
INSERT INTO Usuario VALUES ('Ashley01','ashley@gmail.com','1234',1,'Ashley','Plata ',2)
INSERT INTO Usuario VALUES ('michael01','michael@gmail.com','1234',1,'Michael','Orozco ',2)


 
 
INSERT INTO Reservacion  values (1,3,1,'2021-10-04','08:30:00 ','12:30:00','Ninguna todo salio bien') 
/*

EXEC pc_reservaciones_personal; 

Select * from Reservacion
Select * from Usuario
Select * from TipoReservacion
Select * from Rol  
CREATE PROCEDURE pc_reservaciones_personal AS 
SELECT      per.nombres  AS Nombre_Estudiante,per.apellidos  AS Apellido_Estudiante,r.rolName AS Rol,  tres.nombreTipoR AS Tipo_Reservacion
,lab.nombreLab AS Laboratorio,pc.nombre AS Computadora, res.fecha AS Fecha,
res.horaEntrada AS Inicio ,res.horaSalida AS Salida, res.observacion AS Comentario
 from Usuario AS per
JOIN  Reservacion AS res ON  per.idUser = res.idpersona
JOIN  TipoReservacion AS tres ON tres.id = res.tipoR
JOIN  Computadora AS pc  ON pc.id = res.pcReservacion
JOIN  Laboratorio AS lab ON  pc.id = lab.id
JOIN  Rol AS r ON  r.idrol = per.idRol

;




 SELECT        dbo.Usuario.idUser, dbo.Usuario.username, dbo.Rol.rolName, dbo.RolOpcion.idRolOpciones, dbo.Opcion.nombreOpcion
FROM            dbo.Usuario INNER JOIN
                         dbo.Rol ON dbo.Usuario.idRol = dbo.Rol.idrol INNER JOIN
                         dbo.RolOpcion ON dbo.Rol.idrol = dbo.RolOpcion.idRol INNER JOIN
                         dbo.Opcion ON dbo.RolOpcion.idOpcion = dbo.Opcion.id


						 Select * from RolOpcion

						 
						 */