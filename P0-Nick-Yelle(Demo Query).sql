/*
DDL: Data Definition Language
*/

CREATE TABLE Breweries (
    brewery_id INT PRIMARY KEY IDENTITY,
    Name VARCHAR(100),
    City VARCHAR(50),
    State VARCHAR(50)
);

CREATE TABLE Brews (
    brew_id INT PRIMARY KEY IDENTITY,
    Name VARCHAR(100) NOT NULL,
    Price INT,
    brewery_id INT FOREIGN KEY REFERENCES Breweries(brewery_id) ON DELETE CASCADE NOT NULL
);

ALTER TABLE Brews
ADD brew_quantity INT;

CREATE TABLE Customers (
    customer_id INT PRIMARY KEY IDENTITY,
    Name VARCHAR(100) NOT NULL
);

CREATE TABLE Orders (
    order_id INT PRIMARY KEY IDENTITY,
    customer_id INT FOREIGN KEY REFERENCES Customers(customer_id),
    order_placed BIT NOT NULL
);

CREATE TABLE Order_Item (
    order_item_id INT PRIMARY KEY IDENTITY,
    order_id INT FOREIGN KEY REFERENCES Orders(order_id),
    brew_id INT FOREIGN KEY REFERENCES Brews(brew_id),
    order_quantity INT
);

/*
DML: Data Manipulation Language
*/

INSERT INTO Breweries (Name, City, State) VALUES
('Night Shift Brewing', 'Boston', 'MA'),
('67 Degrees Brewing', 'Franklin', 'MA'),
('Lops Brewing', 'Woonsocket', 'RI'),
('Bog Iron Brewing', 'Norton', 'MA');

SELECT * FROM Breweries;

INSERT INTO Brews (Name, Price, brewery_id, brew_quantity) VALUES
('Pumpkin Piescraper', 10, 1, 80),
('Route 140', 8, 2, 120),
('Lemonade Shandy', 12, 3, 49),
('Off My Lawn', 10, 4, 43);

SELECT * FROM Brews;

DELETE FROM Brews WHERE brew_id > 0;

