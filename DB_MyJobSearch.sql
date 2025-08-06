CREATE DATABASE DB_MyJobSearch
GO

USE DB_MyJobSearch
GO

-- jobaplication puede tener una modalidad de trabajo
-- jobaplication puede tener 1 o mas JobContact
-- jobaplication puede tener 0,1 JobPlace
-- jobaplication puede tener 0,1 o más JobLanguage


CREATE TABLE JobModality (
	id bigint primary key identity NOT NULL,
	modalityType varchar(100), -- remote, hybrid, Face-to-face, desc
);

CREATE TABLE JobPlace (
	id bigint primary key identity NOT NULL,
	country varchar(150),
	province varchar(150),
	city varchar(150),
	address varchar(250),
);

CREATE TABLE JobApplication (
	id bigint primary key identity NOT NULL,
	datePublication datetime,
	applicationDate datetime,
	jobName varchar(150),
	company varchar(150),
	applicationPage varchar(150),
	salary decimal,
	benefits varchar(5000),
	requirement varchar(5000),
	functions varchar(5000),
	softSkills varchar(5000),
	experience varchar(5000),
	jobModalityId bigint FOREIGN KEY REFERENCES JobModality(id),
	jobPlaceId bigint NULL FOREIGN KEY REFERENCES JobPlace(id),
);

CREATE TABLE JobContactType (
	id bigint primary key identity NOT NULL,
	name varchar(100), -- email, phone, whatsapp, other, sin contacto
);

CREATE TABLE JobContact (
	id bigint primary key identity NOT NULL,
	contact varchar(250),
	idContactType bigint foreign key references JobContactType(id),
	jobApplicationId bigint NOT NULL FOREIGN KEY REFERENCES JobApplication(id)
);

CREATE TABLE JobLanguage (
	id bigint primary key identity NOT NULL,
	language varchar(100),
	levelLanguage varchar(100),
);

-- Tabla intermedia para la relación muchos a muchos
CREATE TABLE JobApplicationLanguage (
	id bigint PRIMARY KEY IDENTITY NOT NULL,
	jobApplicationId bigint FOREIGN KEY REFERENCES JobApplication(id),
	jobLanguageId bigint FOREIGN KEY REFERENCES JobLanguage(id)
);

-- integrar api de paises y ciudades
-- integrar api de idiomas


INSERT INTO JobModality (modalityType)
VALUES 
  ('Remote'),
  ('Hybrid'),
  ('Face-to-face'),
  ('Unknown');


INSERT INTO JobPlace (country, province, city, address)
VALUES 
  ('Costa Rica', 'San José', 'San José', 'Av. Central, Edificio XYZ'),
  ('Costa Rica', 'Alajuela', 'Alajuela Centro', 'Calle 5, Plaza Real'),
  ('Costa Rica', 'Heredia', 'Belén', 'Zona Franca Metropolitana'),
  ('Panamá', 'Panamá', 'Ciudad de Panamá', 'Torre Global Bank, Piso 10');


INSERT INTO JobApplication (
  datePublication, applicationDate, jobName, company, applicationPage,
  salary, benefits, requirement, functions, softSkills, experience,
  jobModalityId, jobPlaceId
)
VALUES 
('2025-07-01', '2025-07-02', 'Backend Developer', 'TechCorp', 'LinkedIn',
 1200000, 'Seguro médico, vacaciones', 'Experiencia con .NET', 'Desarrollar APIs', 'Trabajo en equipo', '3 años de experiencia',
 1, 1),

('2025-07-03', '2025-07-05', 'Frontend Developer', 'CreativeSoft', 'Glassdoor',
 1000000, 'Horario flexible', 'React avanzado', 'Diseño UI/UX', 'Comunicación', '2 años en frontend',
 2, 2),

('2025-07-10', '2025-07-12', 'Project Manager', 'Innova Solutions', 'Elempleo',
1500000, 'Bonos trimestrales', 'Certificación PMP', 'Gestión de proyectos', 'Liderazgo', '5 años liderando equipos',
 3, 3),

('2025-07-15', '2025-07-16', 'QA Engineer', 'QA Labs', 'Indeed',
900000, 'Trabajo desde casa', 'Pruebas automatizadas', 'Diseñar planes de prueba', 'Pensamiento crítico', '2 años en QA',
 1, NULL);


INSERT INTO JobContactType (name)
VALUES 
  ('Email'),
  ('Phone'),
  ('WhatsApp'),
  ('Other'),
  ('Without contact');

INSERT INTO JobContact (contact, idContactType, jobApplicationId)
VALUES 
  ('reclutamiento@techcorp.com', 1, 1),
  ('(506) 8888-1234', 2, 2),
  ('+507 6543-9876', 3, 3),
  ('No se especifica', 5, 4);

INSERT INTO JobLanguage (language, levelLanguage)
VALUES 
  ('Inglés', 'Avanzado'),
  ('Español', 'Nativo'),
  ('Portugués', 'Intermedio'),
  ('Francés', 'Básico');


INSERT INTO JobApplicationLanguage (jobApplicationId, jobLanguageId)
VALUES 
  (1, 1), -- Inglés
  (1, 2), -- Español
  (2, 2),
  (2, 3),
  (3, 2),
  (4, 1),
  (4, 4);


ALTER PROCEDURE getAllJobApplications
AS BEGIN
	SELECT 
		ja.id, 
		datePublication, 
		applicationDate, 
		jobName, 
		company, 
		applicationPage, 
		salary, 
		benefits, 
		requirement, 
		functions, 
		softSkills, 
		experience, 
		m.id AS 'JobModalityId',
		m.modalityType, 
		p.id AS 'JobPlaceId',
		p.country, 
		p.province, 
		p.city, 
		p.address 
	FROM JobApplication ja
	LEFT JOIN JobModality m
	ON ja.jobModalityId = m.id
	LEFT JOIN JobPlace p 
	ON ja.jobPlaceId = p.id
END 
GO

CREATE PROCEDURE getJobApplicationById
	@id bigint
AS BEGIN
	SELECT 
		ja.id, 
		datePublication, 
		applicationDate, 
		jobName, 
		company, 
		applicationPage, 
		salary, 
		benefits, 
		requirement, 
		functions, 
		softSkills, 
		experience, 
		m.id AS 'JobModalityId',
		m.modalityType, 
		p.id AS 'JobPlaceId',
		p.country, 
		p.province, 
		p.city, 
		p.address 
	FROM JobApplication ja
	LEFT JOIN JobModality m
	ON ja.jobModalityId = m.id
	LEFT JOIN JobPlace p 
	ON ja.jobPlaceId = p.id
	WHERE ja.id = @id
END 
GO


ALTER PROCEDURE createJobApplication
	@datePublication datetime,
    @applicationDate datetime,
    @jobName varchar(150),
    @company varchar(150),
    @applicationPage varchar(150),
    @salary decimal(18,0),
    @benefits varchar(5000),
    @requirements varchar(5000),
    @functions varchar(5000),
    @softSkills varchar(5000),
    @experience varchar(5000),
    @jobModalityId bigint,
	@country varchar(150),
	@province varchar(150),
	@city varchar(150),
	@address varchar(250)
AS BEGIN
	INSERT INTO JobPlace (country, province, city, address)
	VALUES (@country, @province, @city, @address)

	DECLARE @jobPlaceId bigint = SCOPE_IDENTITY()

	INSERT INTO [dbo].[JobApplication]
           ([datePublication],[applicationDate],[jobName],[company],[applicationPage],[salary],[benefits],
		   [requirement],[functions],[softSkills],[experience],[jobModalityId],[jobPlaceId])
     VALUES
           (@datePublication,@applicationDate,@jobName,@company,@applicationPage,@salary,@benefits,@requirements,
		   @functions,@softSkills,@experience,@jobModalityId,@jobPlaceId)
END
GO


ALTER PROCEDURE updateJobApplication
	@id bigint,
	@jobPlaceId bigint,
	@jobModalityId bigint,
	@datePublication datetime,
    @applicationDate datetime,
    @jobName varchar(150),
    @company varchar(150),
    @applicationPage varchar(150),
    @salary decimal(18,0),
    @benefits varchar(5000),
    @requirements varchar(5000),
    @functions varchar(5000),
    @softSkills varchar(5000),
    @experience varchar(5000),
	@country varchar(150),
	@province varchar(150),
	@city varchar(150),
	@address varchar(250)
AS BEGIN
   UPDATE [dbo].[JobApplication]
   SET [datePublication] = @datePublication
      ,[applicationDate] = @applicationDate
      ,[jobName] = @jobName
      ,[company] = @company
      ,[applicationPage] = @applicationPage
      ,[salary] = @salary
      ,[benefits] = @benefits
      ,[requirement] = @requirements
      ,[functions] = @functions
      ,[softSkills] = @softSkills
      ,[experience] = @experience
      ,[jobModalityId] = @jobModalityId
	WHERE id = @id

	UPDATE [dbo].[JobPlace]
	SET [country] = @country
      ,[province] = @province
      ,[city] = @city
      ,[address] = @address
	WHERE id = @jobPlaceId
END
GO


ALTER PROCEDURE deleteJobApplication
	@id bigint
AS BEGIN
	DELETE FROM [dbo].[JobApplication]
	WHERE id = @id

    DELETE FROM [dbo].[JobPlace]
	WHERE id = @id
END
GO


select * from JobApplication
select * from JobPlace
select * from JobModality
