CREATE TABLE [Department] (
	[id] INT IDENTITY(1,1),
	[name] NVARCHAR(50) NOT NULL,
	[totalStaff] INT DEFAULT 0,

	CONSTRAINT PK_Department PRIMARY KEY ([id])
);

CREATE TABLE [User] (
	[id] INT IDENTITY(1,1),
	[username] NVARCHAR(50) UNIQUE NOT NULL,
	[password] NVARCHAR(MAX) NOT NULL,
	[fullname] NVARCHAR(50) NOT NULL,
	[departmentId] INT,
	[salaryScale] NUMERIC(5,2) NOT NULL DEFAULT 0.0,
	[annualLeave] NUMERIC(4,1) DEFAULT 0.0,
	[imagePath] NVARCHAR(MAX),
	[isActive] BIT DEFAULT 0,
	[startDate] DATETIME DEFAULT GETDATE(),
	[isManager] BIT DEFAULT 0,
	[isDirector] BIT DEFAULT 0,

	CONSTRAINT PK_User PRIMARY KEY ([id]),
	CONSTRAINT FK_User FOREIGN KEY (departmentId) REFERENCES [Department]([id])
);

CREATE TABLE [AttendanceStatus] (
	[id] INT IDENTITY(1,1),
	[userId] INT,
	[startAt] DATETIME DEFAULT GETDATE(),
	[endAt] DATETIME,
	[position] NVARCHAR(50) NOT NULL,
	[confirmOn] DATETIME DEFAULT GETDATE(),

	CONSTRAINT PK_Attendance PRIMARY KEY ([id]),
	CONSTRAINT FK_AttendanceStatus FOREIGN KEY ([userId]) REFERENCES [User]([id])
);

CREATE TABLE [Leave] (
	[id] INT IDENTITY(1,1),
	[reason] NVARCHAR(MAX) NOT NULL,
	[leaveHour] INT NOT NULL,
	[leaveDate] DATE DEFAULT CAST(GETDATE() AS DATE),
	[requestBy] INT,
	[requestOn] DATETIME DEFAULT GETDATE(),
	[approveOn] DATETIME DEFAULT GETDATE(),
	
	CONSTRAINT PK_Leave PRIMARY KEY ([id]),
	CONSTRAINT FK_Leave FOREIGN KEY ([requestBy]) REFERENCES [User]([id]) 
);

CREATE TABLE [Salary] (
	[id] INT,
	[userId] INT,
	[salary] INT,
	[payOn] DATETIME DEFAULT GETDATE(),

	CONSTRAINT PK_Salary PRIMARY KEY ([id]),
	CONSTRAINT FK_Salary FOREIGN KEY ([userId]) REFERENCES [User]([id])
);

CREATE TRIGGER tr_UpdateAnnualLeave
ON [Leave]
AFTER UPDATE
AS
BEGIN
	DECLARE @UserId INT
	DECLARE @LeaveHour INT

	SELECT @UserId = [requestBy], @LeaveHour = [leaveHour] 
	FROM [inserted]

	UPDATE [User]
	SET [annualLeave] = ([annualLeave] * 8 - @LeaveHour) / 8
	WHERE [id] = @UserId
END;

/*
CREATE TRIGGER tr_UpdateTotalStaff
ON [User]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
	DECLARE 
END
*/