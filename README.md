So, assuming we have the DB in Management Studio (SqlServer), lets open Visual Studio

STEP 1:

INSTALL NUGETS:

-Microsoft.EntityFrameworkCore.SqlServer 
-Microsoft.EntityFrameworkCore.Tools
-Microsoft.EntityFrameworkCore

STEP 2:

SCAFFOLD:

Open the NuGets console and copy paste:

Scaffold-DbContext "Server=localhost;Database=MOVIES;Trusted_Connection=True; Encrypt=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

Server name might be different, according to the one in your SQLServer client

STEP 3:

SET UP CONNECTION STRING:

Go to the appsettings.json and, right after "AllowedHosts": "*", we copy paste this:

,

//set connection string:

  "ConnectionStrings": {
    "MoviesContext": "Server=localhost;Database=MoviesDB;Trusted_Connection=True;"
  }

STEP 4:

SET UP SERVICES FOR DATABASE CONTEXT:

Go to program.cs and, right under the line builder.Services.AddControllersWithViews();
paste the following:

builder.Services.AddDbContext<MoviesContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesContext"));
});

********************************************************************************************************

CREATE THE DATABASE: just copypaste this code and query in SqlServer Management Studio

(it can be done in mysql, etc but the syntax changes, as well as the NuGet packages that 
must be downloaded later on)


**************************************************************************************************

CREATE DATABASE MOVIES;
USE MOVIES;

CREATE TABLE Directors (
    
    Director_name varchar(255) NOT NULL,
    Director_table_id int IDENTITY(1,1) PRIMARY KEY,
);

CREATE TABLE Films (
    
    Title varchar(255) NOT NULL,
    Genre varchar(255),
    Director_id int NOT NULL,
    Film_id int IDENTITY(1,1) PRIMARY KEY,
    Release_year int,
    foreign key (Director_id) references Directors(Director_table_id) ON DELETE CASCADE
);


CREATE TABLE Actors (
    
    Actor_name varchar(255) NOT NULL,
    Actor_id int IDENTITY(1,1) PRIMARY KEY,
);

CREATE TABLE FilmsAndActors (
    
	Table_id int IDENTITY(1,1) PRIMARY KEY,
    Film_id_fk int NOT NULL,
    Actor_id_fk int NOT NULL,
    FOREIGN KEY (Film_id_fk) REFERENCES Films(Film_id) ON DELETE CASCADE,
    FOREIGN KEY (Actor_id_fk) REFERENCES Actors(Actor_id) ON DELETE CASCADE
);


/*

NOW WE FILL THE DB

*/

insert into directors(director_name)
values ('Quentin Tarantino');
insert into directors(director_name)
values ('Steven Spielberg');
insert into directors(director_name)
values ('James Cameron');
insert into directors(director_name)
values ('Stanley Kubrick');
insert into directors(director_name)
values ('Tim Burton');
insert into directors(director_name)
values ('Christopher Nolan');
insert into directors(director_name)
values ('Martin Scorsese');
insert into directors(director_name)
values ('David Fincher');
insert into directors(director_name)
values ('Alejandro González Iñárritu');
insert into directors(director_name)
values ('Ridley Scott');

insert into actors(actor_name)
values ('Leonardo DiCaprio');
insert into actors(actor_name)
values ('Brad Pitt');
insert into actors(actor_name)
values ('Kate Winslet');
insert into actors(actor_name)
values ('Samuel L. Jackson');
insert into actors(actor_name)
values ('Robert DeNiro');
insert into actors(actor_name)
values ('Michael Keaton');
insert into actors(actor_name)
values ('Jack Nicholson');
insert into actors(actor_name)
values ('Michael Cane');
insert into actors(actor_name)
values ('Heath Ledger');
insert into actors(actor_name)
values ('Gary Oldman');
insert into actors(actor_name)
values ('Tom Cruise');
insert into actors(actor_name)
values ('Nicole Kidman');
insert into actors(actor_name)
values ('Malcolm McDowell');
insert into actors(actor_name)
values ('Morgan Freeman');
insert into actors(actor_name)
values ('Edward Norton');
insert into actors(actor_name)
values ('Johnny Depp');
insert into actors(actor_name)
values ('Helena Bonham Carter');
insert into actors(actor_name)
values ('Kevin Spacey');
insert into actors(actor_name)
values ('Matt Damon');
insert into actors(actor_name)
values ('Vera Farmiga');
insert into actors(actor_name)
values ('Tom Hardy');
insert into actors(actor_name)
values ('Joseph Gordon-Levitt');
insert into actors(actor_name)
values ('Christian Bale');
insert into actors(actor_name)
values ('Hugh Jackman');
insert into actors(actor_name)
values ('Cilian Murphy');
insert into actors(actor_name)
values ('Marion Cotillard');
insert into actors(actor_name)
values ('Joaquin Phoenix');
insert into actors(actor_name)
values ('Russell Crowe');
insert into actors(actor_name)
values ('Ray Liotta');
insert into actors(actor_name)
values ('Joe Pesci');
insert into actors(actor_name)
values ('Harrison Ford');
insert into actors(actor_name)
values ('Katie Holmes');
insert into actors(actor_name)
values ('Ellen Page');
insert into actors(actor_name)
values ('Winona Ryder');
insert into actors(actor_name)
values ('Emma Stone');
insert into actors(actor_name)
values ('Cate Blanchet');
insert into actors(actor_name)
values ('Tom Hanks');
insert into actors(actor_name)
values ('Jodie Foster');
insert into actors(actor_name)
values ('Matthew McConaughey');
insert into actors(actor_name)
values ('Anne Hathaway');
insert into actors(actor_name)
values ('Margot Robbie');
insert into actors(actor_name)
values ('Jonah Hill');
insert into actors(actor_name)
values ('Christoph Waltz');
insert into actors(actor_name)
values ('Jamie Foxx');
insert into actors(actor_name)
values ('Uma Thurman');
insert into actors(actor_name)
values ('John Travolta');
insert into actors(actor_name)
values ('David Carradine');
insert into actors(actor_name)
values ('Roy Scheider');
insert into actors(actor_name)
values ('Jeff Goldblum');
insert into actors(actor_name)
values ('Julianne Moore');
insert into actors(actor_name)
values ('Catherine-Zeta Jones');
insert into actors(actor_name)
values ('Stanley Tucci');
insert into actors(actor_name)
values ('Michael Fassbender');
insert into actors(actor_name)
values ('Eli Roth');
insert into actors(actor_name)
values ('Diane Kruger');
insert into actors(actor_name)
values ('Sam Neill');
insert into actors(actor_name)
values ('Laura Dern');

INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Kill Bill: Volume 1', 'Action, Martial Arts', 1, 2003); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Kill Bill: Volume 2', 'Action, Martial Arts', 1, 2004); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Pulp Fiction', 'Crime', 1, 1994); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Inglorious Basterds', 'War', 1, 2009); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Django Unchained', 'Western', 1, 2012); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Once Upon a Time in Hollywood', 'Comedy, Drama', 1, 2019); 

INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Jaws', 'Thriller', 2, 1975); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Raiders of the Lost Ark', 'Action, Adventure', 2, 1981); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Empire of the Sun', 'War', 2, 1987); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Jurassic Park', 'Science Fiction, Action', 2, 1997); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Saving Private Ryan', 'War', 2, 1998); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('The Terminal', 'Comedy, Drama', 2, 2004); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Catch Me If You Can', 'Crime, Comedy, Drama', 2, 2002); 

INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Titanic', 'Romance', 3, 1997); 

INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Eyes Wide Shut', 'Mystery, Drama', 4, 1999); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('A Clockwork Orange', 'Crime', 4, 1971); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('The Shining', 'Horror', 4, 1980); 

INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Beetlejuice', 'Fantasy, Horror, Comedy', 5, 1988); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Batman', 'Superhero', 5, 1989); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Edward Scissorhands', 'Fantasy, Romance', 5, 1990); 

INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Batman Begins', 'Superhero', 6, 2005); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('The Prestige', 'Mystery, Psychological Thriller', 6, 2006); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('The Dark Knight', 'Superhero', 6, 2008); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Inception', 'Science Fiction, Action', 6, 2010); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('The Dark Knight Rises', 'Superhero', 6, 2012);
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Interstellar', 'Science Fiction', 6, 2014); 

INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Taxi Driver', 'Psychological Thriller', 7, 1976); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Goodfellas', 'Crime', 7, 1990); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('The Aviator', 'Drama', 7, 2004); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('The Wolf of Wall Street', 'Black Comedy, Crime', 7, 2013); 

INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Se7en', 'Crime, Thriller', 8, 1995); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Fight Club', 'Psychological Thriller', 8, 1999); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('The Curious Case of Benjamin Button', 'Drama', 8, 2008);

INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Birdman or (The Unexpected Virtue of Ignorance)', 'Black Comedy, Drama', 9, 2014); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('The Revenant', 'Western, Adventure', 9, 2015); 

INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Blade Runner', 'Science Fiction', 10, 1982); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('Gladiator', 'Historical Drama', 10, 2000); 
INSERT INTO films (title, genre, director_id, Release_year)
VALUES ('The Martian', 'Science Fiction', 10, 2015); 

INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (1, 45);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (2, 45); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (2, 47);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (3, 45);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (3, 46);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (3, 4);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (4, 2); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (4, 53); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (4, 54); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (4, 55); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (4, 43); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (5, 43); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (5, 44); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (5, 1);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (5, 4);  
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (6, 2); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (6, 1); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (6, 41); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (7, 48); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (8, 31);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (9, 23);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (10, 56);  
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (10, 57); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (10, 49);   
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (11, 37); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (11, 19); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (12, 37); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (12, 51); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (12, 52);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (13, 37); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (13, 1);
 INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (14, 1); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (14, 3); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (15, 11); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (15, 12); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (16, 13); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (17, 7); 
 INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (18, 6); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (18, 34); 


INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (19, 6); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (19, 7); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (20, 16); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (20, 34); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (21, 23); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (21, 8); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (21, 25); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (21, 10); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (21, 14); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (21, 32)
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (22, 23); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (22, 24); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (22, 8); 

INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (23, 23); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (23, 8); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (23, 9); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (23, 10);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (23, 14); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (24, 1); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (24, 8); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (24, 21); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (24, 25); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (24, 22); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (24, 33); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (24, 26); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (25, 23); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (25, 21);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (25, 8);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (25, 40);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (25, 26);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (25, 10);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (25, 14);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (25, 22);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (25, 25);

INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (26, 39);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (26, 40);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (26, 8);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (26, 19);

INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (27, 5);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (27, 38);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (28, 29);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (28, 5);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (28, 30);

INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (29, 1);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (29, 36);

INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (30, 1);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (30, 41);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (30, 42);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (30, 39);

 INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (31, 2); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (31, 18); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (31, 14);

INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (31, 23); 

INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (32, 2); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (32, 15); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (32, 17); 

INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (33, 2); 
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (33, 36);


INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (34, 6);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (34, 15);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (34, 35);

INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (35, 1);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (35, 21);

INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (36, 31);

INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (37, 28);
INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (37, 27);

INSERT INTO filmsandactors (film_id_fk, actor_id_fk)
VALUES (38, 19);

