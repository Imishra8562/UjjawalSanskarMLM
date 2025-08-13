CREATE PROCEDURE [dbo].[sp_List_Team_Level]
@Child_Token_No NVARCHAR(MAX)=NULL,
@Parent_Token_No NVARCHAR(MAX)=NULL

AS
BEGIN    
 BEGIN TRY   
      
      SELECT * FROM tbl_Team_Level WITH(NOLOCK) WHERE Parent_Token_No=ISNULL(@Parent_Token_No,Parent_Token_No) 
	  AND Child_Token_No=ISNULL(@Child_Token_No,Child_Token_No) AND Is_Active=1 ORDER BY Level DESC

 END TRY   
 BEGIN CATCH    
	DECLARE @ErrorMessage VARCHAR(MAX);    
   SELECT @ErrorMessage ='SP ERROR : LIST Team_Levels FAILED' + Char(13) + Char(10) + 'THE ERROR WAS : ' + Char(13) + Char(10) + ERROR_MESSAGE()   
	RAISERROR (@ErrorMessage, 16, 1)   
 END CATCH    
END