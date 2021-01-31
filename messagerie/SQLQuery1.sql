create database messagerie;

create table users(
    id varchar(50) PRIMARY KEY,
    mdp varchar(50) NOT NULL
)

create table conversation(
	uide varchar(50) foreign key references users,
	uidr varchar(50) foreign key references users,
	idc int primary key identity,
	constraint UN_conversation unique(uide,uidr)
)

create table message(
	msg varchar(7000) not null,
	idc int foreign key references conversation,
	datem datetime,
	uide varchar(50) foreign key references users
)


