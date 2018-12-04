CREATE TABLE UserData(
    login varchar(40)  primary key,
    password varchar(50)
);

CREATE TABLE GuideUpdate(
    id int identity(1,1) primary key,
    source varchar(255) not null,
    posted datetime
);

CREATE TABLE Series(
    id int identity(1,1) primary key,
    title varchar(255) not null
);

CREATE TABLE Programme (
    id int identity(1,1) primary key,
    title varchar(255) not null,
    icon_url varchar(255), 
    seq_number varchar(8),
    series_id int references Series
);

CREATE TABLE Channel(
    id int identity(1,1) primary key,
    name varchar(255) not null,
    icon_url varchar(255) 
);

CREATE TABLE Feature(
    id int identity(1,1) primary key,
    type varchar(255) not null,
    name varchar(255) not null
);

CREATE TABLE Description(
    id int identity(1,1) primary key,
    content text not null,
    guideupdate_id int not null references GuideUpdate,
    programme_id int not null references Programme
);

CREATE TABLE TrackedSeries(
    user_login varchar(40) not null references UserData,
    series_id int not null references Series
);

CREATE TABLE FeatureExample(
    feature_id int not null references Feature,
    programme_id int not null references Programme
);


CREATE TABLE FavoriteFeatures(
    user_login varchar(40) not null references UserData,
    feature_id int not null references Feature
);

CREATE TABLE FavoriteProgrammes(
    user_login varchar(40) not null references UserData,
    programme_id int not null references Programme
);

CREATE TABLE Emission(
    id int identity(1,1) primary key,
    start datetime,
    stop datetime,
    channel_id int not null references Channel,
    programme_id int not null references Programme
);


CREATE TABLE FavoriteChannels(
    user_login varchar(40) not null references UserData,
    channel_id int not null references Channel
);

