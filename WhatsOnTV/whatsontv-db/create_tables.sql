create table Programme (
	id int identity(1,1) primary key,
	title varchar(255) not null,
	img_url varchar(255)
);

create table DescriptionSource (
	id int identity(1,1) primary key, 
	name varchar(30) not null
);

create table Description (
	id int identity(1,1) primary key,
	content text not null,
	source_id int references DescriptionSource,
	programme_id int references Programme
);

create table Feature (
	id int identity(1,1) primary key,
	name varchar(30) not null,
	value varchar(30) not null,
	programme_id int references Programme
);

create table Channel(
	id int identity(1,1) primary key,
	name varchar(30) not null,
	icon_url varchar(255)
)

create table Emission (
	id int identity(1,1) primary key,
	start datetime not null,
	stop datetime not null,
	channel_id int references Channel,
	programme_id int references Programme
);

