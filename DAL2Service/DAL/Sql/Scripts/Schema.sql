-- create data base is not exist
SET @database_name = "SharegainDb";
SET @CreateDb =  CONCAT('CREATE DATABASE IF NOT EXISTS `', @database_name, '`');

PREPARE stmp from @createDb;
EXECUTE stmp;
DEALLOCATE PREPARE stmp;


SET @CreateTableSignals = 'CREATE TABLE IF NOT EXISTS Signals (
	id int auto_increment primary key,
	TimeStemp datetime null,
	Time varchar(127) null,
	Date varchar(127) null,
	Value float null,
	Type varchar(127) null,
	Error bit
)

';

SET @CreateTableUsers = 'CREATE TABLE IF NOT EXISTS Users (
	id int auto_increment primary key,
	name varchar(255) not null,
	password longtext not null,
	constraint users_pk unique (name)
)

';

PREPARE stmp from @CreateTableSignals;
EXECUTE stmp;
DEALLOCATE PREPARE stmp;

PREPARE stmt FROM @createTableUsers;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;