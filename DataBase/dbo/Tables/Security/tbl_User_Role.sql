CREATE TABLE [dbo].[tbl_User_Role]
(
	[User_Role_Id] INT IDENTITY(1,1) NOT NULL, 
    [User_Role_Name] NVARCHAR(MAX) NOT NULL, 
    [User_Role_Description] NVARCHAR(MAX) NULL, 
    
    [Created_On] DATETIME NOT NULL DEFAULT (GETDATE()), 
    [Created_By] INT NOT NULL, 
    [Created_IP] NVARCHAR(MAX) NULL, 
    [Modified_On] DATETIME NULL, 
    [Modified_By] INT NULL,    
    [Modified_IP] NVARCHAR(MAX) NULL, 
    [Is_Active] BIT NOT NULL
CONSTRAINT [PK_tbl_User_Role] PRIMARY KEY CLUSTERED ([User_Role_Id] ASC)
 WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]
