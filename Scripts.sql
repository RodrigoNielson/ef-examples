CREATE TABLE dbo.[Order] (
	Id uniqueidentifier PRIMARY KEY,
	Code varchar(255),
	CustomerName varchar(255),
	Date date
);

CREATE TABLE  dbo.OrderItem
(
	Id uniqueidentifier PRIMARY KEY,
	Quantity decimal(12,2),
	OrderId uniqueidentifier,
	ProductId uniqueidentifier
);

CREATE TABLE dbo.Product
(
	Id uniqueidentifier PRIMARY KEY,
	Name varchar(255),
	Description varchar(255),
	Price decimal(12,2)
);