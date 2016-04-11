USE [master]
GO

CREATE DATABASE AddressBookDB;
GO

USE [AddressBookDB];

CREATE TABLE [dbo].[Person]
(
	[Id] INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR(512) NOT NULL,
    [Surname] NVARCHAR(512) NOT NULL
)
GO

CREATE TABLE dbo.[Email]
(
    [Id] INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (512) NOT NULL,
    [PersonId] INT NOT NULL,
	CONSTRAINT FK_Email_Person FOREIGN KEY (PersonId)
	REFERENCES Person(Id) ON DELETE CASCADE
)
GO

CREATE TABLE dbo.[Address]
(
	[Id] INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (512) NOT NULL,
	[PersonId] INT NOT NULL,
	CONSTRAINT FK_PhoneNumber_Person FOREIGN KEY (PersonId)
	REFERENCES Person(Id) ON DELETE CASCADE
)
GO

CREATE INDEX IX_Email_PersonId ON Email(PersonId)
CREATE INDEX IX_Address_PersonId on [Address] (PersonId)

insert into Person (Name, Surname) values ('Deborah', 'Wilson');
insert into Person (Name, Surname) values ('Amanda', 'Ford');
insert into Person (Name, Surname) values ('Nancy', 'Sanchez');
insert into Person (Name, Surname) values ('Ronald', 'Pierce');
insert into Person (Name, Surname) values ('Judy', 'Cooper');
insert into Person (Name, Surname) values ('Robert', 'Snyder');
insert into Person (Name, Surname) values ('Roy', 'Ruiz');
insert into Person (Name, Surname) values ('Henry', 'Owens');
insert into Person (Name, Surname) values ('Kenneth', 'Elliott');
insert into Person (Name, Surname) values ('Evelyn', 'Anderson');
insert into Person (Name, Surname) values ('Marilyn', 'Elliott');
insert into Person (Name, Surname) values ('Ann', 'Ramirez');
insert into Person (Name, Surname) values ('Helen', 'Hart');
insert into Person (Name, Surname) values ('Jerry', 'Reyes');
insert into Person (Name, Surname) values ('Jane', 'Jordan');
insert into Person (Name, Surname) values ('Donald', 'Kelly');
insert into Person (Name, Surname) values ('Anthony', 'Jordan');
insert into Person (Name, Surname) values ('Louis', 'Walker');
insert into Person (Name, Surname) values ('Donna', 'Nguyen');
insert into Person (Name, Surname) values ('Joyce', 'Gonzales');
insert into Person (Name, Surname) values ('Norma', 'Meyer');
insert into Person (Name, Surname) values ('Arthur', 'Chapman');
insert into Person (Name, Surname) values ('Donna', 'Schmidt');
insert into Person (Name, Surname) values ('Juan', 'Henry');
insert into Person (Name, Surname) values ('Lisa', 'Perry');
insert into Person (Name, Surname) values ('Daniel', 'Mitchell');
insert into Person (Name, Surname) values ('Denise', 'Gonzales');
insert into Person (Name, Surname) values ('Joe', 'Mason');
insert into Person (Name, Surname) values ('Wayne', 'Parker');
insert into Person (Name, Surname) values ('Brenda', 'Owens');
insert into Person (Name, Surname) values ('James', 'Olson');
insert into Person (Name, Surname) values ('Matthew', 'Lopez');
insert into Person (Name, Surname) values ('Nicholas', 'Mendoza');
insert into Person (Name, Surname) values ('Eugene', 'Gibson');
insert into Person (Name, Surname) values ('Craig', 'Cox');
insert into Person (Name, Surname) values ('Rebecca', 'Franklin');
insert into Person (Name, Surname) values ('Jeremy', 'Bowman');
insert into Person (Name, Surname) values ('Jane', 'Olson');
insert into Person (Name, Surname) values ('Joe', 'Lewis');
insert into Person (Name, Surname) values ('Jacqueline', 'Frazier');
insert into Person (Name, Surname) values ('Kathy', 'Richards');
insert into Person (Name, Surname) values ('Ashley', 'Campbell');
insert into Person (Name, Surname) values ('Keith', 'Ford');
insert into Person (Name, Surname) values ('Judy', 'Spencer');
insert into Person (Name, Surname) values ('Paul', 'Jacobs');
