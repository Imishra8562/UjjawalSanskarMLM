CREATE TABLE [dbo].[tbl_Registration]
(
	[Registration_Id] INT IDENTITY(1,1) NOT NULL,
    [FK_User_Id] INT NOT NULL,
    [FK_Reg_Status_Id] INT NOT NULL,
    [Registration_Code] NVARCHAR(50) NULL,
    [Token_Id] NVARCHAR(50) NOT NULL,
    [Full_Name] NVARCHAR(100) NULL,
    [Password] NVARCHAR(100) NULL,

    [Sponsor_Id] NVARCHAR(50) NULL,
    [Sponsor_Name] NVARCHAR(100) NULL,
    [Parent_Id] NVARCHAR(50) NULL,
    [Parent_Name] NVARCHAR(100) NULL,
    [Position] NVARCHAR(50) NULL,
    [Gender] NVARCHAR(20) NULL,
    [DOB] DATETIME NULL,
    [Address] NVARCHAR(MAX) NULL,

    -- Commission
    [Mobile] NVARCHAR(20) NULL,
    [Email] NVARCHAR(100) NULL,

    -- KYC
    [PAN_No] NVARCHAR(20) NULL,
    [Aadhar_No] NVARCHAR(20) NULL,
    [Img_File] NVARCHAR(MAX) NULL,
    [Is_KYC_Approved] BIT NOT NULL DEFAULT(0),

    -- Bank Info
    [Bank_Name] NVARCHAR(100) NULL,
    [Account_No] NVARCHAR(100) NULL,
    [IFSC_Code] NVARCHAR(50) NULL,
    [Account_Holder_Name] NVARCHAR(100) NULL,
    [UPI_ID] NVARCHAR(150) NULL,
    [UPI_QR_Code] NVARCHAR(MAX) NULL,

    -- Payment Info
    [Transation_Id] NVARCHAR(100) NULL,
    [Payment_SS] NVARCHAR(MAX) NULL,
    [Payment_Date] DATETIME NULL,
    [Registration_Fee] DECIMAL(18,2) NOT NULL DEFAULT(0),
    [Registration_Gst] DECIMAL(18,2) NOT NULL DEFAULT(0),
    [Total_Reg_Fee_Paid] DECIMAL(18,2) NOT NULL DEFAULT(0),
    [IsRegFeeApproved] BIT NOT NULL DEFAULT(0),
    [IsPaid] BIT NOT NULL DEFAULT(0),
    [ActiveDate] DATETIME NULL,

    -- Team
    [Team_Business] DECIMAL(18,2) NOT NULL DEFAULT(0),
    [Gole_Completed]BIT NOT NULL DEFAULT(0),

    -- Audit Fields (Base)
    [Created_By] INT NOT NULL,
    [Created_IP] NVARCHAR(50) NULL,
    [Created_On] DATETIME NOT NULL DEFAULT(GETDATE()),
    [Modified_By] INT NULL,
    [Modified_IP] NVARCHAR(50) NULL,
    [Modified_On] DATETIME NULL,
    [Is_Active] BIT NOT NULL DEFAULT(1)

 CONSTRAINT [PK_tbl_Registration] PRIMARY KEY CLUSTERED ([Registration_Id] ASC)
 WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]