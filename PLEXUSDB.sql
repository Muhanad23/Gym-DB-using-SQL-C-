-------GYM BRANCH----

create database PLEXUSDB
Go

use PLEXUSDB;
------------Table Creation-----------------

create table Gym_branch
(
ID int not null,
primary key(ID),
address varchar(200),
Manager int,---DONE--
);


create table Users
(
ID int not null,
primary key (ID),
Name VARCHAR(50) not null Unique, 
password varchar(50) not null DEFAULT '12345',
Gender CHAR(1),
Startdate date,
Enddate date,
Age int,
Branch_id int not null,--DOne--
foreign key (Branch_id) references Gym_branch ON UPDATE CASCADE,
);



CREATE TABLE Track
(
Type varchar(50) not null,
level int not null,
primary key (Type,level),
description varchar(200),
);


create table Equipment
(
code int not null,
primary key (code),
Name varchar(50),
status varchar(50),
Branch_id int not null,--FOREIGN KEY DONE---
Foreign key (branch_id) references Gym_branch ON DELETE CASCADE ON UPDATE CASCADE,
);


create table Trainer
(
ID int not null,
primary key(ID),--DOne--
Foreign key (ID) references Users ON UPDATE CASCADE,
salary int,
);


create table class
(
Name varchar(50) not null,
primary key(Name),
description varchar(200),
start_time varchar(50),
end_time varchar(50),
Day varchar(50),
trainer_id int not null,
foreign key (trainer_id) references trainer ON DELETE CASCADE ON UPDATE CASCADE
);

create table Trainee
(
ID int not null,--DONE--
primary key(ID),
Foreign key (ID) references users ON DELETE CASCADE ON UPDATE CASCADE, 
height float,
Active int,
Track varchar(50),
FitnessLevel int,--DONE--
Foreign key (Track,FitnessLevel) references Track ON DELETE SET NULL ON UPDATE CASCADE,
Membership_Length int,
start_weight float,
current_weight float,
Muscle_weight float,
Fat_weight float,
Trainer_rate int,
personal_trainer int,--DOne--
foreign key (personal_trainer) references trainer --code handling as if I set on delete there is a multiple cascade error--
);


create table Management
(
ID int not null,--DOne--
primary key (ID),
Foreign key (ID) references users ON DELETE CASCADE ON UPDATE CASCADE,
salary int ,
);


create table Training_schedule
(
track_level int not null,
track_type varchar(50) not null,
day varchar(50) not null,
primary key(day,track_type,track_level),
Foreign key (track_type,track_level) references track ON DELETE CASCADE ON UPDATE CASCADE,--DONE--
Description varchar(900),
Trainer_id int,--DONE--
Foreign key (Trainer_id) references Trainer ON DELETE SET NULL ON UPDATE CASCADE
);


create table challenge
(
Name varchar(50) not null,
primary key (Name),
info varchar(200),
);

create table Gym_classes
(
Branch_id int not null,
class_Name varchar(50) not null,
primary key(branch_id,class_Name),
Foreign key (branch_id) references Gym_branch ON DELETE CASCADE ON UPDATE CASCADE,--DONE--
Foreign key (class_Name) references class--DONE---
);

create table Gym_Tracks
(
Branch_id int not null,
track_level int not null,
track_type varchar(50) not null,
primary key(branch_id,track_type,track_level),
Foreign key (track_type,track_level) references track ON DELETE CASCADE ON UPDATE CASCADE,--DONE---
Foreign key (branch_id) references Gym_branch ON DELETE CASCADE ON UPDATE CASCADE--DONE--
);

create table Class_Trainee
(
Trainee_id int not null,--DONE--
Foreign key (Trainee_id) references Trainee ON DELETE CASCADE ON UPDATE CASCADE,
class_Name varchar(50) not null,
Foreign key (class_Name) references class,--DONE---
primary key(Trainee_id,Class_Name)
);

----------- inserting manager Foreign key to branch --------------
alter table Gym_Branch add foreign key (Manager) references MANAGEMENT ;


----------inserting values into tables---------
------For ID 1st number is gym branch 2nd number is user type third number is his number in the gym-----
insert into Gym_branch
values
(1,'Akshya Nagar 1st Block 1st Cross, Rammurthy nagar, Bangalore-560016',null);



insert into Users
values
(1000000,'Admin','12345','M','2018-11-30','2020-12-12',20,1);---branch 1 manager 0--

insert into Management
values
(1000000,12000);
update Gym_branch  set Manager = 1000000	where ID = 1;

insert into Track
values
('Weight Loss',1,'Lose small gained weight'),
('Weight Loss',2,'Lose moderate gained weight'),
('Weight Loss',3,'Lose over 30 kilos gained weight'),
('Weight gain',1,'gain small gained weight'),
('Weight gain',2,'gain moderate gained weight'),
('Weight gain',3,'gain over 30 kilos gained weight');



insert into Training_schedule
values
(1,'Weight Loss','sunday','50 push ups',null),
(2,'Weight Loss','friday','70 push ups',null),
(3,'Weight Loss','saturday','100 push ups',null);


insert into challenge
values
('New Dimension','lose weight as much as you can sweat '),
('Body Builder','Make the most out of your weight number and make it muscle weight'),
('ball of fire','Be the most active on the gym and make the most out of your time here');
