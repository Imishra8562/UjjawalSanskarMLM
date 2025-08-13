CREATE PROCEDURE [dbo].[sp_Add_Registration]
@FK_User_Id INT = NULL,
@FK_Reg_Status_Id INT = NULL,
@Registration_Code NVARCHAR(50) = NULL,
@Token_Id NVARCHAR(50) = NULL,
@Full_Name NVARCHAR(100) = NULL,
@Password NVARCHAR(100) = NULL,
@Sponsor_Id NVARCHAR(50) = NULL,
@Sponsor_Name NVARCHAR(100) = NULL,
@Parent_Id NVARCHAR(50) = NULL,
@Parent_Name NVARCHAR(100) = NULL,
@Position NVARCHAR(50) = NULL,
@Gender NVARCHAR(20) = NULL,
@DOB DATETIME = NULL,
@Address NVARCHAR(MAX) = NULL,
@Mobile NVARCHAR(20) = NULL,
@Email NVARCHAR(100) = NULL,
@PAN_No NVARCHAR(20) = NULL,
@Aadhar_No NVARCHAR(20) = NULL,
@Img_File NVARCHAR(MAX) = NULL,
@Is_KYC_Approved BIT = 0,
@Bank_Name NVARCHAR(100) = NULL,
@IFSC_Code NVARCHAR(50) = NULL,
@Account_No NVARCHAR(100) = NULL,
@Account_Holder_Name NVARCHAR(100) = NULL,
@UPI_ID NVARCHAR(150) = NULL,
@UPI_QR_Code NVARCHAR(MAX) = NULL,
@Transation_Id NVARCHAR(100) = NULL,
@Payment_SS NVARCHAR(MAX) = NULL,
@Payment_Date DATETIME = NULL,
@Registration_Fee DECIMAL(18,2) = 0,
@Registration_Gst DECIMAL(18,2) = 0,
@Total_Reg_Fee_Paid DECIMAL(18,2) = 0,
@IsRegFeeApproved BIT = 0,
@IsPaid BIT = 0,
@ActiveDate DATETIME = NULL,
@Team_Business DECIMAL(18,2) = 0,

-- Base
@Created_By INT = NULL,
@Created_IP NVARCHAR(50) = NULL,
@Created_On DATETIME = NULL,
@Modified_By INT = NULL,
@Modified_IP NVARCHAR(50) = NULL,
@Modified_On DATETIME = NULL,
@Is_Active BIT = 1
AS
BEGIN
    BEGIN TRY
        DECLARE @Registration_Id INT = (SELECT top 1 Registration_Id FROM tbl_Registration WHERE Email=@Email)

        IF(@Registration_Id IS NULL)
        BEGIN
        -- Generate Token_Id
        SELECT @Token_Id = 'US' + CAST(CAST(RAND() * 8999999 + 1000000 AS INT) AS NVARCHAR)


        -- Create User Entry
        INSERT INTO tbl_User (
            User_Name,
            Password,
            Email_Id,
            Mobile_No,
            FK_User_Role_Id,
            Is_Locked,
            Created_By,
            Created_IP,
            Created_On
        )
        VALUES (
            @Token_Id,
            CONVERT(VARBINARY(MAX), CAST(@Password AS VARCHAR)),
            @Email,
            @Mobile,
            3,
            0,
            @Created_By,
            @Created_IP,
            GETDATE()
        )

        SET @FK_User_Id = SCOPE_IDENTITY()

        -- Insert into tbl_Registration
        INSERT INTO tbl_Registration (
            FK_User_Id,
            FK_Reg_Status_Id,
            Registration_Code,
            Token_Id,
            Full_Name,
            Password,
            Sponsor_Id,
            Sponsor_Name,
            Parent_Id,
            Parent_Name,
            Position,
            Gender,
            DOB,
            Address,
            Mobile,
            Email,
            PAN_No,
            Aadhar_No,
            Img_File,
            Is_KYC_Approved,
            Bank_Name,
            IFSC_Code,
            Account_No,
            Account_Holder_Name,
            UPI_ID,
            UPI_QR_Code,
            Transation_Id,
            Payment_SS,
            Payment_Date,
            Registration_Fee,
            Registration_Gst,
            Total_Reg_Fee_Paid,
            IsRegFeeApproved,
            IsPaid,
            ActiveDate,
            Team_Business,
            Created_By,
            Created_IP,
            Created_On,
            Modified_By,
            Modified_IP,
            Modified_On
        )
        VALUES (
            @FK_User_Id,
            @FK_Reg_Status_Id,
            @Registration_Code,
            @Token_Id,
            @Full_Name,
            @Password,
            @Sponsor_Id,
            @Sponsor_Name,
            @Parent_Id,
            @Parent_Name,
            @Position,
            @Gender,
            @DOB,
            @Address,
            @Mobile,
            @Email,
            @PAN_No,
            @Aadhar_No,
            @Img_File,
            @Is_KYC_Approved,
            @Bank_Name,
            @IFSC_Code,
            @Aadhar_No,
            @Account_Holder_Name,
            @UPI_ID,
            @UPI_QR_Code,
            @Transation_Id,
            @Payment_SS,
            @Payment_Date,
            1059.32,
            190.68,
            1250,
            @IsRegFeeApproved,
            @IsPaid,
            @ActiveDate,
            @Team_Business,
            @Created_By,
            @Created_IP,
            GETDATE(),
            @Modified_By,
            @Modified_IP,
            @Modified_On
        )

        SET @Registration_Id = SCOPE_IDENTITY()

        -- Insert hierarchy level tracking
        DECLARE @LEVEL INT = 1
        WHILE (@Parent_Id IS NOT NULL AND @Parent_Id != '')
        BEGIN
            INSERT INTO tbl_Team_Level (
                Parent_Token_No,
                Child_Token_No,
                Level,
                Created_By,
                Created_IP,
                Created_On
            )
            VALUES (
                @Parent_Id,
                @Token_Id,
                @LEVEL,
                @Created_By,
                @Created_IP,
                GETDATE()
            )

            DECLARE @NEWSPID NVARCHAR(50) = @Parent_Id
            SET @Parent_Id = NULL
            SELECT @Parent_Id = Parent_Id FROM tbl_Registration WHERE Token_Id = @NEWSPID
            SET @LEVEL += 1
        END

        SELECT @Registration_Id
        END        
    END TRY
    BEGIN CATCH
    DECLARE @ErrorMessage NVARCHAR(MAX)

    SELECT @ErrorMessage = 
        'SP ERROR: ADD REGISTRATION FAILED' + CHAR(13) + CHAR(10) +
        'ErrorMessage: ' + ERROR_MESSAGE() + CHAR(13) + CHAR(10) +
        'ErrorSeverity: ' + CAST(ERROR_SEVERITY() AS NVARCHAR) + CHAR(13) + CHAR(10) +
        'ErrorState: ' + CAST(ERROR_STATE() AS NVARCHAR) + CHAR(13) + CHAR(10) +
        'ErrorLine: ' + CAST(ERROR_LINE() AS NVARCHAR) + CHAR(13) + CHAR(10) +
        'Procedure: ' + ISNULL(ERROR_PROCEDURE(), 'N/A')

    RAISERROR (@ErrorMessage, 16, 1)
END CATCH
END
