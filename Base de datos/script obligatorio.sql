----------------------------------------------CREACION-DE-LA-BASE-DE-DATOS----------------------------------------------
use master
go

if exists(SELECT * FROM SysDataBases WHERE name='Obligatorio')
BEGIN
	DROP DATABASE Obligatorio
END
go

CREATE DATABASE Obligatorio
go

----------------------------------------------USUARIO-PARA-SERVIDOR----------------------------------------------
create login [IIS APPPOOL\DefaultAppPool] from windows
go

use Obligatorio
go

CREATE USER [IIS APPPOOL\DefaultAppPool] for login [IIS APPPOOL\DefaultAppPool]
go

exec sys.sp_addrolemember 'db_owner', [IIS APPPOOL\DefaultAppPool]
go

----------------------------------------------CREO-TABLAS----------------------------------------------
CREATE TABLE EMPLEADO(
	Usuario varchar(10) primary key
	check(Usuario like '[a-zA-Z][a-zA-Z][a-zA-Z][a-zA-Z][a-zA-Z][a-zA-Z][a-zA-Z][a-zA-Z][a-zA-Z][a-zA-Z]'),
	Contrasenia varchar(7) not null
	check(Contrasenia like '[a-zA-Z][a-zA-Z][a-zA-Z][a-zA-Z][0-9][0-9][0-9]'),
)
GO

CREATE TABLE PERIODISTA(
	Cedula int primary key check(Cedula > 0 and Cedula < 10000000),
	Nombre varchar(20) not null check(len(Nombre)>0),
	Mail varchar(50) not null check(Mail like '_%@_%.com')
)
GO

CREATE TABLE SECCION(
CodigoSeccion varchar(5) primary key check (CodigoSeccion like '[a-zA-Z][a-zA-Z][a-zA-Z][a-zA-Z][a-zA-Z]'),
Nombre varchar(15) not null check(len(Nombre)>0)
)
GO

CREATE TABLE NOTICIA(
CodigoNoticia varchar(4) primary key check(CodigoNoticia like '[a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9]'),
Fecha datetime not null, -- check(Fecha >= GETDATE()),
Titulo varchar(50) not null check(len(Titulo)>0),
Cuerpo varchar(200) not null check(len(Cuerpo)>0),
Importancia int not null check(Importancia>=1 and Importancia<=5),
CodigoSeccion varchar(5) not null foreign key references SECCION(CodigoSeccion),
Usuario varchar(10) not null foreign key(Usuario) references EMPLEADO(Usuario),
)
GO

CREATE TABLE ESCRITAPOR(
CodigoNoticia varchar(4) not null foreign key references NOTICIA(CodigoNoticia),
Cedula int not null foreign key references PERIODISTA(Cedula),
primary key(CodigoNoticia,Cedula)
)
GO

----------------------------------------------PROCEDIMIENTOS----------------------------------------------
create procedure AltaEmpleado @user varchar(10), @pass varchar(7) as
begin
	--valido que no exista un usuario con el mismo nombre
	if (exists(select * from EMPLEADO where Usuario = @user))
	   return -1 --ya existe el empleado
	else
		Begin
			--alta
			insert into EMPLEADO values(@user, @pass)

			if(@@ERROR != 0)
				return  -2 --error
			else
				return  1
		End
end
go

create procedure AltaSeccion @code varchar(5), @nombre varchar(15) as
begin
	--valido que no exista una seccion con el mismo codigo
	if (exists(select * from SECCION where CodigoSeccion = @code))
	   return -1 --ya existe la seccion

	else
		Begin
			--insert
			insert into SECCION (CodigoSeccion, Nombre) values(@code, @nombre)
			
			if(@@ERROR != 0)
				return -2 --error
			else
				return 1
		End
end
go

create procedure ModificoSeccion @code varchar(5), @nombre varchar(15) as
begin
	--valido que exista la seccion
	if (not exists(select * from SECCION where CodigoSeccion = @code))
	   return -1 --no existe la seccion

	else
		Begin
			--update
			update SECCION set Nombre = @nombre where CodigoSeccion = @code

			if(@@ERROR != 0)
				return -2 --error
			else
				return 1
		End
end
go

--con executeSqlCommand
create procedure EliminoSeccion @code varchar(5), @ret int output as
begin
	--valido que exista la seccion
	if (not exists(select * from SECCION where CodigoSeccion = @code))
	   set @ret =  -1 --no existe la seccion
	else
		Begin
			--valido que la seccion no tenga noticias asignadas
			if (exists(select * from NOTICIA where CodigoSeccion = @code))
				set @ret = -2 --la seccion tiene noticias asignadas
			else
			Begin
    			delete from SECCION where CodigoSeccion = @code
    		
    			if(@@ERROR != 0)
    				set @ret =  -3 --error
    			else
    				set @ret =  1
    		End
		End
end
go

create procedure AltaPeriodista @ced int, @nom varchar(20), @mail varchar(50) as
begin
	--valido que no exista un periodista activo con la misma cedula
	if (exists(select * from PERIODISTA where Cedula = @ced))
	   return -1 --ya existe el periodista

	else
		Begin
			--insert
			insert into PERIODISTA(Cedula, Nombre, Mail) values(@ced,@nom,@mail)
			
			if(@@ERROR != 0)
				return -2 --error
			else
				return 1
		End
end
go

create procedure EditoPeriodista @ced int, @nom varchar(20), @mail varchar(50) as
begin
	--valido que exista un periodista activo con la misma cedula
	if (not exists(select * from PERIODISTA where Cedula = @ced))
	   return -1 --no existe el periodista

	else
		Begin
			--actualizo el registro
			update PERIODISTA set Nombre = @nom, Mail = @mail where Cedula = @ced
			
			if(@@ERROR != 0)
				return -2 --error
			else
				return  1
		End
end
go

--con executeSqlCommand
create procedure EliminoPeriodista @ced int, @ret int output as
begin
	--valido que exista el periodista
	if (not exists(select * from PERIODISTA where Cedula = @ced))
	   set @ret =  -1 --no existe el periodista

	--valido que el periodista no tenga noticias asignadas
	else 
		Begin
			if (exists(select * from ESCRITAPOR where Cedula = @ced))
				set @ret = -2 -- el periodista tiene noticias asignadas
			else
				Begin
					delete from PERIODISTA where Cedula = @ced
    			
    				if(@@ERROR != 0)
    					set @ret =  -3 --error
    				else
    					set @ret =  1
				End
		End
end
go

----------------------------------------------DATOS-DE-PRUEBA----------------------------------------------
insert into EMPLEADO values
	( 'unempleado', 'aaaa111'),
	('qwertyuiop', 'qwer123'),
	('poiuytrewq', 'rewq321'),
	('nanananana', 'nana217')

insert into SECCION values
	('econo', 'Economia'),
	('depor', 'Deportes'),
	('tecno', 'Tecnologia'),
	('polit', 'Politica'),
	('polic', 'Policial')

insert into PERIODISTA values
	(1111111, 'Jeff Algiz', 'jeffalgiz@mail.com'),
	(2222222, 'Elon Hagalaz', 'el0nalaz@mail.com'),
	(3333333, 'Bernard Jera', 'JeraBern@mail.com'),
	(4444444, 'Bill Perthro', 'billyperthro@mail.com'),
	(5555555, 'Mark Ehwaz', 'Markehwaz@mail.com'),
	(6666666, 'Warren Dagaz', 'Warrenndagaz@mail.com'),
	(7777777, 'Larry Ansuz', 'Larryansuz@mail.com'),
	(8888888, 'Larry Berkano', 'berkanolarry@mail.com')

insert into Noticia (CodigoNoticia,Fecha,Titulo,Cuerpo,Importancia,CodigoSeccion,Usuario) 
values
	('no01', DATEADD(day,-1,getdate()), 'Muere parlamentario birtanico',						'Miembro del Partido Conservador, Amess murió a consecuencia de las heridas recibidas. La policía arrestó a un joven de 25 años.',			1, 'polit','poiuytrewq'),
	('no02', DATEADD(day,-1,getdate()), 'Red de puertos china controla el mundo',			'El gigante asiático le ha dado un fuerte impulso a su Ruta Marítima de la Seda para expandir su influencia por los océanos del mundo.',	2, 'depor','unempleado'),
	('no03', DATEADD(day,-1,getdate()), 'Triple crisis que enfrenta Reino Unido',			'Pandemia, inflación y escasez en la quinta economía del mundo auguran un invierno difícil para los británicos.',							3, 'tecno','poiuytrewq'),
	('no04', DATEADD(day,-1,getdate()), 'última oportunidad de conocer el orígen del covid',	'La Organización Mundial de la Salud presenta un nuevo grupo para examinar más de cerca la aparición del virus.',							4, 'polit','unempleado'),
	('no05', DATEADD(day,-2,getdate()), 'Ataque con arco y flechas que dejó 5 muertos',		'Un danés de 37 años identificado como Espen Andersen Bråthen está detenido como sospechoso.',												3, 'econo','poiuytrewq'),
	('no06', DATEADD(day,-2,getdate()), 'Receta económica de Portugal como ejemplo',			'Portugal dio un giro a las políticas austeridad que dominaron en Europa en los años de la crisis económica.',								2, 'polic','poiuytrewq'),
	('no07', DATEADD(day,-2,getdate()), 'Ataques de la derecha española contra Biden',		'Políticos del Partido Popular y de Vox rechazan la idea de que España pida disculpas por sus actos durante la conquista.',					5, 'polit','poiuytrewq'),
	('no08', DATEADD(day,-2,getdate()), 'uno de los mayores fracasos de salud pública',		'El gobierno británico fue demasiado lento en hacer confinamientos, pero el plan de vacunación fue un éxito.',								4, 'polic','unempleado'),
	('no09', DATEADD(day,-3,getdate()), 'McCartney: Lennon instigó la separación del grupo',	'El músico británico le explica a la BBC por qué se dio la separación de The Beatles.',														3, 'depor','poiuytrewq'),
	('no10', DATEADD(day,-3,getdate()), 'Crecimiento en compras online',						'Ciberlunes creció casi 30% en cantidad de visitas con respecto a noviembre de 2020',														2, 'econo','unempleado'),
	('no11', DATEADD(day,-3,getdate()), 'Muerte de feriante en un tiroteo',					'El segundo implicado en la muerte fue enviado a prisión preventiva por el homicidio ocurrido en noviembre de 2020 en Nuevo París.',		3, 'polit','poiuytrewq'),
	('no12', DATEADD(day,-3,getdate()), 'Lacalle Pou en velatorio del Jefe de Policía',		'El presidente llegó acompañado del ministro del Interior Luis Alberto Heber y demás autoridades policiales',								4, 'polit','unempleado'),
	('no13', DATEADD(day,-4,getdate()), 'Muere parlamentario birtanico',						'Miembro del Partido Conservador, Amess murió a consecuencia de las heridas recibidas. La policía arrestó a un joven de 25 años.',			1, 'polit','poiuytrewq'),
	('no14', DATEADD(day,-4,getdate()), 'Red de puertos china controla el mundo',			'El gigante asiático le ha dado un fuerte impulso a su Ruta Marítima de la Seda para expandir su influencia por los océanos del mundo.',	2, 'depor','poiuytrewq'),
	('no15', DATEADD(day,-4,getdate()), 'Triple crisis que enfrenta Reino Unido',			'Pandemia, inflación y escasez en la quinta economía del mundo auguran un invierno difícil para los británicos.',							3, 'tecno','unempleado'),
	('no16', DATEADD(day,-4,getdate()), 'última oportunidad de conocer el orígen del covid',	'La Organización Mundial de la Salud presenta un nuevo grupo para examinar más de cerca la aparición del virus.',							4, 'polit','unempleado'),
	('no17', DATEADD(day,-5,getdate()), 'Ataque con arco y flechas que dejó 5 muertos',		'Un danés de 37 años identificado como Espen Andersen Bråthen está detenido como sospechoso.',												3, 'econo','poiuytrewq'),
	('no18', DATEADD(day,-5,getdate()), 'Receta económica de Portugal como ejemplo',			'Portugal dio un giro a las políticas austeridad que dominaron en Europa en los años de la crisis económica.',								2, 'polic','unempleado'),
	('no19', DATEADD(day,-5,getdate()), 'Receta económica de Portugal como ejemplo',			'Portugal dio un giro a las políticas austeridad que dominaron en Europa en los años de la crisis económica.',								2, 'polic','unempleado'),
	('no20', DATEADD(day,-5,getdate()), 'McCartney: Lennon instigó la separación del grupo',	'El músico británico le explica a la BBC por qué se dio la separación de The Beatles.',														3, 'depor','poiuytrewq'),
	('no21', DATEADD(day,-1,getdate()), 'Crecimiento en compras online',						'Ciberlunes creció casi 30% en cantidad de visitas con respecto a noviembre de 2020',														2, 'econo','unempleado'),
	('no22', DATEADD(day,-1,getdate()), 'Muere parlamentario birtanico',						'Miembro del Partido Conservador, Amess murió a consecuencia de las heridas recibidas. La policía arrestó a un joven de 25 años.',			1, 'polit','poiuytrewq'),
	('no23', DATEADD(day,-1,getdate()), 'Red de puertos china controla el mundo',			'El gigante asiático le ha dado un fuerte impulso a su Ruta Marítima de la Seda para expandir su influencia por los océanos del mundo.',	2, 'depor','poiuytrewq'),
	('no24', DATEADD(day,-1,getdate()), 'Triple crisis que enfrenta Reino Unido',			'Pandemia, inflación y escasez en la quinta economía del mundo auguran un invierno difícil para los británicos.',							3, 'tecno','poiuytrewq'),
	('no25', DATEADD(day,-2,getdate()), 'última oportunidad de conocer el orígen del covid',	'La Organización Mundial de la Salud presenta un nuevo grupo para examinar más de cerca la aparición del virus.',							4, 'polit','unempleado'),
	('no26', DATEADD(day,-2,getdate()), 'Ataque con arco y flechas que dejó 5 muertos',		'Un danés de 37 años identificado como Espen Andersen Bråthen está detenido como sospechoso.',												3, 'econo','unempleado'),
	('no27', DATEADD(day,-2,getdate()), 'Receta económica de Portugal como ejemplo',			'Portugal dio un giro a las políticas austeridad que dominaron en Europa en los años de la crisis económica.',								2, 'polic','poiuytrewq'),
	('no28', DATEADD(day,-3,getdate()), 'Crecimiento en compras online',						'Ciberlunes creció casi 30% en cantidad de visitas con respecto a noviembre de 2020',														2, 'econo','unempleado'),
	('no29', DATEADD(day,-3,getdate()), 'McCartney: Lennon instigó la separación del grupo',	'El músico británico le explica a la BBC por qué se dio la separación de The Beatles.',														3, 'depor','poiuytrewq'),
	('no30', DATEADD(day,-3,getdate()), 'Crecimiento en compras online',						'Ciberlunes creció casi 30% en cantidad de visitas con respecto a noviembre de 2020',														2, 'econo','unempleado'),
	('no31', DATEADD(day,-3,getdate()), 'McCartney: Lennon instigó la separación del grupo',	'El músico británico le explica a la BBC por qué se dio la separación de The Beatles.',														3, 'depor','unempleado'),
	('no32', DATEADD(day,-4,getdate()), 'Crecimiento en compras online',						'Ciberlunes creció casi 30% en cantidad de visitas con respecto a noviembre de 2020',														2, 'econo','poiuytrewq'),
	('no33', DATEADD(day,-4,getdate()), 'Crecimiento en compras online',						'Ciberlunes creció casi 30% en cantidad de visitas con respecto a noviembre de 2020',														2, 'econo','unempleado'),
	('no34', DATEADD(day,-4,getdate()), 'Muere parlamentario birtanico',						'Miembro del Partido Conservador, Amess murió a consecuencia de las heridas recibidas. La policía arrestó a un joven de 25 años.',			1, 'polit','poiuytrewq'),
	('no35', DATEADD(day,-4,getdate()), 'Red de puertos china controla el mundo',			'El gigante asiático le ha dado un fuerte impulso a su Ruta Marítima de la Seda para expandir su influencia por los océanos del mundo.',	2, 'depor','unempleado'),
	('no36', DATEADD(day,-5,getdate()), 'Triple crisis que enfrenta Reino Unido',			'Pandemia, inflación y escasez en la quinta economía del mundo auguran un invierno difícil para los británicos.',							3, 'tecno','poiuytrewq'),
	('no37', DATEADD(day,-5,getdate()), 'última oportunidad de conocer el orígen del covid',	'La Organización Mundial de la Salud presenta un nuevo grupo para examinar más de cerca la aparición del virus.',							4, 'polit','poiuytrewq'),
	('no38', DATEADD(day,-5,getdate()), 'Ataque con arco y flechas que dejó 5 muertos',		'Un danés de 37 años identificado como Espen Andersen Bråthen está detenido como sospechoso.',												3, 'econo','poiuytrewq'),
	('no39', DATEADD(day,-5,getdate()), 'Crecimiento en compras online',						'Ciberlunes creció casi 30% en cantidad de visitas con respecto a noviembre de 2020',														2, 'econo','poiuytrewq')
	
insert into ESCRITAPOR values
	('no01',1111111),
	('no02',1111111),
	('no02',3333333),
	('no02',2222222),
	('no03',3333333),
	('no04',1111111),
	('no05',3333333),
	('no06',1111111),
	('no07',1111111),
	('no07',4444444),
	('no08',1111111),
	('no09',3333333),
	('no10',1111111),
	('no11',3333333),
	('no12',1111111),
	('no13',4444444),
	('no14',4444444),
	('no15',1111111),
	('no15',2222222),
	('no16',1111111),
	('no17',4444444),
	('no18',3333333),
	('no18',2222222),
	('no19',1111111),
	('no20',4444444),
	('no21',1111111),
	('no22',2222222),
	('no23',4444444),
	('no24',1111111),
	('no25',2222222),
	('no26',1111111),
	('no27',1111111),
	('no28',4444444),
	('no29',1111111),
	('no30',4444444),
	('no30',2222222),
	('no31',4444444),
	('no32',1111111),
	('no33',1111111),
	('no34',4444444),
	('no35',2222222),
	('no36',1111111),
	('no37',2222222),
	('no38',4444444),
	('no39',1111111)
go

alter table NOTICIA with nocheck
add check(Fecha>=getdate())
go