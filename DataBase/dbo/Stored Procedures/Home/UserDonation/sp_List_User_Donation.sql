CREATE PROCEDURE [dbo].[sp_List_User_Donation]
@User_Donation_Id INT=NULL

AS
BEGIN    
 BEGIN TRY   

      IF @User_Donation_Id=0 SET @User_Donation_Id=NULL
      
      SELECT * FROM tbl_User_Donation WITH(NOLOCK) WHERE User_Donation_Id=ISNULL(@User_Donation_Id,User_Donation_Id) 
      AND Is_Active=1 ORDER BY User_Donation_Id DESC

 END TRY   
 BEGIN CATCH    
	DECLARE @ErrorMessage VARCHAR(MAX);    
   SELECT @ErrorMessage ='SP ERROR : LIST User_Donation FAILED' + Char(13) + Char(10) + 'THE ERROR WAS : ' + Char(13) + Char(10) + ERROR_MESSAGE()   
	RAISERROR (@ErrorMessage, 16, 1)   
 END CATCH    
END