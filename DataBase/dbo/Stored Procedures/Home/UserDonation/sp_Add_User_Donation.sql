CREATE PROCEDURE [dbo].[sp_Add_User_Donation]
@Full_Name NVARCHAR(MAX)=NULL,
@PhoneNo NVARCHAR(MAX)=NULL,
@DonationAmount decimal(18,2)=NULL,
@UTR_No VARCHAR(MAX)=NULL,
@DonationDate DATETIME=NULL,
@PaymentSS VARCHAR(MAX)=NULL,
@Note VARCHAR(MAX)=NULL,

@Created_On DATETIME=NULL,
@Created_By INT=NULL,
@Created_IP NVARCHAR(MAX)=NULL,
@Modified_On DATETIME=NULL,
@Modified_By INT=NULL,
@Modified_IP NVARCHAR(MAX)=NULL,
@Is_Active BIT=NULL

AS
BEGIN
BEGIN TRY  
	   

         INSERT INTO tbl_User_Donation(Full_Name,
		                           PhoneNo,
								   DonationAmount,
								   UTR_No,
								   DonationDate,
								   PaymentSS,
								   Note,
								   Created_By,
								   Created_IP)
		                    VALUES(@Full_Name,
							       @PhoneNo,
								   @DonationAmount,
								   @UTR_No,
								   @DonationDate,
								   @PaymentSS,
								   @Note,
								   @Created_By,
								   @Created_IP)

		 SELECT SCOPE_IDENTITY()


END TRY   
BEGIN CATCH    
   DECLARE @ErrorMessage VARCHAR(MAX);    
   SELECT @ErrorMessage ='SP ERROR : ADD Donation FAILED' + Char(13) + Char(10) + 'THE ERROR WAS : ' + Char(13) + Char(10) + ERROR_MESSAGE()   
   RAISERROR (@ErrorMessage, 16, 1)   
END CATCH
END