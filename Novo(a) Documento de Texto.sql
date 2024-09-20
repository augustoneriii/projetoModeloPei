CREATE DataBase Produto;
USE Produto;

Create Table Produto(
	Id Int Primary Key auto_increment NOT NULL,
    Nome varchar(255) NOT NULL,
    Preco double NOT NULL
);