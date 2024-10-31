CREATE DATABASE ERP;
GO

USE ERP;
GO

CREATE TABLE Client (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

INSERT INTO Client (Name, Email, Phone) VALUES
('Juan Pérez', 'juan.perez@example.com', '1234567890'),
('Ana Gómez', 'ana.gomez@example.com', '0987654321'),
('Carlos Ruiz', 'carlos.ruiz@example.com', '1122334455');
GO
