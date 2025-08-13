CREATE PROCEDURE [dbo].[sp_Update_Registration]
@Registration_Id INT=NULL,
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
   BEGIN
    UPDATE tbl_Registration SET 
                              FK_Reg_Status_Id = @FK_Reg_Status_Id,
                              Full_Name  = @Full_Name,
                              Password = @Password,
                              Gender= @Gender,
                              DOB  = @DOB,
                              Address = @Address,
                              Mobile = @Mobile,
                              Email = @Email,
                              PAN_No = @PAN_No,
                              Aadhar_No = @Aadhar_No,
                              Img_File = @Img_File,
                              Is_KYC_Approved = @Is_KYC_Approved,
                              Bank_Name = @Bank_Name,
                              IFSC_Code = @IFSC_Code,
                              Account_No = @Account_No,
                              Account_Holder_Name = @Account_Holder_Name,
                              UPI_ID = @UPI_ID,
                              UPI_QR_Code = @UPI_QR_Code,                              
                              ActiveDate  = @ActiveDate,

                              Modified_By=@Modified_By,
                              Modified_On=GETDATE(),
                              Modified_IP=@Modified_IP                  
                       WHERE Registration_Id=@Registration_Id



    SELECT @Registration_Id

   END
    
END TRY   
BEGIN CATCH    
   DECLARE @ErrorMessage VARCHAR(MAX);    
   SELECT @ErrorMessage ='SP ERROR : UPDATE REGISTRATION FAILED' + Char(13) + Char(10) + 'THE ERROR WAS : ' + Char(13) + Char(10) + ERROR_MESSAGE()   
   RAISERROR (@ErrorMessage, 16, 1)   
END CATCH
END