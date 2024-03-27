

-- Create Roles Table
CREATE TABLE Roles (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

-- Create Permissions Table
CREATE TABLE Permissions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);


-- Create RolePermissions Table (Many-to-Many Relationship)
CREATE TABLE RolePermissions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RoleId INT,
    PermissionID INT,
    FOREIGN KEY (RoleId) REFERENCES Roles(Id),
    FOREIGN KEY (PermissionID) REFERENCES Permissions(Id)
);


-- Create Users Table
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
	RoleId INT NOT NULL,
	JwtToken NVARCHAR(MAX),
	FOREIGN KEY (RoleId) REFERENCES Roles(Id),
    -- Add other user-related fields as needed
);

-- Create RefreshTokens Table
CREATE TABLE RefreshTokens (
    UserId INT,
    Token NVARCHAR(MAX),
    ExpiryDateTime DATETIME,
    PRIMARY KEY (UserId),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

-- Product Catalog Tables
CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(10, 2) NOT NULL,
    -- Add other product-related fields as needed
);

CREATE TABLE Categories (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(MAX)
);

CREATE TABLE Brands (
    BrandId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(MAX)
);

CREATE TABLE ProductCategories (
	Id INT IDENTITY(1,1) PRIMARY KEY,	
    ProductId INT,
    CategoryId INT,
    FOREIGN KEY (ProductID) REFERENCES Products(Id),
    FOREIGN KEY (CategoryID) REFERENCES Categories(Id)
);

CREATE TABLE ProductBrands (
	Id INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT,
    BrandId INT,
    FOREIGN KEY (ProductID) REFERENCES Products(Id),
    FOREIGN KEY (BrandID) REFERENCES Brands(BrandID)
);

-- Shopping Cart Tables
CREATE TABLE ShoppingCarts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT, -- If you want to associate carts with users
    CreatedDateTime DATETIME DEFAULT GETDATE(),
    Completed BIT DEFAULT 0
);

CREATE TABLE ShoppingCartItems (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CartId INT,
    ProductId INT,
    Quantity INT,
    FOREIGN KEY (CartID) REFERENCES ShoppingCarts(Id),
    FOREIGN KEY (ProductID) REFERENCES Products(Id)
);

-- Checkout and Payment Tables
CREATE TABLE Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT, -- If you want to associate orders with users
    TotalAmount DECIMAL(10, 2),
    ShippingAddress NVARCHAR(MAX),
    PaymentMethod NVARCHAR(50),
    OrderDateTime DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id) -- Reference to the Users table
);

-- Order Items table if needed
CREATE TABLE OrderItems (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT,
    ProductId INT,
    Quantity INT,
    Price DECIMAL(10, 2),
    FOREIGN KEY (OrderID) REFERENCES Orders(Id),
    FOREIGN KEY (ProductID) REFERENCES Products(Id)
);

-- Payment Transactions table if needed
CREATE TABLE PaymentTransactions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT,
    Amount DECIMAL(10, 2),
    PaymentDateTime DATETIME DEFAULT GETDATE(),
    -- Add other payment-related fields as needed
    FOREIGN KEY (OrderID) REFERENCES Orders(Id)
);
