CREATE PROCEDURE [dbo].[sp_List_Email_OTP]
@Email_OTP_Id INT=NULL,
@Email_Id NVARCHAR(MAX)=NULL

AS
BEGIN    
 BEGIN TRY   

      IF @Email_OTP_Id=0 SET @Email_OTP_Id=NULL
      
      SELECT * FROM tbl_Email_OTP WITH(NOLOCK) WHERE Email_OTP_Id=ISNULL(@Email_OTP_Id,Email_OTP_Id) AND Email_Id=ISNULL(@Email_Id,Email_Id) AND Is_Active=1 ORDER BY Email_OTP_Id DESC

 END TRY   
 BEGIN CATCH    
	DECLARE @ErrorMessage VARCHAR(MAX);    
   SELECT @ErrorMessage ='SP ERROR : LIST Email_OTP FAILED' + Char(13) + Char(10) + 'THE ERROR WAS : ' + Char(13) + Char(10) + ERROR_MESSAGE()   
	RAISERROR (@ErrorMessage, 16, 1)   
 END CATCH    
END