-- Create the database
CREATE DATABASE EventDB;
GO

-- Use the database
USE EventDB;
GO

-- Create the Venue table
CREATE TABLE Venue ( 
    Venue_Id INT IDENTITY(1,1) PRIMARY KEY,
    Venue_Name VARCHAR(250) NOT NULL,
    Location VARCHAR(250) NOT NULL,
    Capacity INT NOT NULL,
    Image_Url VARCHAR(500)
);

-- Create the Event table (uses Venue_Id as a foreign key)
CREATE TABLE Event ( 
    Event_Id INT IDENTITY(1,1) PRIMARY KEY,
    Event_Name VARCHAR(250) NOT NULL,
    Event_Date DATETIME NOT NULL,
    Description VARCHAR(MAX),
    Venue_Id INT, -- foreign key to Venue table
    FOREIGN KEY (Venue_Id) REFERENCES Venue(Venue_Id)
);

-- Create the Booking table
CREATE TABLE Booking (
    Booking_Id INT IDENTITY(1,1) PRIMARY KEY,
    Event_Id INT NOT NULL,
    Venue_Id INT NOT NULL,
    Booking_Date DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (Event_Id) REFERENCES Event(Event_Id),
    FOREIGN KEY (Venue_Id) REFERENCES Venue(Venue_Id),
    CONSTRAINT UQ_Booking UNIQUE (Event_Id, Venue_Id) -- prevents double booking
);

-- Insert sample venues
INSERT INTO Venue (Venue_Name, Location, Capacity, Image_Url)
VALUES 
('Grand Hall', '123 Main Street, Cape Town', 500, 'https://example.com/images/grandhall.jpg'),
('Ocean View Arena', '456 Beach Road, Durban', 1000, 'https://example.com/images/oceanview.jpg'),
('Mountain Retreat Center', '789 Hilltop Ave, Pretoria', 300, 'https://example.com/images/mountainretreat.jpg');

-- Insert sample events (linked to Venue_Id)
INSERT INTO Event (Event_Name, Event_Date, Description, Venue_Id)
VALUES 
('Tech Conference 2025', '2025-09-15 09:00:00', 'A gathering of top tech innovators.', 1),
('Summer Music Fest', '2025-12-20 16:00:00', 'Live performances by top artists.', 2),
('Yoga and Wellness Retreat', '2025-07-10 08:00:00', 'A full-day event focused on mindfulness and well-being.', 3);

-- Insert sample bookings (matching correct Event_Id and Venue_Id)
INSERT INTO Booking (Event_Id, Venue_Id, Booking_Date)
VALUES 
(1, 1, '2025-06-01 10:00:00'),
(2, 2, '2025-06-05 12:30:00'),
(3, 3, '2025-06-08 09:45:00');

-- View all data
SELECT * FROM Venue;
SELECT * FROM Event;
SELECT * FROM Booking;
