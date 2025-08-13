CREATE PROCEDURE [dbo].[sp_Update_Agent_Password]
@Agent_Registration_Id INT=NULL,
@Old_Password NVARCHAR(MAX)=NULL,
@Password NVARCHAR(MAX)=NULL

AS
BEGIN
BEGIN TRY  
	
   DECLARE @User_Id INT = NULL
    
    SELECT @User_Id=Agent_Registration_Id FROM tbl_Agent_Registration WHERE Agent_Registration_Id=@Agent_Registration_Id AND Password=@Old_Password
  
  IF(@User_Id IS NOT NULL AND @User_Id>0)
  BEGIN
  UPDATE tbl_Agent_Registration SET Password=@Password WHERE Agent_Registration_Id=@User_Id
  SELECT @User_Id
  END 
  
END TRY   
BEGIN CATCH    
   DECLARE @ErrorMessage VARCHAR(MAX);    
   SELECT @ErrorMessage ='SP ERROR : UPDATE USER FAILED' + Char(13) + Char(10) + 'THE ERROR WAS : ' + Char(13) + Char(10) + ERROR_MESSAGE()   
   RAISERROR (@ErrorMessage, 16, 1)   
END CATCH
END
